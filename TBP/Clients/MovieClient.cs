using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TBP.Clients.ActorModels;
using TBP.Clients.MovieModels;
using TBP.Entities;
using TBP.Interfaces;
using TBP.Options;

namespace TBP.Clients
{
    public class MovieClient : BaseClient, IMovieClient
    {
        private readonly IMDBApiOptions _options;

        public MovieClient(HttpClient httpClient, IOptions<IMDBApiOptions> options) : base(httpClient)
        {
            _options = options?.Value ?? throw new ArgumentNullException(nameof(options));
            SetBaseUri(_options.BaseURI);
        }

        public MovieClient(HttpClient httpClient, IMDBApiOptions options) : base(httpClient)
        {
            _options = options ?? throw new ArgumentNullException(nameof(options));
            SetBaseUri(_options.BaseURI);
        }

        public async Task<List<Actor>> GetMovieCrew(int movieID)
        {
            string requestUri = $"{_client.BaseAddress}/movie/{movieID}/credits?api_key={_options.APIKey}";
            var response = await _client.GetAsync(requestUri);
            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                var finalResult = JsonConvert.DeserializeObject<CreditsMovieClientResponse>(json);
                return MapActorObjects(finalResult);
            }
            return new List<Actor>();
        }

        public async Task<List<Movie>> GetPopularMovies(int pageNumber)
        {
            string requestUri = $"{_client.BaseAddress}/movie/popular?api_key={_options.APIKey}&page={pageNumber}";
            var response = await _client.GetAsync(requestUri);
            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                var finalResult = JsonConvert.DeserializeObject<PopularMovieClientResponse>(json);
                return MapMovieObjects(finalResult);
            }
            return new List<Movie>();
        }

        private List<Movie> MapMovieObjects(PopularMovieClientResponse response)
        {
            if (response == null || response.results == null)
                return new List<Movie>();

            return response.results.Select(i => new Movie
            {
                BackDropPath = i.backdrop_path,
                Id = Guid.NewGuid(),
                IMDBId = i.id,
                IMDBRating = i.vote_average,
                Language = i.original_language,
                Overview = i.overview,
                PosterPath = i.poster_path,
                ReleaseDate = i.release_date,
                Title = i.title
            }).ToList();
        }

        private List<Actor> MapActorObjects(CreditsMovieClientResponse response)
        {
            if (response == null || response.cast == null)
                return new List<Actor>();

            return response.cast.Select(i => new Actor
            {
                Character = i.character,
                Id = Guid.NewGuid(),
                Name = i.name,
                IMDBId = i.id,
                ProfilePath = i.profile_path
            }).ToList();
        }
    }
}
