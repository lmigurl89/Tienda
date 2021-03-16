using ProyectoAPI.Modelos_para_Vistas.Mod_Seguridad;
using ProyectoAPI.AutoMapper.Interfaces_de_AutoMapper;
using ProyectoDATA.Entidades.Mod_Tienda;
using ProyectoDATA.Modelo_de_Datos.Mod_Tienda;
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace ProyectoAPI.Modelos_para_Vistas.Mod_Tienda
{
    public class ActualizarOrdenViewModel :  IMapFrom<OrdenModel>, IValidatableObject

    {

        #region Properties
        public Guid Id { get; set; }

        [Required(ErrorMessage ="El campo {0} es obligatorio")]
        public EstadoOrden Estado { get; set; }

        #endregion

        #region Propiedades Navigacionales
        #endregion

        #region Metodos
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errores = new List<ValidationResult>();
            if (!Estado.Equals(EstadoOrden.Created) && !Estado.Equals(EstadoOrden.Confirmed) && !Estado.Equals(EstadoOrden.Canceled))
                errores.Add(new ValidationResult("Estado de la orden incorrecto", new List<string> { nameof(ActualizarOrdenViewModel.Estado) }));

            return errores;
        }
        #endregion

    }
}
