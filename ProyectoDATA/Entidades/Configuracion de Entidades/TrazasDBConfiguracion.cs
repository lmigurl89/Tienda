using Microsoft.EntityFrameworkCore;
using ProyectoDATA.Entidades.Mod_Seguridad;

namespace ProyectoDATA.Entidades.Configuracion_de_Entidades
{
    public class TrazasDBConfiguracion
    {
        public static void SetEntityBuilder(ModelBuilder modelBuilder)
        {
            #region Configurando Entidad
            BaseDBConfiguracion<Traza>.SetEntityBuilder(modelBuilder);

            modelBuilder.Entity<Traza>().ToTable("Trazas");

            modelBuilder.Entity<Traza>().Property(trace => trace.Descripcion).IsRequired();
            modelBuilder.Entity<Traza>().Property(trace => trace.NombrePC).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<Traza>().Property(user => user.UserName).IsRequired().HasMaxLength(100);

            modelBuilder.Entity<Traza>().Property(user => user.ObjetoCreado).HasColumnType("json");
            modelBuilder.Entity<Traza>().Property(user => user.ObjetoAntesModificar).HasColumnType("json");
            modelBuilder.Entity<Traza>().Property(user => user.ObjetoModificado).HasColumnType("json");
            modelBuilder.Entity<Traza>().Property(user => user.ObjetoEliminado).HasColumnType("json");

            
            modelBuilder.Entity<Traza>().HasIndex(trace => trace.NombreAccion).HasName("FK_NombreAccion_Trazas_idx");


            
            #endregion
        }
    }
}
