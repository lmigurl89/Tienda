using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoDATA.Entidades.Mod_Seguridad
{
    public class Traza : BaseEntity
    {
        #region Propiedades
        public string UserName { get; set; }
        public string NombreAccion { get; set; }
        public string NombrePC { get; set; }
        public string Descripcion { get; set; }
        public string ObjetoCreado { get; set; }
        public string ObjetoAntesModificar { get; set; }
        public string ObjetoModificado { get; set; }
        public string ObjetoEliminado { get; set; }
        #endregion

        #region Propiedades Navigacionales
        #endregion
    }
}
