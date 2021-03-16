using Microsoft.EntityFrameworkCore;
using ProyectoDATA.Entidades.Mod_Seguridad;
using System;
using System.Collections.Generic;

namespace ProyectoDATA.Entidades.Configuracion_de_Entidades
{
    public class UsuariosDBConfiguracion
    {
        public static void SetEntityBuilder(ModelBuilder modelBuilder)
        {
            #region Configurando Entidad

            modelBuilder.Entity<Usuario>().ToTable("Usuarios");

            modelBuilder.Entity<Usuario>().Property(user => user.Nombre).IsRequired().HasMaxLength(100); ;
            modelBuilder.Entity<Usuario>().Property(user => user.Apellidos).IsRequired().HasMaxLength(100);

            #endregion
            #region Filtros
            
            #endregion
            #region Datos Inciales
            List<Usuario> usuarios = new List<Usuario> {
                new Usuario{ Id=new Guid("F6A8C8EB-309C-4CA7-91E1-46AA0FF16BC0").ToString(), Nombre="usuario",Apellidos=" Administrador", Email="administrador@tienda.com", NormalizedEmail="ADMINISTRADOR@TIENDA.COM", UserName="administrador", NormalizedUserName="ADMINISTRADOR", PasswordHash="AQAAAAEAACcQAAAAEAkXw5Al5CMhhZiQOvGxpO4d5hOXE4YdHbHv7NvP7wXhuwIPQkBLgjM+4zHCkBqaPA==", SecurityStamp="NLN3QCO657I7UVCHLVZ2AMKFMK37JCIY", ConcurrencyStamp="44c8c6e9-f046-4af2-92ba-eb91b232b110", LockoutEnabled=true },
                new Usuario{ Id=new Guid("549889BF-FEC3-4403-A536-133CD8CDEAC5").ToString(), Nombre="usuario",Apellidos=" Vendedor", Email="vendedor@tienda.com", NormalizedEmail="VENDEDOR@TIENDA.COM", UserName="vendedor", NormalizedUserName="VENDEDOR", PasswordHash="AQAAAAEAACcQAAAAEAkXw5Al5CMhhZiQOvGxpO4d5hOXE4YdHbHv7NvP7wXhuwIPQkBLgjM+4zHCkBqaPA==", SecurityStamp="NLN3QCO657I7UVCHLVZ2AMKFMK37JCIY", ConcurrencyStamp="44c8c6e9-f046-4af2-92ba-eb91b232b110", LockoutEnabled=true },
                new Usuario{ Id=new Guid("787D0BC9-048C-4E9F-B6C3-DF267442A3AA").ToString(), Nombre="usuario",Apellidos=" Cliente", Email="cliente@tienda.com", NormalizedEmail="CLIENTE@TIENDA.COM", UserName="cliente", NormalizedUserName="CLIENTE", PasswordHash="AQAAAAEAACcQAAAAEAkXw5Al5CMhhZiQOvGxpO4d5hOXE4YdHbHv7NvP7wXhuwIPQkBLgjM+4zHCkBqaPA==", SecurityStamp="NLN3QCO657I7UVCHLVZ2AMKFMK37JCIY", ConcurrencyStamp="44c8c6e9-f046-4af2-92ba-eb91b232b110", LockoutEnabled=true }
            };
            modelBuilder.Entity<Usuario>().HasData(usuarios);
            #endregion
        }
    }
}
