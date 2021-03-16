using System.Collections.Generic;
using System.Globalization;

namespace ProyectoDATA.Entidades.Mod_Tienda
{
    public class Producto : BaseEntity
    {
        #region Campos
        
        private string _Nombre;

        #endregion

        #region Properties
        public string Nombre { get { return _Nombre; } set { _Nombre = CultureInfo.InvariantCulture.TextInfo.ToTitleCase(value.ToLower()).Trim(); } }
        public string Descripcion { get; set; }
        public string Slug { get; set; }
        public double Precio { get; set; }
        public int Cantidad { get; set; }
        public string CreadoPor { get; set; }

        #endregion

        #region Propiedades Navigacionales

        public List<Orden> Ordenes { get; set; }

        #endregion

        #region Metodos
        public Producto()
        {
            Ordenes = new List<Orden>();
        }
        #endregion
    }
}
