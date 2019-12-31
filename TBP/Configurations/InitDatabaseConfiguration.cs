using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
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
        public bool DatabaseFillStarted { get; set; } = false;

        public void Configure(IServiceCollection services, IConfiguration configuration)
        {
            CreateUserRoles(configuration);
            GetMoviesFromClient(configuration);
            GetGenresFromClinet(configuration);
        }

        private void CreateUserRoles(IConfiguration configuration)
        {
            Task.Run(async () => //run task
            {
                IRepository<Role> roleRepo = RepositoryFactory.GetRoleRepo(configuration);
                if(await roleRepo.Count() == 0)
                {
                    //add roles to mongoDB
                    var roles = configuration.GetSection(nameof(Role)).Get<Role[]>();
                    bool successRoles = await roleRepo.AddRange(roles);
                    if (!successRoles)
                        throw new Exception("error - roles");

                    //add admin to mongoDB
                    IPassword hasher = ServiceFactory.GetPasswordHasher(configuration);
                    IRepository<User> userRepo = RepositoryFactory.GetUserRepo(configuration);
                    var admin = configuration.GetSection(nameof(User)).Get<User>();
                    admin.HashedPassword = await hasher.HashPassword(admin.UserName);
                    admin.RoleId = roles.FirstOrDefault(i => i.LowercaseName == "admin").Id;
                    bool successAdmin = await userRepo.Add(admin);
                    if (!successAdmin)
                        throw new Exception("error - admin");
                }
            }).GetAwaiter().GetResult();
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

        public static IPassword GetPasswordHasher(IConfiguration configuration)
        {
            var security = configuration.GetSection(nameof(SecurityOptions)).Get<SecurityOptions>();
            return new PasswordHasher(security);
        }
    }

    public static class RepositoryFactory
    {
        public static IMovieRepository GetMovieRepo(IConfiguration configuration)
        {
            var databaseOptions = configuration.GetSection(nameof(MovieDatabaseOptions)).Get<MovieDatabaseOptions>();
            return new MovieRepository(databaseOptions);
        }

        public static IRepository<Role> GetRoleRepo(IConfiguration configuration)
        {
            var databaseOptions = configuration.GetSection(nameof(MovieDatabaseOptions)).Get<MovieDatabaseOptions>();
            return new Repository<Role>(databaseOptions);
        }

        public static IRepository<User> GetUserRepo(IConfiguration configuration)
        {
            var databaseOptions = configuration.GetSection(nameof(MovieDatabaseOptions)).Get<MovieDatabaseOptions>();
            return new Repository<User>(databaseOptions);
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
