using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ProyectoDATA.Modelo_de_Datos.Mod_Seguridad;
using ProyectoDATA.Repositorios.Interfaces_de_Repositorios;
using System;
using System.Net;
using System.Threading.Tasks;
namespace ProyectoAPI.Controladores
{
    public class BaseController : ControllerBase
    {
        protected readonly IUnitOfWork _context;
        protected readonly IMapper _mapper;
        protected readonly IConfiguration _configuration;
        protected readonly ILogger<BaseController> _logger;

        protected BaseController(IUnitOfWork context, IMapper mapper, IConfiguration configuration, ILogger<BaseController> logger)
        {
            _context = context;
            _configuration = configuration;
            _mapper = mapper;
            _logger = logger;
        }

        protected async Task CrearTraza(string nombreAccion, string descripcion, object objetoCreado = null, object objetoOriginal = null,
                                        object objetoModificado = null, object objetoEliminado = null, string username= null)
        {
            await _context.Trazas.Adicionar(new TrazaModel
            {
                Id = new Guid(),
                UserName = username==null? User.Identity.Name : username,
                NombreAccion = nombreAccion,
                NombrePC = Dns.GetHostName(),
                Descripcion = descripcion,
                ObjetoCreado = JsonConvert.SerializeObject(objetoCreado, Formatting.Indented, new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects }),
                ObjetoAntesModificar = JsonConvert.SerializeObject(objetoOriginal, Formatting.Indented, new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects }),
                ObjetoModificado = JsonConvert.SerializeObject(objetoModificado, Formatting.Indented, new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects }),
                ObjetoEliminado = JsonConvert.SerializeObject(objetoEliminado, Formatting.Indented, new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects }),
            });
        }
    }
}