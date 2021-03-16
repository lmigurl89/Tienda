using ProyectoDATA.AutoMapper.Interfaces_de_AutoMapper;
using ProyectoDATA.Entidades.Mod_Seguridad;

namespace ProyectoDATA.Modelo_de_Datos.Mod_Seguridad
{
    public class TrazaModel : BaseModel, IMapFrom<Traza>
    {
        #region Propiedades
        public string NombreAccion { get; set; }
        public string UserName { get; set; }
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
