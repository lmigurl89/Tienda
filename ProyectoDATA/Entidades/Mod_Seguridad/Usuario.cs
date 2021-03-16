using Microsoft.AspNetCore.Identity;
using ProyectoDATA.Entidades.Mod_Tienda;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace ProyectoDATA.Entidades.Mod_Seguridad
{
    public class Usuario : IdentityUser
    {
        #region Campos

        private string _Apellidos;
        private string _Nombre;

        #endregion

        #region Properties
        public string Nombre { get { return _Nombre; } set { _Nombre = CultureInfo.InvariantCulture.TextInfo.ToTitleCase(value.ToLower()).Trim(); } }
        public string NombreCompleto { get => $"{Nombre} {Apellidos}"; }
        public string Apellidos { get { return _Apellidos; } set { _Apellidos = CultureInfo.InvariantCulture.TextInfo.ToTitleCase(value.ToLower()).Trim(); } }
        #endregion

        #region Propiedades Navigacionales

        public List<Orden> Ordenes { get; set; }

        #endregion

        #region Metodos
        public Usuario()
        {
            Ordenes = new List<Orden>();            
        }
        #endregion
    }
}
