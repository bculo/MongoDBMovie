using AutoMapper;
using TBP.Contracts.Movie;
using TBP.Entities;

namespace TBP.Configurations.Automapper
{
    public class MovieProfile : Profile
    {
        public MovieProfile()
        {
            CreateMap<Movie, MovieResponseModel>()
                .ForMember(i => i.Id, opt => opt.MapFrom(src => src.Id.ToString()));
            CreateMap<Character, CharacterResponseModel>()
                .ForMember(i => i.Id, opt => opt.MapFrom(src => src.Id.ToString()));
            CreateMap<Genre, GenreResponseModel>()
                .ForMember(i => i.Id, opt => opt.MapFrom(src => src.Id.ToString()));
            CreateMap<Genre, MovieWrapperResponseModel>();
        }
    }
}
