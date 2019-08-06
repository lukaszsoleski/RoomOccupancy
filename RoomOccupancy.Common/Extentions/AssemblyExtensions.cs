using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace RoomOccupancy.Common.Extentions
{
    public static class AssemblyExtensions
    {
        public static IEnumerable<T> ActivateInstances<T> (this Assembly rootAssembly) where T : class
        {
            if (!typeof(T).IsInterface)
                throw new ArgumentException("The type must be an interface");
            var types = rootAssembly.GetExportedTypes()
                .Where(x => typeof(T).IsAssignableFrom(x)
                    && !x.IsAbstract
                    && !x.IsInterface
            );
            return types.Select(x => Activator.CreateInstance(x) as T);
        }
    }
}
