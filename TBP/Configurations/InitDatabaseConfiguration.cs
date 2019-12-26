using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
        public void Configure(IServiceCollection services, IConfiguration configuration)
        {
            Task.Run( async () =>
            {
                IMovieRepository repo = RepositoryFactory.GetMovieService(configuration);
                if (await repo.Count() == 0)
                { 
                    var task1 = ClientFactory.GetMovieClient(configuration).GetPopularMovies(1);
                    var task2 = ClientFactory.GetMovieClient(configuration).GetPopularMovies(2);
                    var task3 = ClientFactory.GetMovieClient(configuration).GetPopularMovies(3);

                    await Task.WhenAll(task1, task2, task3);

                    var movieList = new List<Movie>();

                    movieList.AddRange(task1.Result);
                    movieList.AddRange(task2.Result);
                    movieList.AddRange(task3.Result);

                    movieList = movieList.GroupBy(i => i.IMDBId).Select(i => i.First()).ToList();

                    IMovieService service = ServiceFactory.GetMovieService(configuration);

                    movieList.ForEach(i => service.AddMovie(i));
                }
            });
        }
    }

    public static class ServiceFactory
    {
        public static IMovieService GetMovieService(IConfiguration configuration)
        {
            var databaseOptions = configuration.GetSection(nameof(MovieDatabaseOptions)).Get<MovieDatabaseOptions>();
            return new MovieService(new MovieRepository(databaseOptions));
        }
    }

    public static class RepositoryFactory
    {
        public static IMovieRepository GetMovieService(IConfiguration configuration)
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
