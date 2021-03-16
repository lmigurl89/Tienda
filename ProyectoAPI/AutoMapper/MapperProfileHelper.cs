using ProyectoAPI.AutoMapper.Interfaces_de_AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ProyectoAPI.AutoMapper
{
    public sealed class Map
    {
        public Type Source { get; set; }
        public Type Destination { get; set; }
    }
    public static class MapperProfileHelper
    {
        public static IList<Map> LoadStandardMappings(Assembly rootAssembly)
        {
            var types = rootAssembly.GetExportedTypes().Where(type => !type.IsAbstract && !type.IsInterface);

            List<Map> mapsFrom = new List<Map>();
            foreach (var type in types)
            {
                var interfaces = type.GetInterfaces().Where(instance => instance.IsGenericType && instance.GetGenericTypeDefinition() == typeof(IMapFrom<>)).ToList();
                mapsFrom.AddRange(interfaces.Select(instance => new Map
                {
                    Source = type.GetInterfaces().First().GetGenericArguments().First(),
                    Destination = type
                }));
            }

            return mapsFrom;
        }

        public static IList<IHaveCustomMapping> LoadCustomMappings(Assembly rootAssembly)
        {
            var types = rootAssembly.GetExportedTypes().Where(type => typeof(IHaveCustomMapping).IsAssignableFrom(type) && !type.IsAbstract && !type.IsInterface);

            List<IHaveCustomMapping> mapsFrom = new List<IHaveCustomMapping>();
            foreach (var type in types)
            {
                var interfaces = type.GetInterfaces().ToList();
                mapsFrom.AddRange(interfaces.Select(instance => (IHaveCustomMapping)Activator.CreateInstance(type)));
            }

             mapsFrom = (
                    from type in types
                    from instance in type.GetInterfaces()
                    where
                        typeof(IHaveCustomMapping).IsAssignableFrom(type) &&
                        !type.IsAbstract &&
                        !type.IsInterface
                    select (IHaveCustomMapping)Activator.CreateInstance(type)).ToList();

            return mapsFrom;
        }
    }
}
