using FobumCinema.Auth.Model;
using FobumCinema.Data.Dtos.Auth;
using Microsoft.AspNetCore.Identity;
using HtmlAgilityPack;
using FobumCinema.Data.Entities;
using FobumCinema.Data.Repositories;

namespace FobumCinema.Data
{
    public class DatabaseSeeder
    {

        private readonly IMovieRepository _MovieRepository;
        private readonly IScreeningRepository _ScreeningRepository;

        private readonly UserManager<FobumCinemaUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly HttpClient client = new HttpClient();
        public DatabaseSeeder(UserManager<FobumCinemaUser> userManager, RoleManager<IdentityRole> roleManager, IMovieRepository MovieRepository, IScreeningRepository ScreeningRepository)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _MovieRepository = MovieRepository;
            _ScreeningRepository = ScreeningRepository;
        }

        public async Task SeedAsync()
        {
            foreach (var role in UserRoles.All)
            {
                var roleExist = await _roleManager.RoleExistsAsync(role);
                if (!roleExist)
                {
                    await _roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            var newAdminUser = new FobumCinemaUser
            {
                UserName = "admin",
                Email = "admin@admin.com"
            };

            var existingAdminUser = await _userManager.FindByNameAsync(newAdminUser.UserName);
            if (existingAdminUser == null)
            {
                var createAdminUserResult = await _userManager.CreateAsync(newAdminUser, "VeryStrong1!");
                if (createAdminUserResult.Succeeded)
                {
                    await _userManager.AddToRolesAsync(newAdminUser, UserRoles.All);
                }
            }
        }

        public async Task ScrapeData()
        {
            await DeleteScreenings();

            await ScrapeForumCinemaData(2, "https://www.forumcinemas.lt/Movies/Kaunas/"); //Forum Cinema Kaunas
        }
        public async Task ScrapeForumCinemaData(int CinemaID, string url)
        {
            Random random = new Random();

            List<Movie> Movies = new();

            List<Screening> Screenings = new();

            var forumCinemasKaunasPage = await client.GetStringAsync(url);

            var forumCinemasKaunasDoc = new HtmlDocument();
            forumCinemasKaunasDoc.LoadHtml(forumCinemasKaunasPage);

            var forumCinemasKaunasMoviesList = forumCinemasKaunasDoc.DocumentNode.SelectNodes("//td[contains(@class, 'result')]");


            if (forumCinemasKaunasMoviesList != null)
            {
                foreach (var list in forumCinemasKaunasMoviesList)
                {
                    if (!string.IsNullOrEmpty(list.InnerHtml))
                    {
                        var titleNode = list.SelectSingleNode(".//a[contains(@class, 'result_h4')]");

                        var imgNode = list.SelectSingleNode(".//img");

                        var link = titleNode.Attributes["href"].Value;
                        var newPage = await client.GetStringAsync(link);
                        var newDoc = new HtmlDocument();
                        newDoc.LoadHtml(newPage);

                        var genreNode = newDoc.DocumentNode.SelectSingleNode("//div[contains(@style, 'margin-top: 10px;')]");

                        var genreNodes = newDoc.DocumentNode.SelectNodes("//div[contains(@style, 'margin-top: 10px;')]");

                        HtmlNode secondGenreNode = null;

                        if (genreNodes != null && genreNodes.Count > 1)
                        {
                            secondGenreNode = genreNodes[1]; // Index is 0-based, so 1 is the second item
                        }

                        var imgNode2 = newDoc.DocumentNode.SelectSingleNode(".//img[contains(@src, 'portrait_small')]");


                        var imgNode3 = newDoc.DocumentNode.SelectNodes(".//img");

                        var descriptionNode = newDoc.DocumentNode.SelectSingleNode("//div[@class='contboxrow']");

                        var durationNode = newDoc.DocumentNode.SelectSingleNode(".//b[contains(text(), 'min')]");

                        var movie = new Movie
                        {
                            Id = random.Next(99999999),
                            Title = titleNode.InnerText.Trim(),
                            Img = imgNode.Attributes["src"].Value,
                            Genre = secondGenreNode?.InnerText.Replace("\n", "").Replace("\r", "").Trim(),
                            CinemaId = CinemaID,
                            Duration = durationNode?.InnerText.Replace("\n", "").Replace("\r", "").Trim(),
                            Description = descriptionNode?.InnerText.Trim()
                        };
                        Movies.Add(movie);

                        var screenings = list.SelectNodes(".//li[@class='showTime']");
                        if (screenings != null)
                        {
                            foreach (var screening in screenings)
                            {
                                var newTimeNode = screening.SelectSingleNode(".//a");
                                var pageForScreenings = await client.GetStringAsync(newTimeNode.Attributes["href"].Value);
                                var docForScreenings = new HtmlDocument();
                                docForScreenings.LoadHtml(pageForScreenings);

                                var seatNodes = docForScreenings.DocumentNode.SelectNodes("//td[@align='right']");

                                HtmlNode EmptySeats = null;

                                if (seatNodes != null && seatNodes.Count > 1)
                                {
                                    EmptySeats = seatNodes[1]; // Index is 0-based, so 1 is the second item
                                }

                                var priceNode = docForScreenings.DocumentNode.SelectSingleNode(".//b[contains(text(), '€')]");

                                var screeningObj = new Screening
                                {
                                    Id = random.Next(99999999),
                                    Time = screening.InnerText.Trim(),
                                    Emptyseatnumber = Convert.ToInt16(EmptySeats.InnerText.Trim()),
                                    Price = priceNode?.InnerText.Trim(),
                                    MovieId = movie.Id,
                                    Url = newTimeNode.Attributes["href"].Value
                                };
                                Screenings.Add(screeningObj);
                            }
                        }
                    }
                }
                FormatData(Movies, Screenings);
            }
        }
        public async Task DeleteScreenings()
        {
            await _ScreeningRepository.DeleteAllAsync();
        }

        public async Task FormatData(List<Movie> Movies, List<Screening> Screenings)
        {
            foreach (Movie movie in Movies)
            {
                Movie movie2 = await _MovieRepository.GetByCinemaIdAndTitleAsync(movie.CinemaId, movie.Title);

                if (movie2 == null)
                {
                    await _MovieRepository.InsertAsync(movie);
                }
                else
                {
                    Screenings.Where(o => o.MovieId == movie2.Id).ToList().ForEach(o => o.MovieId = movie2.Id);
                }
            }

            await _ScreeningRepository.InsertRangeAsync(Screenings);
        }
    }
}
