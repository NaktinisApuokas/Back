using Microsoft.AspNetCore.Identity;
using HtmlAgilityPack;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using FobumCinema.Core.Entities;
using FobumCinema.API.Auth.Model;
using FobumCinema.Core.Interfaces;
using Newtonsoft.Json;

namespace FobumCinema.Infrastructure.Data
{
    public class DatabaseSeeder
    {

        private readonly IMovieRepository _MovieRepository;
        private readonly IScreeningRepository _ScreeningRepository;

        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly HttpClient client = new HttpClient();

        public DatabaseSeeder(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IMovieRepository MovieRepository, IScreeningRepository ScreeningRepository)
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

            var newAdminUser = new User
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

            //await ScrapeForumCinemaData(2003, "https://www.forumcinemas.lt/Movies/Kaunas/"); //Forum Cinema Kaunas
            await ScrapeCinamonKinoData(2006, "https://cinamonkino.com/mega/lt");
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
                            secondGenreNode = genreNodes[1];
                        }

                        var imgNode2 = newDoc.DocumentNode.SelectSingleNode(".//img[contains(@src, 'portrait_small')]");


                        var imgNode3 = newDoc.DocumentNode.SelectNodes(".//img");

                        var descriptionNode = newDoc.DocumentNode.SelectSingleNode("//div[@class='contboxrow']");

                        var durationNode = newDoc.DocumentNode.SelectSingleNode(".//b[contains(text(), 'min')]");

                        Movie movie2 = await _MovieRepository.GetByCinemaIdAndTitleAsync(CinemaID, titleNode.InnerText.Trim());

                        int MovieID = 0;

                        if (movie2 != null)
                        {
                            MovieID = movie2.Id;
                        }

                        if (movie2 == null)
                        {
                            var movie = new Movie
                            {
                                Title = titleNode.InnerText.Trim(),
                                Img = imgNode.Attributes["src"].Value.Replace("_micro", "_medium"),
                                Genre = secondGenreNode?.InnerText.Replace("\n", "").Replace("\r", "").Trim(),
                                CinemaId = CinemaID,
                                Duration = durationNode?.InnerText.Replace("\n", "").Replace("\r", "").Trim(),
                                Description = descriptionNode?.InnerText.Trim()
                            };
                            Movies.Add(movie);

                            Movie movie3 = await _MovieRepository.InsertAsync(movie);
                            MovieID = movie3.Id;
                        }


                        var screenings = list.SelectNodes(".//li[@class='showTime']");
                        if (screenings != null && MovieID != 0)
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
                                    EmptySeats = seatNodes[1];
                                }

                                var priceNode = docForScreenings.DocumentNode.SelectSingleNode(".//b[contains(text(), '€')]");

                                var screeningObj = new Screening
                                {
                                    Time = screening.InnerText.Trim(),
                                    Emptyseatnumber = EmptySeats == null ? "0" : EmptySeats.InnerText.Trim(),
                                    Price = priceNode?.InnerText.Trim(),
                                    MovieId = MovieID,
                                    Url = newTimeNode.Attributes["href"].Value
                                };
                                Screenings.Add(screeningObj);
                            }
                        }
                    }
                }
                await _ScreeningRepository.InsertRangeAsync(Screenings);

                // await FormatData(Movies, Screenings);
            }
        }

        public async Task ScrapeCinamonKinoData(int CinemaID, string url)
        {
            IWebDriver driver = new ChromeDriver();
           
            driver.Navigate().GoToUrl(url);
            await Task.Delay(3000);

            var movieElements = driver.FindElements(By.CssSelector("div.item.cell.cell-sm-6"));
            List<Movie> movies = new List<Movie>();
            List<Screening> screenings = new List<Screening>();

            foreach (var movieElement in movieElements)
            {
                var title = movieElement.FindElement(By.CssSelector("h3.h2")).Text;
                var imgSrc = movieElement.FindElement(By.CssSelector("div.card__poster")).GetAttribute("data-src");
                var genre = movieElement.FindElement(By.CssSelector("div.card__info__genre")).Text;

                var existingMovie = await _MovieRepository.GetByCinemaIdAndTitleAsync(CinemaID, title);
                int MovieID;

                var cardInfoDiv = movieElement.FindElement(By.CssSelector("div.card__info"));
                var detailPageLink = cardInfoDiv.FindElement(By.CssSelector("a")).GetAttribute("href");

                if (!string.IsNullOrEmpty(detailPageLink))
                {
                    driver.Navigate().GoToUrl(detailPageLink);
                    await Task.Delay(2000);

                    var DurationDiv = driver.FindElement(By.CssSelector("div.pageFilm__about"));

                    var durationElements = DurationDiv.FindElements(By.CssSelector("dd"));
                    string duration = durationElements.Count > 1 ? durationElements[1].Text : "N/A";

                    var DescriptionDiv = driver.FindElement(By.CssSelector("div.pageFilm__synopsis"));
                    var synopsisButton = DescriptionDiv.FindElement(By.CssSelector("button.pageFilm__synopsis__btn"));
                    synopsisButton.Click();
                    await Task.Delay(500);

                    var synopsisDiv = DescriptionDiv.FindElement(By.CssSelector("div"));
                    string description = synopsisDiv.Text;

                    if (existingMovie == null)
                    {
                        var movie = new Movie
                        {
                            Title = title,
                            Img = imgSrc,
                            Genre = genre,
                            CinemaId = CinemaID,
                            Duration = duration,
                            Description = description
                        };
                        movies.Add(movie);
                        existingMovie = await _MovieRepository.InsertAsync(movie);
                    }
                    MovieID = existingMovie.Id;

                    var screeningsDiv = driver.FindElement(By.CssSelector("div.filmSchedule"));

                    var screeningNodes = screeningsDiv.FindElements(By.CssSelector("li"));

                    if (screeningNodes != null && MovieID != 0)
                    {
                        foreach (var screeningNode in screeningNodes)
                        {
                            var timeDiv = screeningNode.FindElement(By.CssSelector("div.filmSchedule__time")).Text;
                            var EmptyseatnumberDiv = screeningNode.FindElement(By.CssSelector("span[style='padding-left:.5rem']")).Text;
                            var buttonDiv = screeningNode.FindElement(By.CssSelector("span[style='padding-left:.5rem']"));

                            var button = screeningNode.FindElement(By.CssSelector("a.button.button--small.button--full"));
                            button.Click();
                            await Task.Delay(3000);  

                            var screeningUrl = driver.Url;

                            var priceDiv = driver.FindElement(By.CssSelector("li.pageSeatPlan__ticketTypes--standard")); 
                            var price = priceDiv.FindElement(By.CssSelector("p")).Text;

                            driver.Navigate().Back();
                            await Task.Delay(2000);

                            try
                            {
                                screenings.Add(new Screening
                                {
                                    Time = timeDiv,
                                    Emptyseatnumber = EmptyseatnumberDiv,
                                    Price = price,
                                    MovieId = MovieID,
                                    Url = screeningUrl
                                });
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("An error occurred while extracting time: " + ex.Message);
                            }
                        }
                    }

                    driver.Navigate().Back();
                    await Task.Delay(3000);
                }
            }

            driver.Quit();

            await _ScreeningRepository.InsertRangeAsync(screenings);
        }

        public async Task DeleteScreenings()
        {
            await _ScreeningRepository.DeleteAllAsync();
        }

    }
}
