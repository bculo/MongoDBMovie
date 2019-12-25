using AutoMapper;
using TBP.Contracts;
using TBP.Contracts.Authentication;
using TBP.Services.Result;

namespace TBP.Configurations.Automapper
{
    public class AuthenticationProfile : Profile
    {
        public AuthenticationProfile()
        {
            CreateMap<AuthLoginResult, AuthLoginResponseModel>();
            CreateMap<AuthLoginResult, ErrorResponseModel>();
            CreateMap<ServiceResult, ErrorResponseModel>();
        }
    }
}
