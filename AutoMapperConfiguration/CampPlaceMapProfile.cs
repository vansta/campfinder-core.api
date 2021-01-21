using AutoMapper;
using CampFinder.Models;
using CampFinder.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace CampFinder.AutoMapperConfiguration
{
    public class CampPlaceMapProfile : Profile
    {
        public CampPlaceMapProfile()
        {
            CreateMap<CampPlace, CampPlaceViewModel>()
                .ForMember(dest => dest.AmountPersons, opt => opt.MapFrom(src => src.AmountPersons.ToString()))
                .ForMember(dest => dest.Area, opt => opt.MapFrom(src => src.Area.ToString()));
        }
    }
}
