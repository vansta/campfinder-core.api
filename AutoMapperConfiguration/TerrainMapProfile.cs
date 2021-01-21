using AutoMapper;
using CampFinder.Models;
using CampFinder.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace CampFinder.AutoMapperConfiguration
{
    public class TerrainMapProfile: Profile
    {
        public TerrainMapProfile()
        {
            CreateMap<Terrain, TerrainViewModel>()
                .ForMember(dest => dest.AmountPersons, opt => opt.MapFrom(src => src.AmountPersons.ToString()))
                .ForMember(dest => dest.Area, opt => opt.MapFrom(src => src.Area.ToString()));
                //.ForMember(dest => dest.Place, opt => opt.Ignore())
                //.ForMember(dest => dest.Person, opt => opt.Ignore());

            CreateMap<TerrainViewModel, Terrain>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id == Guid.Empty ? Guid.NewGuid() : src.Id))
                .ForMember(dest => dest.AmountPersons, opt => opt.MapFrom(src => int.Parse(src.AmountPersons)))
                .ForMember(dest => dest.Area, opt => opt.MapFrom(src => int.Parse(src.Area)));
                //.ForMember(dest => dest.Place, opt => opt.Ignore())
                //.ForMember(dest => dest.Person, opt => opt.Ignore());

            CreateMap<Terrain, TerrainOverviewItemViewModel>()
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Place.City));
        }
    }
}
