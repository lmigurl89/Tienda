using Microsoft.EntityFrameworkCore;
using System;

namespace ProyectoDATA.Entidades.Configuracion_de_Entidades
{
    public class BaseDBConfiguracion<TEntity> where TEntity : BaseEntity
    {
        public static void SetEntityBuilder(ModelBuilder modelBuilder)
        {
            #region Configurando Entidad

            modelBuilder.Entity<TEntity>().Property(entity => entity.Id).IsRequired()
                         .ValueGeneratedOnAdd()
                         .HasColumnType("varchar(50)");

            modelBuilder.Entity<TEntity>().Property(entity => entity.FechaCreado).IsRequired().HasDefaultValue(new DateTime());            

            modelBuilder.Entity<TEntity>().HasIndex(entity => entity.Id).HasName("Id_UNIQUE").IsUnique();

            #endregion
        }
    }
}
