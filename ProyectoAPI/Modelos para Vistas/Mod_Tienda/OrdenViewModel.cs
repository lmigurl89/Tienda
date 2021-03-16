using ProyectoAPI.Modelos_para_Vistas.Mod_Seguridad;
using ProyectoAPI.AutoMapper.Interfaces_de_AutoMapper;
using ProyectoDATA.Entidades.Mod_Tienda;
using ProyectoDATA.Modelo_de_Datos.Mod_Tienda;
using System;
using System.ComponentModel.DataAnnotations;

namespace ProyectoAPI.Modelos_para_Vistas.Mod_Tienda
{
    public class OrdenViewModel :  IMapFrom<OrdenModel>
    {

        #region Properties
        public Guid Id { get; set; }

        [Required(ErrorMessage ="El campo {0} es obligatorio")]
        public DateTime Fecha { get; set; }

        [Required(ErrorMessage ="El campo {0} es obligatorio")]
        public int Cantidad { get; set; }

        [Required(ErrorMessage ="El campo {0} es obligatorio")]
        public EstadoOrden Estado { get; set; }

        [Display(Name ="Usuario")]
        [Required(ErrorMessage ="El campo {0} es obligatorio")]
        public string UsuarioId { get; set; }

        [Display(Name ="Producto")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public Guid ProductoId { get; set; }

        #endregion

        #region Propiedades Navigacionales
        public ProductoViewModel Producto { get; set; }

        #endregion

    }
}
