using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProyectoDATA.DBContext;
using ProyectoDATA.Entidades.Mod_Seguridad;
using ProyectoDATA.Entidades.Mod_Tienda;
using ProyectoDATA.Modelo_de_Datos.Mod_Seguridad;
using ProyectoDATA.Modelo_de_Datos.Mod_Tienda;
using ProyectoDATA.Repositorios.Interfaces_de_Repositorios;
using ProyectoDATA.Repositorios.Mod_Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace ProyectoDATA.Repositorios.Mod_Tienda
{
    public class ProductoRepositorio : Repositorio<Producto, ProductoModel>, IProductoRepositorio
    {
        public ProductoRepositorio(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {

        }

        public async Task<List<ProductoModel>> Listar(int paginaActual, int cantidadFilas)
        {
            //este metodo devuelve la cantidad de productos especificada de forma paginada
            return await _context.Productos.Select(producto=> new ProductoModel { Id=producto.Id, Nombre = producto.Nombre, Cantidad = producto.Cantidad, Descripcion = producto.Descripcion, Precio=producto.Precio, Slug = producto.Slug, FechaCreado=producto.FechaCreado}).OrderBy(producto => producto.FechaCreado).Skip((paginaActual - 1) * cantidadFilas).Take(cantidadFilas).ToListAsync();
        }        
    }
}
