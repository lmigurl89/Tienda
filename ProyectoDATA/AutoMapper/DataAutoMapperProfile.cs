using AutoMapper;
using System.Reflection;

namespace ProyectoDATA.AutoMapper
{
    public class DataAutoMapperProfile : Profile
    {
        public DataAutoMapperProfile()
        {
            LoadStandardMappings();
            LoadCustomMappings();
            LoadConverters();
        }

        private void LoadConverters()
        {

        }

        private void LoadStandardMappings()
        {
            var mapsFrom = MapperProfileHelper.LoadStandardMappings(Assembly.GetExecutingAssembly());
            foreach (var map in mapsFrom)
            {
                CreateMap(map.Source, map.Destination).ReverseMap();
            }
        }
        private void LoadCustomMappings()
        {
            var mapsFrom = MapperProfileHelper.LoadCustomMappings(Assembly.GetExecutingAssembly());
            foreach (var map in mapsFrom)
            {
                map.CreateMapping(this);
            }
        }
    }

}
