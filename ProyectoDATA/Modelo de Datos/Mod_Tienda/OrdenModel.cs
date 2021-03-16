using ProyectoDATA.AutoMapper.Interfaces_de_AutoMapper;
using ProyectoDATA.Entidades.Mod_Tienda;
using ProyectoDATA.Modelo_de_Datos.Mod_Seguridad;
using System;

namespace ProyectoDATA.Modelo_de_Datos.Mod_Tienda
{
    public class OrdenModel : BaseModel, IMapFrom<Orden>
    {   

        #region Properties
        public DateTime Fecha { get; set; }
        public int Cantidad { get; set; }
        public EstadoOrden Estado { get; set; }
        public string UsuarioId { get; set; }
        public Guid ProductoId { get; set; }

        #endregion

        #region Propiedades Navigacionales
        public UsuarioModel Usuario { get; set; }
        public ProductoModel Producto { get; set; }

        #endregion

    }
}
