using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using TBP.Clients;
using TBP.Contracts.Authentication;
using TBP.Contracts.Movie;
using TBP.Interfaces;
using TBP.Repository;
using TBP.Services;

namespace TBP.Configurations
{
    public class DIConfiguration : IInstaller
    {
        public void Configure(IServiceCollection services, IConfiguration configuration)
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfiles(GetProfiles());
            });
            services.AddSingleton(mappingConfig.CreateMapper());
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<ITokenManager, TokenManager>();
            services.AddScoped<IPassword, PasswordHasher>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IMovieRepository, MovieRepository>();
            services.AddScoped<IMovieService, MovieService>();
            services.AddScoped<IGenreRepository, GenreRepository>();
            services.AddScoped<IValidator<AuthLoginRequestModel>, LoginValidator>();
            services.AddScoped<IValidator<AuthRegistrationRequestModel>, RegistrationValidator>();
            services.AddScoped<IValidator<MoviePaginationRequestModel>, ActorValidator>();
            services.AddScoped<IValidator<MovieRequestModel>, GenreValidator>();
            services.AddHttpClient<IMovieClient, MovieClient>();
        }

        private IEnumerable<Profile> GetProfiles()
        {
            var profiles = typeof(Startup).Assembly.GetExportedTypes()
                .Where(i => !i.IsInterface && !i.IsAbstract && typeof(Profile).IsAssignableFrom(i))
                .Select(Activator.CreateInstance)
                .Cast<Profile>()
                .ToList();

            return profiles;
        }
    }
}
