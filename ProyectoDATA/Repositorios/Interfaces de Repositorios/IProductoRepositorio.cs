using ProyectoDATA.Entidades.Mod_Tienda;
using ProyectoDATA.Modelo_de_Datos.Mod_Tienda;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProyectoDATA.Repositorios.Interfaces_de_Repositorios
{
    public interface IProductoRepositorio : IRepositorio<Producto, ProductoModel>
    {
        Task<List<ProductoModel>> Listar(int paginaActual, int cantidadFilas);
    }
}
