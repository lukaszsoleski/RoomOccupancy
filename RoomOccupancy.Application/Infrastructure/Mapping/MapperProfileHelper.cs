
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
            var interfaces = new Type[] { typeof(IMapFrom<>), typeof(IMapTo<>) };
            var mappings =
                    from type in types
                    from instance in type.GetInterfaces()
                    where
                        instance.IsGenericType && interfaces.Any(i => i == instance.GetGenericTypeDefinition()) &&
                        !type.IsAbstract &&
                        !type.IsInterface
                    select type;

            foreach(var type in mappings)
            {
                var @interface = type.GetInterfaces()
                    .First(x => interfaces.Any(i => i == x.GetType()));
                var targetType = @interface.GetGenericArguments().First();
                var mapSetting = (@interface == typeof(IMapFrom<>)) ?
                    new Map() { Source = targetType, Destination = type }
                    :
                    new Map() { Source = type, Destination = targetType };
                yield return mapSetting;
            }

        }

        public static IEnumerable<IHaveCustomMapping> LoadCustomMappings(Assembly rootAssembly)
        {
            var types = rootAssembly.GetExportedTypes();

            var mapsFrom = 
                    from type in types
                    from instance in type.GetInterfaces()
                    where
                        typeof(IHaveCustomMapping).IsAssignableFrom(type) &&
                        !type.IsAbstract &&
                        !type.IsInterface
                    select (IHaveCustomMapping)Activator.CreateInstance(type);

            return mapsFrom;
        }
    }
}
