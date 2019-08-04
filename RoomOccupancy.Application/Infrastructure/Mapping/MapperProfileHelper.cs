
using AutoMapper;
using RoomOccupancy.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace RoomOccupancy.Application.Infrastructure.Mapping
{
    public sealed class Map
    {
        public Type Source { get; set; }
        public Type Destination { get; set; }
    }

    public static class MapperProfileHelper
    {
        public static IEnumerable<Map> LoadStandardMappings(Assembly rootAssembly)
        {
            var types = rootAssembly.GetExportedTypes();
            var markerTypes = new Type[] { typeof(IMapFrom<>), typeof(IMapTo<>) };

            var mappings =
                    from type in types
                    from inteface in type.GetInterfaces()
                    where
                        inteface.IsGenericType && markerTypes.Any(i => i == inteface.GetGenericTypeDefinition()) &&
                        !type.IsAbstract &&
                        !type.IsInterface
                    select type;

            var settings = mappings.Select(type => {
            
                var intefaces = type.GetInterfaces();

                var marker = intefaces.First(x => markerTypes.Any(i => i.Name == x.Name));

                var targetType = marker.GetGenericArguments().First();
                var mapSetting = (marker == typeof(IMapFrom<>)) ?
                    new Map() { Source = targetType, Destination = type }
                    :
                    new Map() { Source = type, Destination = targetType };
                return mapSetting;
            });
            return settings;
        }

        public static IEnumerable<IHaveCustomMapping> LoadCustomMappings(Assembly rootAssembly)
        {
            var types = rootAssembly.GetExportedTypes()
                .Where(x => typeof(IHaveCustomMapping).IsAssignableFrom(x) 
                    && !x.IsAbstract
                    && !x.IsInterface
            );

            var mapSettings = types.Select(x => Activator.CreateInstance(x) as IHaveCustomMapping);

            return mapSettings; 
        }

      
    }
}
