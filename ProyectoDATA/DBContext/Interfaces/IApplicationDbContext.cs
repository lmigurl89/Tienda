using Microsoft.EntityFrameworkCore;
using ProyectoDATA.Entidades.Mod_Seguridad;
using ProyectoDATA.Entidades.Mod_Tienda;
using System.Threading;
using System.Threading.Tasks;

namespace ProyectoDATA.DBContext.Interfaces
{
    public interface IApplicationDbContext
    {
        #region Entidades
        DbSet<Rol> Roles { get; set; }
        DbSet<Traza> Trazas { get; set; }
        DbSet<Usuario> Usuarios { get; set; }
        DbSet<Producto> Productos { get; set; }
        DbSet<Orden> Ordenes { get; set; }
        public DbSet<Rol_Usuarios> Rol_Usuarios { get; set; }

        #endregion

        void Migrate();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
