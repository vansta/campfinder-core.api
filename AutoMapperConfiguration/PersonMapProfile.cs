using AutoMapper;
using CampFinder.Models;
using CampFinder.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace CampFinder.AutoMapperConfiguration
{
    public class PersonMapProfile: Profile
    {
        public PersonMapProfile()
        {
            CreateMap<Person, PersonViewModel>();

            CreateMap<PersonViewModel, Person>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()));
        }
    }
}
