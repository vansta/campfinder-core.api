using AutoMapper;
using CampFinder.Models;
using CampFinder.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace CampFinder.AutoMapperConfiguration
{
    public class BuildingMapProfile: Profile
    {
        public BuildingMapProfile()
        {
            CreateMap<Building, BuildingViewModel>()
                .ForMember(dest => dest.Dormitories, opt => opt.MapFrom(src => src.Dormitories.ToString()))
                .ForMember(dest => dest.AmountPersons, opt => opt.MapFrom(src => src.AmountPersons.ToString()))
                .ForMember(dest => dest.Area, opt => opt.MapFrom(src => src.Area.ToString()));
            //.ForMember(dest => dest.Place, opt => opt.Ignore())
            //.ForMember(dest => dest.Person, opt => opt.Ignore());

            CreateMap<BuildingViewModel, Building>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id == Guid.Empty ? Guid.NewGuid() : src.Id))
                .ForMember(dest => dest.AmountPersons, opt => opt.MapFrom(src => int.Parse(src.AmountPersons)))
                .ForMember(dest => dest.Dormitories, opt => opt.MapFrom(src => int.Parse(src.Dormitories)))
                .ForMember(dest => dest.DaySpaces, opt => opt.MapFrom(src => int.Parse(src.DaySpaces)))
                .ForMember(dest => dest.Area, opt => opt.MapFrom(src => double.Parse(src.Area)));
                //.ForMember(dest => dest.Person, opt => opt.Ignore())
                //.ForMember(dest => dest.Place, opt => opt.Ignore());

            CreateMap<Building, BuildingOverviewItemViewModel>()
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Place.City));
        }
    }
}
