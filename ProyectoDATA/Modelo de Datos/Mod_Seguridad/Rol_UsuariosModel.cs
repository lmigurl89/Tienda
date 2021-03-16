using Microsoft.AspNetCore.Identity;
using ProyectoDATA.AutoMapper.Interfaces_de_AutoMapper;
using ProyectoDATA.Entidades.Mod_Seguridad;

namespace ProyectoDATA.Modelo_de_Datos.Mod_Seguridad
{
    public class Rol_UsuariosModel: IdentityUserRole<string>, IMapFrom<Rol_Usuarios>
    {
        #region Propiedades Navigacionales
       
        #endregion
    }
}