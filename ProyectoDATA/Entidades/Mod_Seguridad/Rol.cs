using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text;

namespace ProyectoDATA.Entidades.Mod_Seguridad
{
    public class Rol : IdentityRole
    {
        #region Campos
        #endregion

        #region Propiedades
        public string Descripcion { get; set; }

        #endregion

        #region Propiedades Navigacionales
        #endregion

        #region Constructor
        public Rol()
        {
        }
        #endregion

        #region Public Methods



        #endregion
    }
}
