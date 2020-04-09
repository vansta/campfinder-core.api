using System;
using AutoMapper;
using CampFinder.ViewModels;
using CampFinder.Models;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CampFinder.AutoMapperConfiguration
{
    public class ReviewMapProfile: Profile
    {
        public ReviewMapProfile()
        {
            CreateMap<ReviewViewModel, Review>();

            CreateMap<Review, ReviewViewModel>();

            CreateMap<List<Review>, List<ReviewViewModel>>();
        }
    }
}
