using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ProyectoAPI.Modelos_para_Vistas;
using ProyectoAPI.Modelos_para_Vistas.Mod_Tienda;
using ProyectoDATA.Entidades.Mod_Tienda;
using ProyectoDATA.Modelo_de_Datos.Mod_Tienda;
using ProyectoDATA.Repositorios.Interfaces_de_Repositorios;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProyectoAPI.Controladores.Mod_Seguridad
{
    [Route("api/[controller]")]
    [EnableCors("AllowAnyCorsPolicy")]
    [ApiController]
    [Authorize("Administrador")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class OrdenesController : BaseController
    {
        public OrdenesController(IUnitOfWork context, IMapper mapper, IConfiguration configuration, ILogger<OrdenesController> logger) : base(context, mapper, configuration, logger)
        {
        }

        // GET: api/Ordenes/
        [HttpGet]
        public async Task<ActionResult<List<OrdenViewModel>>> Listar(int paginaActual = 1, int cantidadFilas = 50)
        {
            /*DEVUELVE EL LISTADO DE ORDENES DE FORMA PAGINADA*/
            try
            {
                //obteniendo ordenes segun el filtrado                
                return Ok(await _context.Ordenes.Listar(paginaActual, cantidadFilas));

            }
            catch (Exception error)
            {
                _logger.LogError(error, "Error al listar ordenes"); //escribiendo el error en el log
                return BadRequest(new ErrorViewModel { Titulo = "Error al listar ordenes", Mensaje = $"{error.Message}{error.InnerException?.Message}" });
            }
        }

        // GET: api/Ordenes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrdenViewModel>> VerDetalles(Guid id)
        {
            /*DEVUELVE LA ORDEN SELECCIONADA CON TODOS SUS DATOS*/
            try
            {
                OrdenModel orden = await _context.Ordenes.Detallar(id);

                if (orden == null)
                    return NotFound("La orden seleccionado ya no se encuentra en la Base de Datos.");
                return _mapper.Map<OrdenViewModel>(orden);
            }
            catch (Exception error)
            {
                _logger.LogError(error, "Error al ver detalles de orden"); //escribiendo el error en el log
                return BadRequest(new ErrorViewModel { Titulo = "Error al ver detalles de orden", Mensaje = $"{error.Message}{error.InnerException?.Message}" });
            }
        }


        // PUT: api/Ordenes/
        [HttpPut("{id}")]
        public async Task<ActionResult> Actualizar(Guid id, [FromBody] ActualizarOrdenViewModel orden)
        {
            /*ACTUALIZA LOS DATOS DE LA ORDEN SELECCIONADO*/
            try
            {
                if (id != orden.Id)
                    return BadRequest();

                //verificando que aun exista el orden 
                OrdenModel ordenOriginal = await _context.Ordenes.Buscar(orden.Id, false);

                if (ordenOriginal == null)
                    return NotFound("La orden seleccionada ya no se encuentra en la Base de Datos.");

                //actualizando estado de la orden
                await _context.Ordenes.ActualizarEstado(orden.Id, orden.Estado);

                // guardandolo en su estado original para las trazas
                await CrearTraza("ActualizarOrden", $"Se ha actualizado la orden con Id: {orden.Id}", objetoOriginal: ordenOriginal, objetoModificado: orden);

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException error)
                {
                    _logger.LogError(error, "Error al actualizar orden"); //escribiendo el error en el log

                    if (!await OrdenExiste(id))
                        return NotFound("La orden seleccionado ya no se encuentra en la Base de Datos.");
                    else
                        return BadRequest(new ErrorViewModel { Titulo = "Error al actualizar orden", Mensaje = $"{error.Message}{error.InnerException?.Message}" });
                }
                catch (DbUpdateException error)
                {
                    _logger.LogError(error, "Error al actualizar orden"); //escribiendo el error en el log

                    if (error.InnerException.Message.Contains("Duplicate entry"))
                    {
                        if (error.InnerException.Message.Contains("for key 'PRIMARY'"))
                            return BadRequest(new ErrorViewModel { Titulo = "Error al actualizar orden", Mensaje = $"El id debe ser único y '{orden.Id}' ya está siendo usado." });
                    }
                    return BadRequest(new ErrorViewModel { Titulo = "Error al actualizar orden", Mensaje = $"{error.Message}{error.InnerException?.Message}" });
                }

                return Ok();
            }
            catch (Exception error)
            {
                _logger.LogError(error, "Error al actualizar orden"); //escribiendo el error en el log
                return BadRequest(new ErrorViewModel { Titulo = "Error al actualizar orden", Mensaje = $"{error.Message}{error.InnerException?.Message}" });
            }
        }

        // DELETE: api/Ordenes/5
        [HttpDelete("{id}")]
        [Authorize("Administrador")]
        [Authorize("Vendedor")]
        public async Task<ActionResult> Eliminar(Guid id)
        {
            /*ELIMINA LA ORDEN SELECCIONADO DE LA BASE DE DATOS*/
            try
            {
                OrdenModel ordenEliminada = await _context.Ordenes.Buscar(id, false);
                if (ordenEliminada == null)
                    return NotFound("La orden seleccionado ya no se encuentra en la Base de Datos.");

                //verificando la orden no esta confirmada
                if (ordenEliminada.Estado == EstadoOrden.Created)
                {
                    //aumentando la cantidad del producto
                    ProductoModel producto = await _context.Productos.Buscar(ordenEliminada.ProductoId, false);
                    producto.Cantidad = producto.Cantidad + ordenEliminada.Cantidad;
                    _context.Productos.Actualizar(producto);

                    _context.Ordenes.Eliminar(ordenEliminada);
                    await CrearTraza("EliminarOrden", $"Se ha eliminado la orden con Id: {ordenEliminada.Id}", objetoEliminado: ordenEliminada);
                    await _context.SaveChangesAsync();
                    return Ok();
                }
                return BadRequest(new ErrorViewModel { Titulo = "Error al eliminar la orden", Mensaje = $"Solo se pueden eliminar ordenes que no han sido confirmadas" });
            }
            catch (DbUpdateException error)
            {
                _logger.LogError(error, "Error al eliminar la orden"); //escribiendo el error en el log

                if (error.InnerException.Message.Contains("Cannot delete or update a parent row: a foreign key constraint fails"))
                {
                    return BadRequest(new ErrorViewModel { Titulo = "Error al eliminar la orden", Mensaje = $"Este orden esta siendo usado por otros objetos y no puede ser eliminado" });
                }
                else
                    return BadRequest(new ErrorViewModel { Titulo = "Error al eliminar la orden", Mensaje = $"{error.Message}{error.InnerException?.Message}" });
            }
            catch (Exception error)
            {
                _logger.LogError(error, "Error al eliminar orden"); //escribiendo el error en el log
                return BadRequest(new ErrorViewModel { Titulo = "Error al eliminar la orden", Mensaje = $"{error.Message}{error.InnerException?.Message}" });
            }
        }

        private async Task<bool> OrdenExiste(Guid id)
        {
            /*VERIFICA QUE LA ORDEN EXISTA EN LA BASE DE DATOS*/
            return await _context.Ordenes.Existe(id);
        }

    }
}
