﻿using AutoMapper;
using CampFinder.Models;
using CampFinder.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace CampFinder.AutoMapperConfiguration
{
    public class PlaceMapProfile: Profile
    {
        public PlaceMapProfile()
        {
            CreateMap<PlaceViewModel, Place>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()));

            CreateMap<Place, PlaceViewModel>();
        }
    }
}
