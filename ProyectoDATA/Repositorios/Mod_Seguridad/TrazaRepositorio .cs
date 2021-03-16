using AutoMapper;
using ProyectoDATA.DBContext;
using ProyectoDATA.Entidades.Mod_Seguridad;
using ProyectoDATA.Modelo_de_Datos.Mod_Seguridad;
using ProyectoDATA.Repositorios.Interfaces_de_Repositorios;

namespace ProyectoDATA.Repositorios.Mod_Seguridad
{
    public class TrazaRepositorio : Repositorio<Traza,TrazaModel>, ITrazaRepositorio
    {
        public TrazaRepositorio(ApplicationDbContext context, IMapper mapper):base(context, mapper)
        {
            
        }
        
    }
}
