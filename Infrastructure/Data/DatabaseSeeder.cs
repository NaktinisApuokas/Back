using FobumCinema.API.Auth.Model;
using FobumCinema.Core.Entities;
using FobumCinema.Core.Interfaces;
using FobumCinema.Migrations;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Identity;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Text.Json;

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
            //await DeleteScreenings();
            await ScrapeForumCinemaData(3006, "https://www.forumcinemas.lt/"); //Forum Cinema Vilnius
            //await ScrapeCinamonKinoData(2006, "https://cinamonkino.com/mega/lt");
        }

        public async Task ScrapeForumCinemaData(int CinemaID, string url)
        {
            List<Movie> movies = new List<Movie>();
            List<Screening> screenings = new List<Screening>();

            var options = new ChromeOptions();
            options.AddArgument("--headless");
            using var driver = new ChromeDriver(options);

            driver.Navigate().GoToUrl(url);

            var updatedPage = driver.PageSource;
            var doc = new HtmlDocument();
            doc.LoadHtml(updatedPage);

            var movieCards = doc.DocumentNode.SelectNodes("//div[contains(@class, 'schedule-card__inner')]");

            if (movieCards != null)
            {
                foreach (var card in movieCards)
                {
                    var titleNode = card.SelectSingleNode(".//p[contains(@class, 'schedule-card__title')]");
                    var titleEngNode = card.SelectSingleNode(".//p[contains(@class, 'schedule-card__secondary-title')]");
                    var genreNode = card.SelectSingleNode(".//span[contains(@class, 'schedule-card__genre')]");
                    var timeNode = card.SelectSingleNode(".//time[contains(@class, 'schedule-card__time')]");
                    var hallNode = card.SelectSingleNode(".//p[contains(@class, 'schedule-card__hall')]");
                    var seatsNode = card.SelectSingleNode(".//p[contains(@class, 'schedule-card__option-title')]");
                    var linkNode = card.SelectSingleNode(".//a[contains(@class, 'schedule-card__primary-button')]");

                    var imgTag = card.SelectSingleNode(".//img[contains(@class, 'image__img')]");
                    string title = titleNode?.InnerText.Trim();
                    string titleEng = titleEngNode?.InnerText.Trim();

                    string imgUrl = null;

                    if (imgTag != null)
                    {
                        var srcSetAttr = imgTag.GetAttributeValue("data-srcset", null)
                                        ?? imgTag.GetAttributeValue("srcset", null);

                        if (!string.IsNullOrEmpty(srcSetAttr))
                        {
                            imgUrl = srcSetAttr.Split(',')[0].Split(' ')[0];

                            if (imgUrl.StartsWith("//"))
                            {
                                imgUrl = "https:" + imgUrl;
                            }
                        }
                    }

                    string genre = genreNode?.InnerText.Trim();
                    string time = timeNode?.InnerText.Trim();
                    string hall = hallNode?.InnerText.Trim();
                    string seats = seatsNode?.InnerText.Trim() ?? "0";
                    string link = linkNode?.Attributes["href"]?.Value;
                    string price = "0";


                    var MovieLinkNode = card.SelectSingleNode(".//div[contains(@class, 'schedule-card__title-container')]/a");
                    string MovieLink = MovieLinkNode?.Attributes["href"]?.Value;
                    string duration = "";
                    string description = "";

                    if (!string.IsNullOrEmpty(MovieLink))
                    {
                        driver.Navigate().GoToUrl(MovieLink.StartsWith("http") ? MovieLink : "https://www.forumcinemas.lt" + MovieLink);
                        var movieDetailPage = driver.PageSource;

                        var detailDoc = new HtmlDocument();
                        detailDoc.LoadHtml(movieDetailPage);

                        var durationNode = detailDoc.DocumentNode.SelectSingleNode("//p[contains(@class, 'movie-details__value')]");
                        var descriptionNode = detailDoc.DocumentNode.SelectSingleNode("//div[contains(@class, 'media-chess__content')]");

                        duration = durationNode?.InnerText.Trim();
                        description = descriptionNode?.InnerText.Trim();
                    }

                    if (!string.IsNullOrEmpty(link))
                    {
                        driver.Navigate().GoToUrl(link.StartsWith("http") ? link : "https://www.forumcinemas.lt" + link);
                        var ticketPage = driver.PageSource;

                        var detailDoc = new HtmlDocument();
                        detailDoc.LoadHtml(ticketPage);

                        var priceNodes = detailDoc.DocumentNode.SelectNodes("//ul[contains(@class, 'ticket-list__list')]//li[contains(@class, 'ticket-list__item')]//span[contains(@class, 'ticket-list__price')]");

                        if (priceNodes != null && priceNodes.Count >= 2)
                        {
                            price = priceNodes[1].InnerText.Trim(); 
                        }
                    }

                    var trailerUrl = await YouTubeTrailerFetcher.GetTrailerUrlAsync(titleEng);

                    Movie movie = await _MovieRepository.GetByCinemaIdAndTitleAsync(CinemaID, title);

                    int MovieID = 0;

                    if (movie != null)
                    {
                        MovieID = movie.Id;
                    }
                    else
                    {
                        movie = new Movie
                        {
                            Title = title,
                            TitleEng = titleEng,
                            Img = imgUrl,
                            Genre = genre,
                            CinemaId = CinemaID,
                            Description = description,
                            Duration = duration,
                            TrailerURL = trailerUrl,
                            Date = DateTime.Now.ToShortDateString(),
                            IsUpcoming = 0
                        };


                        movies.Add(movie);
                        var insertedMovie = await _MovieRepository.InsertAsync(movie);
                        MovieID = insertedMovie.Id;
                    }

                    if (!string.IsNullOrEmpty(time))
                    {

                        var screening = new Screening
                        {
                            Time = time,
                            Emptyseatnumber = seats,
                            Price = price,
                            MovieId = MovieID,
                            Url = link,
                            Date = DateTime.Now.ToShortDateString()
                        };
                        screenings.Add(screening);
                    }
                }

                await _ScreeningRepository.InsertRangeAsync(screenings);
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
                var title = "failed";
                var imgSrc = "failed";
                var genre = "failed";

                try
                {
                    title = movieElement.FindElement(By.CssSelector("h3.h2")).Text;
                }
                catch
                {

                }

                try
                {
                    imgSrc = movieElement.FindElement(By.CssSelector("div.card__poster")).GetAttribute("data-src");
                }
                catch
                {

                }

                try
                {
                    genre = movieElement.FindElement(By.CssSelector("div.card__info__genre")).Text;
                }
                catch
                {

                }

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

                    var titleEng = title;

                    try
                    {
                        titleEng = driver.FindElement(By.CssSelector("p.cover__originalName")).Text;
                    }
                    catch
                    {

                    }

                    var trailerUrl = await YouTubeTrailerFetcher.GetTrailerUrlAsync(titleEng);

                    if (existingMovie == null)
                    {
                        var movie = new Movie
                        {
                            Title = title,
                            TitleEng = titleEng,
                            Img = imgSrc,
                            Genre = genre,
                            CinemaId = CinemaID,
                            Duration = duration,
                            Description = description,
                            TrailerURL = trailerUrl,
                            Date = DateTime.Now.ToShortDateString(),
                            IsUpcoming = 0
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

                            //var button = screeningNode.FindElement(By.CssSelector("a.button.button--small.button--full"));
                            //button.Click();
                            //await Task.Delay(3000);

                            var screeningUrl = driver.Url;

                            //var priceDiv = driver.FindElement(By.CssSelector("li.pageSeatPlan__ticketTypes--standard"));
                            //var price = priceDiv.FindElement(By.CssSelector("p")).Text;
                            var price = "8.60";
                            //driver.Navigate().Back();
                            //await Task.Delay(2000);

                            try
                            {
                                screenings.Add(new Screening
                                {
                                    Time = timeDiv,
                                    Emptyseatnumber = EmptyseatnumberDiv,
                                    Price = price,
                                    MovieId = MovieID,
                                    Url = screeningUrl,
                                    Date = DateTime.Now.ToShortDateString()

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
