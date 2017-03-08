using AutoMapper;
using HouseholdManager.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace HouseholdManager.Web.App_Start
{
    public class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            var types = Assembly.GetExecutingAssembly().GetExportedTypes();
            var viewModelTypes = Assembly.Load("HouseholdManager.Domain.Models").GetExportedTypes();

            LoadStandardMappings(types);
            LoadStandardMappings(viewModelTypes);

            LoadCustomMappings(types);
            LoadCustomMappings(viewModelTypes);
        }

        private static void LoadStandardMappings(IEnumerable<Type> types)
        {
            var maps = (from t in types
                        from i in t.GetInterfaces()
                        where i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapFrom<>) &&
                              !t.IsAbstract &&
                              !t.IsInterface
                        select new
                        {
                            Source = i.GetGenericArguments()[0],
                            Destination = t
                        }).ToArray();

            foreach (var map in maps)
            {
                Mapper.Initialize(config =>
                {
                    config.CreateMap(map.Source, map.Destination);
                    config.CreateMap(map.Destination, map.Source);
                });

                //Mapper.Initialize(config => config.CreateMap(map.Destination, map.Source));

                //Mapper.CreateMap(map.Source, map.Destination);
                //Mapper.CreateMap(map.Destination, map.Source);
            }
        }

        private static void LoadCustomMappings(IEnumerable<Type> types)
        {
            var maps = (from t in types
                        from i in t.GetInterfaces()
                        where typeof(IHaveCustomMappings).IsAssignableFrom(t) &&
                              !t.IsAbstract &&
                              !t.IsInterface
                        select (IHaveCustomMappings)Activator.CreateInstance(t)).ToArray();

            foreach (var map in maps)
            {
                map.CreateMappings(Mapper.Configuration);
            }
        }
    }
}