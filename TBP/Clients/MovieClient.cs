using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TBP.Clients.ActorModels;
using TBP.Clients.GenreModels;
using TBP.Clients.MovieModels;
using TBP.Entities;
using TBP.Interfaces;
using TBP.Options;

namespace TBP.Clients
{
    public class MovieClient : BaseClient, IMovieClient
    {
        private readonly IMDBApiOptions _options;


        [ActivatorUtilitiesConstructor]
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

        public async Task<List<Character>> GetMovieCrew(Movie movie)
        {
            string requestUri = $"{_client.BaseAddress}/movie/{movie.IMDBId}/credits?api_key={_options.APIKey}";
            var response = await _client.GetAsync(requestUri);
            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                var finalResult = JsonConvert.DeserializeObject<CreditsMovieClientResponse>(json);
                return MapActorObjects(finalResult, movie);
            }
            return new List<Character>();
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

        public async Task<List<Genre>> GetMovieGenres(int imdbMovieId)
        {
            string requestUri = $"{_client.BaseAddress}/movie/{imdbMovieId}?api_key={_options.APIKey}";
            var response = await _client.GetAsync(requestUri);
            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                var finalResult = JsonConvert.DeserializeObject<GenreMovieClientResponse>(json);
                return MapGenreObjects(finalResult);
            }
            return new List<Genre>();
        }

        public async Task<Movie> GetMovie(int imdbId)
        {
            string requestUri = $"{_client.BaseAddress}/movie/{imdbId}?api_key={_options.APIKey}";
            var response = await _client.GetAsync(requestUri);
            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                var i = JsonConvert.DeserializeObject<MovieClientModel>(json);
                return new Movie
                {
                    BackDropPath = i.backdrop_path,
                    IMDBId = i.id,
                    IMDBRating = i.vote_average,
                    Language = i.original_language,
                    Overview = i.overview,
                    PosterPath = i.poster_path,
                    ReleaseDate = i.release_date,
                    Title = i.title
                };
            }
            return null;
        }

        private List<Genre> MapGenreObjects(GenreMovieClientResponse response)
        {
            if (response == null || response.genres == null)
                return new List<Genre>();

            return response.genres.Select(i => new Genre
            {
                IMDBId = i.id,
                Name = i.name
            }).ToList();
        }

        private List<Movie> MapMovieObjects(PopularMovieClientResponse response)
        {
            if (response == null || response.results == null)
                return new List<Movie>();

            return response.results.Select(i => new Movie
            {
                BackDropPath = i.backdrop_path,
                IMDBId = i.id,
                IMDBRating = i.vote_average,
                Language = i.original_language,
                Overview = i.overview,
                PosterPath = i.poster_path,
                ReleaseDate = i.release_date,
                Title = i.title
            }).ToList();
        }

        private List<Character> MapActorObjects(CreditsMovieClientResponse response, Movie movie)
        {
            if (response == null || response.cast == null)
                return new List<Character>();

            return response.cast.Select(i => new Character
            {
                MovieId = movie.Id,
                CharacterInMovie = i.character,
                Name = i.name,
                IMDBId = i.id,
                ProfilePath = i.profile_path
            }).ToList();
        }
    }
}
