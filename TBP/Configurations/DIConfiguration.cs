using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TBP.Configurations.Automapper;
using TBP.Contracts.Authentication;
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
                mc.AddProfile(new AuthenticationProfile());
            });
            services.AddSingleton(mappingConfig.CreateMapper());

            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient<ITokenManager, TokenManager>();
            services.AddTransient<IPassword, PasswordHasher>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IUserRepository, UserRepository>();

            services.AddTransient<IValidator<AuthLoginRequestModel>, LoginValidator>();
            services.AddTransient<IValidator<AuthRegistrationRequestModel>, RegistrationValidator>();
        }
    }
}
