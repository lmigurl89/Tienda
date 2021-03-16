using ProyectoDATA.Entidades.Mod_Seguridad;
using System;

namespace ProyectoDATA.Entidades.Mod_Tienda
{
    public class Orden : BaseEntity
    {
        #region Campos


        #endregion

        #region Properties
        public DateTime Fecha { get; set; }
        public int Cantidad { get; set; }
        public EstadoOrden Estado { get; set; }
        public string UsuarioId { get; set; }
        public Guid ProductoId { get; set; }

        #endregion

        #region Propiedades Navigacionales
        public Usuario Usuario { get; set; }
        public Producto Producto { get; set; }

        #endregion

        #region Metodos

        #endregion
    }
}
