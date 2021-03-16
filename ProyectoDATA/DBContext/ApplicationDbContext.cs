using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProyectoDATA.DBContext.Interfaces;
using ProyectoDATA.Entidades.Configuracion_de_Entidades;
using ProyectoDATA.Entidades.Mod_Seguridad;
using ProyectoDATA.Entidades.Mod_Tienda;
using System.Linq;

namespace ProyectoDATA.DBContext
{
    public class ApplicationDbContext :  IdentityDbContext<Usuario>, IApplicationDbContext
    {
        #region Entidades
        public new DbSet<Rol> Roles { get; set; }        
        public DbSet<Traza> Trazas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }        
        public DbSet<Producto> Productos { get; set; }        
        public DbSet<Orden> Ordenes { get; set; }        
        public DbSet<Rol_Usuarios> Rol_Usuarios { get; set; }        
        #endregion

        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            RolesDBConfiguracion.SetEntityBuilder(modelBuilder);           
            TrazasDBConfiguracion.SetEntityBuilder(modelBuilder);
            UsuariosDBConfiguracion.SetEntityBuilder(modelBuilder);
            ProductoDBConfiguracion.SetEntityBuilder(modelBuilder);
            OrdenDBConfiguracion.SetEntityBuilder(modelBuilder);
            Rol_UsuariosDBConfiguracion.SetEntityBuilder(modelBuilder);
            
            base.OnModelCreating(modelBuilder);
        }

        public void Migrate()
        {
            Database.Migrate();
        }

    }
}

