using AutoMapper;
using NZwalks.API.Models.Domain;
using NZwalks.API.Models.DTO;

namespace NZwalks.API.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            //Region Mapper
            CreateMap<Region, RegionDto>().ReverseMap();
            CreateMap<AddRegionRequestDto,Region>().ReverseMap();
            CreateMap<UpdateRegionRequestDto, Region>().ReverseMap();

            //Walk Mapper
            CreateMap<AddWalksRequestDto,Walk>().ReverseMap();
            CreateMap<UpdateWalkRequestDto,Walk>().ReverseMap();
            CreateMap<Walk,WalkDto>().ReverseMap();

            //Difficulty Mapper
            CreateMap<Difficulty, DifficultyDto>().ReverseMap();
        }
    }
}
