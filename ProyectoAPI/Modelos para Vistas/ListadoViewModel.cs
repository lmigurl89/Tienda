using System.Collections.Generic;

namespace ProyectoAPI.Modelos_para_Vistas
{
    public class ListadoViewModel<T>
    {
        public List<T> Listado { get; set; }
        public int PaginaActual { get; set; }
        public int CantidadTotal { get; set; }
    }
}
