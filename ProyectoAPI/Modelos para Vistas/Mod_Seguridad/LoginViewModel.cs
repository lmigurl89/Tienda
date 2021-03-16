using System.ComponentModel.DataAnnotations;

namespace ProyectoAPI.Modelos_para_Vistas.Mod_Seguridad
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [StringLength(100, ErrorMessage ="El campo {0} debe contener como máximo {1} caracteres ")]
        [Display(Name ="Usuario")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [DataType(DataType.Password)]
        [Display(Name ="Contraseña")]
        public string Password { get; set; }

    }
}
