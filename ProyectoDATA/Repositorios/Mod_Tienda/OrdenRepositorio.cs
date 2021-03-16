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
    public class OrdenRepositorio : Repositorio<Orden, OrdenModel>, IOrdenRepositorio
    {
        public OrdenRepositorio(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
        {

        }

        public override async Task<OrdenModel> Detallar(Guid id)
        {
            //este metodo devuelve los datos de la orden seleccionada
            return _mapper.Map<OrdenModel>(await _context.Ordenes.Include(orden => orden.Usuario).Include(orden => orden.Producto).SingleOrDefaultAsync(orden => orden.Id == id));
        }

        public override async Task<List<OrdenModel>> Listar(int paginaActual, int cantidadFilas)
        {
            //este metodo devuelve la cantidad de ordenes especificada de forma paginada
            return await _context.Ordenes.Include(orden=> orden.Usuario).Include(orden=> orden.Producto).Select(orden => new OrdenModel { Id = orden.Id, Estado = orden.Estado, Cantidad = orden.Cantidad, Fecha = orden.Fecha, FechaCreado = orden.FechaCreado, Usuario = new UsuarioModel { Nombre = orden.Usuario.Nombre }, Producto = new ProductoModel { Nombre = orden.Producto.Nombre } }).OrderBy(producto => producto.FechaCreado).Skip((paginaActual - 1) * cantidadFilas).Take(cantidadFilas).ToListAsync();
        }

        public async Task<OrdenModel> BuscarOrdenProducto(Guid ordenId)
        {
            //este metodo devuelve el rol del usuario seleccionado
            return _mapper.Map<OrdenModel>((await _context.Ordenes.Include(orden => orden.Producto).SingleOrDefaultAsync(orden => orden.Id == ordenId)));
        }
        
        public async Task<OrdenModel> ActualizarEstado(Guid ordenId, EstadoOrden estado)
        {
            //este metodo devuelve el rol del usuario seleccionado
            Orden orden = await _context.Ordenes.Include(orden => orden.Producto).SingleOrDefaultAsync(orden => orden.Id == ordenId);
            orden.Estado = estado;
            return _mapper.Map<OrdenModel>(orden);
        }
        
    }
}
