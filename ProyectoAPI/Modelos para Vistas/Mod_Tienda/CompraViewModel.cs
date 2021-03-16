using System;
using System.ComponentModel.DataAnnotations;

namespace ProyectoAPI.Modelos_para_Vistas.Mod_Tienda
{
    public class CompraViewModel 
    {

        #region Properties

        [Required(ErrorMessage ="El campo {0} es obligatorio")]
        public int Cantidad { get; set; }


        [Display(Name ="Producto")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public Guid ProductoId { get; set; }

        #endregion

        #region Propiedades Navigacionales

        #endregion

    }
}
