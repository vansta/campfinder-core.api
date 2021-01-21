using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;

namespace CampFinder.AutoMapperConfiguration
{
    public class MapperService
    {
        private readonly static MapperConfiguration mapperConfiguration;
        static MapperService()
        {
            mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddMaps(new[]
                {
                    typeof(ReviewMapProfile),
                    typeof(BuildingMapProfile),
                    typeof(TerrainMapProfile),
                    typeof(PlaceMapProfile),
                    typeof(PersonMapProfile),
                    typeof(CampPlaceMapProfile)
                });
            });
        }

        public static Mapper Initialize()
        {
            return new Mapper(mapperConfiguration);
        }

        //public T Map(object src)
        //{
        //    return new Mapper(mapperConfiguration).Map<T>(src);
        //}
    }
}
