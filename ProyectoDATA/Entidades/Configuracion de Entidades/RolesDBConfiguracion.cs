using Microsoft.EntityFrameworkCore;
using ProyectoDATA.Entidades.Mod_Seguridad;
using System;
using System.Collections.Generic;

namespace ProyectoDATA.Entidades.Configuracion_de_Entidades
{
    public class RolesDBConfiguracion
    {
        public static void SetEntityBuilder(ModelBuilder modelBuilder)
        {
            #region Configurando Entidad

            modelBuilder.Entity<Rol>().Property(rol => rol.Descripcion).HasMaxLength(200);

            #endregion
            #region Filtros
            
            #endregion
            #region Insertando Datos Iniciales

            List<Rol> roles = new List<Rol>
            {
                new Rol{ Id=new Guid("91E0F2A3-3A4C-46E4-83A6-94356CD46725").ToString(), Name="Administrador", NormalizedName="ADMINISTRADOR", Descripcion="Administradores de la tienda"},
                new Rol{ Id=new Guid("F56DC294-76E6-4277-9685-B4CA524F61D8").ToString(), Name="Vendedor", NormalizedName="VENDEDOR", Descripcion="Vendedor de la tienda"},
                new Rol{ Id=new Guid("49EFD009-CED5-4FB9-868D-CF365A34765F").ToString(), Name="Cliente", NormalizedName="CLIENTE", Descripcion="Cliente de la tienda"},
            };

           modelBuilder.Entity<Rol>().HasData(roles);

            #endregion
        }
    }
}
