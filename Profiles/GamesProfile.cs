using AutoMapper;
using Set.Dtos;
using Set.Models;

namespace Set.Profiles
{
    public class GamesProfile : Profile {

        public GamesProfile()
        {
            //  Source      -> Target
            CreateMap<Game, GameReadDto>();
            CreateMap<GameCreateDto, Game>();
            //CreateMap<SetTryDto, SetTry>();
        }
    }


}