using Microsoft.EntityFrameworkCore;
using ProyectoDATA.Entidades.Mod_Tienda;
using System;
using System.Collections.Generic;

namespace ProyectoDATA.Entidades.Configuracion_de_Entidades
{
    public class ProductoDBConfiguracion
    {
        public static void SetEntityBuilder(ModelBuilder modelBuilder)
        {
            #region Configurando Entidad
            BaseDBConfiguracion<Producto>.SetEntityBuilder(modelBuilder);

            modelBuilder.Entity<Producto>().ToTable("Productos");

            modelBuilder.Entity<Producto>().Property(producto => producto.Nombre).IsRequired(); 
            modelBuilder.Entity<Producto>().Property(producto => producto.Descripcion).HasMaxLength(100);
            modelBuilder.Entity<Producto>().Property(producto => producto.Precio).IsRequired();
            modelBuilder.Entity<Producto>().Property(producto => producto.Cantidad).IsRequired().HasMaxLength(100);

            modelBuilder.Entity<Producto>().HasIndex(producto => producto.Nombre).HasName($"{nameof(Producto.Nombre)}_UNIQUE").IsUnique();

            #endregion
            #region Filtros

            #endregion
            #region Datos Inciales
            List<Producto> productos = new List<Producto> {
                new Producto{ Id = new Guid("5D1A6250-4BDC-46C0-8FB7-B965DAD45D79"), Nombre = "MotherBoard", Descripcion = "MotherBoard", Cantidad = 220, Precio = 20.99, CreadoPor="vendedor", FechaCreado= DateTime.Parse("2021-03-03") },
                new Producto{ Id = new Guid("4B5A5CAA-8858-4FAA-9E86-C5023D1119CD"), Nombre = "Mouse", Descripcion = "Mouse", Cantidad = 614, Precio = 52, CreadoPor="vendedor", FechaCreado= DateTime.Parse("2021-03-03")},
                new Producto{ Id = new Guid("4A7B536F-7777-4AF9-BEC8-E9A9530D630D"), Nombre = "Monitor", Descripcion = "Monitor", Cantidad = 20, Precio = 65.65, CreadoPor="vendedor", FechaCreado= DateTime.Parse("2021-03-03")},
                new Producto{ Id = new Guid("1DF535E8-9F12-4B9A-A780-F32A21C123A9"), Nombre = "RAM", Descripcion = "RAM", Cantidad = 220, Precio = 20, CreadoPor="vendedor", FechaCreado= DateTime.Parse("2021-03-03")},
                new Producto{ Id = new Guid("6C875378-7BF3-4CA3-B056-BC360D65AC8C"), Nombre = "KeyBoard", Descripcion = "KeyBoard", Cantidad = 220, Precio = 10.99, CreadoPor="vendedor", FechaCreado= DateTime.Parse("2021-03-03")},
            };
            modelBuilder.Entity<Producto>().HasData(productos);
            #endregion
        }
    }
}
