using Microsoft.AspNetCore.Identity;
using ProyectoDATA.AutoMapper.Interfaces_de_AutoMapper;
using ProyectoDATA.Entidades.Mod_Seguridad;
using System.Collections.Generic;

namespace ProyectoDATA.Modelo_de_Datos.Mod_Seguridad
{
    public class RolModel : IdentityRole, IMapFrom<Rol>
    {
        #region Propiedades
        public string Descripcion { get; set; }

        #endregion

        #region Propiedades Navigacionales
        #endregion

        #region Constructor
        public RolModel()
        {
        }
        #endregion
    }
}
