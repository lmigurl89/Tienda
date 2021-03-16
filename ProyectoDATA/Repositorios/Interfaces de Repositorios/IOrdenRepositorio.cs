using ProyectoDATA.Entidades.Mod_Tienda;
using ProyectoDATA.Modelo_de_Datos.Mod_Tienda;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProyectoDATA.Repositorios.Interfaces_de_Repositorios
{
    public interface IOrdenRepositorio : IRepositorio<Orden, OrdenModel>
    {
        Task<OrdenModel> BuscarOrdenProducto(Guid ordenId);
        Task<OrdenModel> Detallar(Guid id);
        Task<List<OrdenModel>> Listar(int paginaActual, int cantidadFilas);
        Task<OrdenModel> ActualizarEstado(Guid ordenId, EstadoOrden estado);
    }
}
