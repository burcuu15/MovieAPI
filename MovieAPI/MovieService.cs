using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;

//TheMovieDB API üzerinden popüler filmleri çekmek için bir HTTP isteği gönderir ve sonuçları JArray formatında döndürür
namespace MovieAPI
{
    public class MovieService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private readonly int _maxMovies;

        public MovieService(IConfiguration configuration, HttpClient httpClient)
        {
            _httpClient = httpClient;
            _apiKey = configuration["TheMovieDB:ApiKey"];
            _maxMovies = int.Parse(configuration["TheMovieDB:MaxMovies"]);
        }

        public async Task<JArray> FetchMoviesAsync()
        {
            var response = await _httpClient.GetStringAsync($"https://api.themoviedb.org/3/movie/popular?api_key={_apiKey}");
            var movies = JObject.Parse(response)["results"] as JArray;

            return new JArray(movies.Take(_maxMovies));
        }
    }
}
