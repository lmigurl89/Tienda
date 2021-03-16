using AutoMapper;
using Microsoft.AspNetCore.Identity;
using ProyectoDATA.DBContext;
using ProyectoDATA.Entidades.Mod_Seguridad;
using ProyectoDATA.Modelo_de_Datos.Mod_Seguridad;
using ProyectoDATA.Repositorios.Interfaces_de_Repositorios;
using ProyectoDATA.Repositorios.Mod_Seguridad;
using ProyectoDATA.Repositorios.Mod_Tienda;
using System.Threading;
using System.Threading.Tasks;

namespace ProyectoDATA.Repositorios
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;        
        private readonly IMapper  _mapper;
        private readonly SignInManager<Usuario> _signInManager;
        private readonly UserManager<Usuario> _userManager;

        public ITrazaRepositorio Trazas { get; }
        public IProductoRepositorio Productos { get; }
        public IOrdenRepositorio Ordenes { get; }
        

        public UnitOfWork(ApplicationDbContext context, IMapper mapper, UserManager<Usuario> userManager, SignInManager<Usuario> signInManager)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;

            Trazas = new TrazaRepositorio (context, mapper);        
            Productos = new ProductoRepositorio(context, mapper);        
            Ordenes = new OrdenRepositorio(context, mapper);        
           
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return _context.SaveChangesAsync(cancellationToken);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
