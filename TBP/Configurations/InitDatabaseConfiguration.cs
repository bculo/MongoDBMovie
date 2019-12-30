using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TBP.Clients;
using TBP.Entities;
using TBP.Interfaces;
using TBP.Options;
using TBP.Repository;
using TBP.Services;

namespace TBP.Configurations
{
    public class InitDatabaseConfiguration : IInstaller
    {
        public bool DatabaseFillStarted { get; set; } = true;

        public void Configure(IServiceCollection services, IConfiguration configuration)
        {
            GetMoviesFromClient(configuration);
            GetGenresFromClinet(configuration);
        }

        private void GetMoviesFromClient(IConfiguration configuration)
        {
            Task.Run(async () => //run task
            {
                IMovieRepository repo = RepositoryFactory.GetMovieRepo(configuration);
                if (await repo.Count() == 0)
                {
                    //set flag
                    DatabaseFillStarted = true;

                    //get movies from client
                    var task1 = ClientFactory.GetMovieClient(configuration).GetPopularMovies(1);
                    var task2 = ClientFactory.GetMovieClient(configuration).GetPopularMovies(2);
                    await Task.WhenAll(task1, task2);

                    //concat fetched movies into single list
                    var movieList = new List<Movie>();
                    movieList.AddRange(task1.Result);
                    movieList.AddRange(task2.Result);

                    //remove duplicate movies based on IMDBid
                    movieList = movieList.GroupBy(i => i.IMDBId).Select(i => i.First()).ToList();

                    //write them into mongodb
                    IMovieService service = ServiceFactory.GetMovieService(configuration);
                    foreach (var movie in movieList)
                        await service.AddMovie(movie);
                }
            }).GetAwaiter().GetResult(); //block thread
        }

        private void GetGenresFromClinet(IConfiguration configuration)
        {
            Task.Run(async () => //run task
            {
                IMovieRepository repo = RepositoryFactory.GetMovieRepo(configuration);
                if (DatabaseFillStarted)
                {
                    //get movies from mongodb
                    List<Movie> movies = await repo.GetAll();

                    //get genres for each movie via client
                    IMovieClient client = ClientFactory.GetMovieClient(configuration);
                    foreach (var movie in movies)
                        movie.Genres = await client.GetMovieGenres(movie.IMDBId);

                    //add genres to movie in mongodb
                    IMovieService service = ServiceFactory.GetMovieService(configuration);
                    foreach(var movie in movies)
                    {
                        foreach(var movieGenre in movie.Genres)
                            await service.AddGenreToMovie(movie.Id.ToString(), movieGenre);
                    }   
                }
            }).GetAwaiter().GetResult(); //block thread
        }
    }

    public static class ServiceFactory
    {
        public static IMovieService GetMovieService(IConfiguration configuration)
        {
            var databaseOptions = configuration.GetSection(nameof(MovieDatabaseOptions)).Get<MovieDatabaseOptions>();
            return new MovieService(new MovieRepository(databaseOptions),
                new GenreRepository(databaseOptions),
                new Repository<MovieGenre>(databaseOptions));
        }
    }

    public static class RepositoryFactory
    {
        public static IMovieRepository GetMovieRepo(IConfiguration configuration)
        {
            var databaseOptions = configuration.GetSection(nameof(MovieDatabaseOptions)).Get<MovieDatabaseOptions>();
            return new MovieRepository(databaseOptions);
        }
    }

    public static class ClientFactory
    {
        public static IMovieClient GetMovieClient(IConfiguration configuration)
        {
            var apiOptions = configuration.GetSection(nameof(IMDBApiOptions)).Get<IMDBApiOptions>();
            return new MovieClient(new HttpClient(), apiOptions);
        }
    }
}
