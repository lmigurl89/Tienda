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
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using ProyectoDATA.Entidades.Mod_Seguridad;

namespace ProyectoAPI.Controladores.Mod_Seguridad
{
    [Route("api/[controller]")]
    [EnableCors("AllowAnyCorsPolicy")]
    [ApiController]    
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ProductosController : BaseController
    {
        private readonly UserManager<Usuario> _userManager;
        public ProductosController(IUnitOfWork context, IMapper mapper, IConfiguration configuration, ILogger<ProductosController> logger, UserManager<Usuario> userManager) : base(context, mapper, configuration, logger)
        {
            _userManager = userManager;

        }

        // GET: api/Productos/
        [AllowAnonymous]
        [HttpGet]        
        public async Task<ActionResult<List<ProductoViewModel>>> Listar(int paginaActual = 1, int cantidadFilas = 50)
        {
            /*DEVUELVE EL LISTADO DE PRODUCTOS DISPONIBLES DE FORMA PAGINADA*/
            try
            {
                //obteniendo productos segun el filtrado                
                return _mapper.Map<List<ProductoViewModel>>( await _context.Productos.Listar(paginaActual, cantidadFilas));
                
            }
            catch (Exception error)
            {
                _logger.LogError(error, "Error al listar productos"); //escribiendo el error en el log
                return BadRequest(new ErrorViewModel { Titulo = "Error al listar productos", Mensaje = $"{error.Message}{error.InnerException?.Message}" });
            }
        }

        // GET: api/Productos/5
        [HttpGet("{id}")]        
        [AllowAnonymous]
        public async Task<ActionResult<ProductoViewModel>> VerDetalles(Guid id)
        {
            /*DEVUELVE EL PRODUCTO SELECCIONADO CON TODOS SUS DATOS*/
            try
            {
                ProductoModel producto = await _context.Productos.Detallar(id);

                if (producto == null)
                    return NotFound("El producto seleccionado ya no se encuentra en la Base de Datos.");
                return _mapper.Map<ProductoViewModel>(producto);
            }
            catch (Exception error)
            {
                _logger.LogError(error, "Error al ver detalles de producto"); //escribiendo el error en el log
                return BadRequest(new ErrorViewModel { Titulo = "Error al ver detalles de producto", Mensaje = $"{error.Message}{error.InnerException?.Message}" });
            }
        }

        // POST: api/Productos
        [HttpPost]
        [Authorize("Administrador")]
        [Authorize("Vendedor")]
        public async Task<ActionResult> Adicionar([FromBody] ProductoViewModel producto)
        {
            /*AGREGA UN NUEVO PRODUCTO A LA BASE DE DATOS*/
            try
            {
                ProductoModel producto1 = _mapper.Map<ProductoModel>(producto);
                //creando producto
                producto1.CreadoPor = User.Identity.Name;
                ProductoModel productoCreado = await _context.Productos.Adicionar(producto1);
                await CrearTraza("AdicionarProducto", $"Se ha creado el producto {productoCreado.Nombre} con Id: {productoCreado.Id}", objetoCreado: productoCreado);
                await _context.SaveChangesAsync();

                return Ok();
            }

            catch (DbUpdateException error)
            {
                _logger.LogError(error, "Error al adicionar producto"); //escribiendo el error en el log

                if (error.InnerException.Message.Contains("Duplicate entry"))
                {
                    if (error.InnerException.Message.Contains($"{nameof(producto.Nombre)}_UNIQUE"))
                        return BadRequest(new ErrorViewModel { Titulo = "Error al adicionar producto", Mensaje = $"El nombre del producto debe ser único y '{producto.Nombre}' ya está siendo usado." });
                    else if (error.InnerException.Message.Contains("PRIMARY"))
                        return BadRequest(new ErrorViewModel { Titulo = "Error al adicionar producto", Mensaje = $"El id debe ser único y '{producto.Id}' ya está siendo usado." });
                }
                return BadRequest(new ErrorViewModel { Titulo = "Error al adicionar producto", Mensaje = $"{error.Message}{error.InnerException?.Message}" });
            }
            catch (Exception error)
            {
                _logger.LogError(error, "Error al adicionar producto"); //escribiendo el error en el log
                return BadRequest(new ErrorViewModel { Titulo = "Error al adicionar producto", Mensaje = $"{error.Message}{error.InnerException?.Message}" });
            }
        }

        // PUT: api/Productos/
        [HttpPut("{id}")]
        [Authorize("Administrador")]
        [Authorize("Vendedor")]
        public async Task<ActionResult> Actualizar(Guid id, [FromBody] ProductoViewModel producto)
        {
            /*ACTUALIZA LOS DATOS DEL PRODUCTO SELECCIONADO*/
            try
            {
                if (id != producto.Id)
                    return BadRequest();                

                //verificando que aun exista el producto 
                ProductoModel productoOriginal = await _context.Productos.Buscar(producto.Id, false);

                if (productoOriginal == null)
                    return NotFound("El producto seleccionado ya no se encuentra en la Base de Datos.");

                //verificando que el usuario que intenta modificar el producto sea el mismo que lo creo o tenga rol de administrador
                List<string> roles = (await _userManager.GetRolesAsync(await _userManager.FindByNameAsync(User.Identity.Name))).ToList();
                
                if (productoOriginal.CreadoPor == User.Identity.Name || roles.Contains("Administrador"))
                {
                    ProductoModel productoModel = _mapper.Map<ProductoModel>(producto);

                    //conservando la fecha en que se creo el producto
                    productoModel.FechaCreado = productoOriginal.FechaCreado;

                    // guardandolo en su estado original para las trazas
                    await CrearTraza("ActualizarProducto", $"Se ha actualizado al producto {producto.Nombre} con Id: {producto.Id}", objetoOriginal: productoOriginal, objetoModificado: producto);
                    
                    //actualizando rol
                    _context.Productos.Actualizar(productoModel);

                    try
                    {
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException error)
                    {
                        _logger.LogError(error, "Error al actualizar producto"); //escribiendo el error en el log

                        if (!await ProductoExiste(id))
                            return NotFound("El producto seleccionado ya no se encuentra en la Base de Datos.");
                        else
                            return BadRequest(new ErrorViewModel { Titulo = "Error al actualizar producto", Mensaje = $"{error.Message}{error.InnerException?.Message}" });
                    }
                    catch (DbUpdateException error)
                    {
                        _logger.LogError(error, "Error al actualizar producto"); //escribiendo el error en el log

                        if (error.InnerException.Message.Contains("Duplicate entry"))
                        {
                            if (error.InnerException.Message.Contains($"{nameof(producto.Nombre)}_UNIQUE"))
                                return BadRequest(new ErrorViewModel { Titulo = "Error al actualizar producto", Mensaje = $"El nombre del producto debe ser único y '{producto.Nombre}' ya está siendo usado." });
                            else if (error.InnerException.Message.Contains("for key 'PRIMARY'"))
                                return BadRequest(new ErrorViewModel { Titulo = "Error al actualizar producto", Mensaje = $"El id debe ser único y '{producto.Id}' ya está siendo usado." });
                        }
                        return BadRequest(new ErrorViewModel { Titulo = "Error al actualizar producto", Mensaje = $"{error.Message}{error.InnerException?.Message}" });
                    }

                    return Ok();
                }
                else return Unauthorized("El producto solo puede ser editado por el usuario que lo creó o por un usuario administrador");
            }
            catch (Exception error)
            {
                _logger.LogError(error, "Error al actualizar producto"); //escribiendo el error en el log
                return BadRequest(new ErrorViewModel { Titulo = "Error al actualizar producto", Mensaje = $"{error.Message}{error.InnerException?.Message}" });
            }
        }


        // DELETE: api/Productos/5
        [HttpDelete("{id}")]
        [Authorize("Administrador")]
        [Authorize("Vendedor")]
        public async Task<ActionResult> Eliminar(Guid id)
        {
            /*ELIMINA AL PRODUCTO SELECCIONADO DE LA BASE DE DATOS*/
            try
            {
                ProductoModel productoEliminado = await _context.Productos.Buscar(id, false);
                if (productoEliminado == null)
                    return NotFound("El producto seleccionado ya no se encuentra en la Base de Datos.");

                //verificando que el usuario que intenta eliminar el producto sea el mismo que lo creo o tenga rol de administrador
                List<string> roles = (await _userManager.GetRolesAsync(await _userManager.FindByNameAsync(User.Identity.Name))).ToList();

                if (productoEliminado.CreadoPor == User.Identity.Name || roles.Contains("Administrador"))
                {
                    _context.Productos.Eliminar(productoEliminado);
                    await CrearTraza("EliminarProducto", $"Se ha eliminado el producto {productoEliminado.Nombre} con Id: {productoEliminado.Id}", objetoEliminado: productoEliminado);
                    await _context.SaveChangesAsync();
                    return Ok();
                }
                return Unauthorized();
            }
            catch (DbUpdateException error)
            {
                _logger.LogError(error, "Error al eliminar producto"); //escribiendo el error en el log

                if (error.InnerException.Message.Contains("Cannot delete or update a parent row: a foreign key constraint fails"))
                {                    
                    return BadRequest(new ErrorViewModel { Titulo = "Error al eliminar producto", Mensaje = $"Este producto esta siendo usado por otros objetos y no puede ser eliminado" });
                }
                else 
                    return BadRequest(new ErrorViewModel { Titulo = "Error al eliminar producto", Mensaje = $"{error.Message}{error.InnerException?.Message}" });
            }
            catch (Exception error)
            {
                _logger.LogError(error, "Error al eliminar producto"); //escribiendo el error en el log
                return BadRequest(new ErrorViewModel { Titulo = "Error al eliminar producto", Mensaje = $"{error.Message}{error.InnerException?.Message}" });
            }
        }

        [Authorize("Cliente")]
        // Pget: api/Productos/Comprar
        [HttpPost("Comprar")]
        public async Task<ActionResult> ComprarProducto(CompraViewModel compra)
        {
            /*REBAJA LA EXISTENCIA DEL PRODUCTO EN LA TIENDA*/
            try
            {
                //verificando que aun exista el producto y rebajando la cantidad
                ProductoModel producto = await _context.Productos.Buscar(compra.ProductoId, false);

                if (producto == null)
                    return NotFound("El producto seleccionado ya no se encuentra en la Base de Datos.");

                if(producto.Cantidad<compra.Cantidad)
                    return BadRequest("La cantidad de productos seleccionada es inferior a la existente actualmente en la tienda.");

                //rebajando existencia del producto
                producto.Cantidad = producto.Cantidad - compra.Cantidad;
                _context.Productos.Actualizar(producto);

                //creando orden
                string usuarioId = (await _userManager.FindByNameAsync(User.Identity.Name)).Id;
                OrdenModel orden = new OrdenModel { Cantidad = compra.Cantidad, Estado = EstadoOrden.Created, Fecha = DateTime.Now, ProductoId = compra.ProductoId, UsuarioId = usuarioId };
                await _context.Ordenes.Adicionar(orden);
                await CrearTraza("ComprarProducto", $"Se ha creado una orden para comprar el producto {producto.Nombre} con Id: {producto.Id}", objetoCreado: orden);                

                await _context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception error)
            {
                _logger.LogError(error, "Error al comprar producto"); //escribiendo el error en el log
                return BadRequest(new ErrorViewModel { Titulo = "Error al comprar producto", Mensaje = $"{error.Message}{error.InnerException?.Message}" });
            }
        }

        private async Task<bool> ProductoExiste(Guid id)
        {
            /*VERIFICA QUE EL ROL EXISTA EN LA BASE DE DATOS*/
            return await _context.Productos.Existe(id);
        }

    }
}
