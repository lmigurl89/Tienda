using ProyectoAPI.AutoMapper.Interfaces_de_AutoMapper;
using ProyectoDATA.Modelo_de_Datos.Mod_Tienda;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProyectoAPI.Modelos_para_Vistas.Mod_Tienda
{
    public class ProductoViewModel :  IMapFrom<ProductoModel>
    {

        #region Properties
        public Guid Id { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Nombre { get; set; }

        [Display(Name ="Descipción")]
        [Required(ErrorMessage ="El campo {0} es obligatorio")]
        [MaxLength(100, ErrorMessage ="EL campo {0} solo puede contener hasta {1} caracteres")]
        public string Descripcion { get; set; }

        public string Slug { get; set; }

        [Required(ErrorMessage ="El campo {0} es obligatorio")]
        public double Precio { get; set; }

        [Required(ErrorMessage ="El campo {0} es obligatorio")]
        public int Cantidad { get; set; }

        #endregion        

    }
}
