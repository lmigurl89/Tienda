using Microsoft.AspNetCore.Identity;
using ProyectoDATA.AutoMapper.Interfaces_de_AutoMapper;
using ProyectoDATA.Entidades.Mod_Seguridad;
using ProyectoDATA.Modelo_de_Datos.Mod_Tienda;
using System;
using System.Collections.Generic;

namespace ProyectoDATA.Modelo_de_Datos.Mod_Seguridad
{
    public class UsuarioModel: IdentityUser, IMapFrom<Usuario>
    {
        #region Properties
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string NombreCompleto { get => $"{Nombre} {Apellidos}"; }
        public string Password { get; set; }
        #endregion

        #region Propiedades Navigacionales
        public List<Rol_UsuariosModel> ListadoRolUsuarios { get; set; }

        public List<OrdenModel> Ordenes { get; set; }
        #endregion

        public UsuarioModel()
        {
            Ordenes = new List<OrdenModel>();
        }
    }
}
