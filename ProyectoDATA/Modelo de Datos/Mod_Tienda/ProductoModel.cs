using ProyectoDATA.AutoMapper.Interfaces_de_AutoMapper;
using ProyectoDATA.Entidades.Mod_Tienda;
using System.Collections.Generic;

namespace ProyectoDATA.Modelo_de_Datos.Mod_Tienda
{
    public class ProductoModel : BaseModel, IMapFrom<Producto>
    { 

        #region Properties
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Slug { get; set; }
        public double Precio { get; set; }
        public int Cantidad { get; set; }
        public string CreadoPor { get; set; }

        #endregion

        #region Propiedades Navigacionales

        public List<OrdenModel> Ordenes { get; set; }

        #endregion

    }
}
