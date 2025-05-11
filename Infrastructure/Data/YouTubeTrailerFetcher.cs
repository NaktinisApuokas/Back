using System.Text.Json;
using System.Text.RegularExpressions;

namespace FobumCinema.Infrastructure.Data
{
    public class YouTubeTrailerFetcher
    {
        private static readonly HttpClient httpClient = new HttpClient();
        public static async Task<string> GetTrailerUrlAsync(string movieName)
        {
            string query = $"{Uri.EscapeDataString(movieName + " trailer")}";
            string url = $"https://www.youtube.com/results?search_query={query}";

            var response = await httpClient.GetStringAsync(url);

            var match = Regex.Match(response, @"\/watch\?v=(.{11})");

            if (match.Success)
            {
                string videoId = match.Groups[1].Value;
                return $"https://www.youtube.com/watch?v={videoId}";
            }

            return null;
        }
    }
}
