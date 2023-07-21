using AutoMapper;
using NZWalksAPI.Models.Domains;
using NZWalksAPI.Models.DTO;

namespace NZWalksAPI.Mappings
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {

            CreateMap<Region, RegionDto>().ReverseMap();
            CreateMap<AddRegionDto, Region>().ReverseMap();
            CreateMap<UpdateRegionDto, Region>().ReverseMap();
            CreateMap<AddWalkDto, Walk>().ReverseMap();
            CreateMap<Walk, WalkDto>().ReverseMap();
            CreateMap<Difficulty, DifficultyDto>().ReverseMap();
            CreateMap<UpdateWalkDto, Walk>().ReverseMap();



        }
    }
}
