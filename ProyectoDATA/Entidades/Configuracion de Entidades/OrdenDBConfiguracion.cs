using Microsoft.EntityFrameworkCore;
using ProyectoDATA.Entidades.Mod_Tienda;
using System;
using System.Collections.Generic;

namespace ProyectoDATA.Entidades.Configuracion_de_Entidades
{
    public class OrdenDBConfiguracion
    {
        public static void SetEntityBuilder(ModelBuilder modelBuilder)
        {
            #region Configurando Entidad
            BaseDBConfiguracion<Orden>.SetEntityBuilder(modelBuilder);

            modelBuilder.Entity<Orden>().ToTable("Ordenes");

            modelBuilder.Entity<Orden>().Property(orden => orden.Fecha).IsRequired().HasColumnType("Date"); ;
            modelBuilder.Entity<Orden>().Property(orden => orden.Cantidad).IsRequired();
            modelBuilder.Entity<Orden>().Property(orden => orden.Estado).IsRequired();

            modelBuilder.Entity<Orden>().HasIndex(orden => orden.Fecha);

            modelBuilder.Entity<Orden>().HasOne(orden => orden.Usuario).WithMany(usuario => usuario.Ordenes)
                                            .HasPrincipalKey(usuario => usuario.Id)
                                            .HasForeignKey(orden => orden.UsuarioId)
                                            .IsRequired(true)
                                            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Orden>().HasOne(orden => orden.Producto).WithMany(producto => producto.Ordenes)
                                            .HasPrincipalKey(producto => producto.Id)
                                            .HasForeignKey(orden => orden.ProductoId)
                                            .IsRequired(true)
                                            .OnDelete(DeleteBehavior.Restrict);
            #endregion
            #region Filtros

            #endregion
            #region Datos Inciales
            List<Orden> ordenes = new List<Orden> {
                new Orden{ Id = new Guid("4CD7A29E-1E21-4697-B3C9-B7F8EA30A72D"), Fecha = DateTime.Parse("2021-03-12"), Cantidad = 20, Estado = EstadoOrden.Confirmed, ProductoId = new Guid("6C875378-7BF3-4CA3-B056-BC360D65AC8C"), UsuarioId = new Guid("F6A8C8EB-309C-4CA7-91E1-46AA0FF16BC0").ToString()},
                new Orden{ Id = new Guid("40EA0E5D-C412-45F7-94F6-3E0051350F96"), Fecha = DateTime.Parse("2021-03-12"), Cantidad = 10, Estado = EstadoOrden.Created, ProductoId = new Guid("1DF535E8-9F12-4B9A-A780-F32A21C123A9"), UsuarioId = new Guid("F6A8C8EB-309C-4CA7-91E1-46AA0FF16BC0").ToString()},
                new Orden{ Id = new Guid("72690B9A-0AFB-4B3C-98C8-26809816BD7D"), Fecha = DateTime.Parse("2021-03-12"), Cantidad = 5, Estado = EstadoOrden.Created, ProductoId = new Guid("1DF535E8-9F12-4B9A-A780-F32A21C123A9"), UsuarioId = new Guid("F6A8C8EB-309C-4CA7-91E1-46AA0FF16BC0").ToString()},
                new Orden{ Id = new Guid("F2F4A187-3F67-4EB3-8BC8-B1550BD9FFE8"), Fecha = DateTime.Parse("2021-03-12"), Cantidad = 2, Estado = EstadoOrden.Canceled, ProductoId = new Guid("1DF535E8-9F12-4B9A-A780-F32A21C123A9"), UsuarioId = new Guid("F6A8C8EB-309C-4CA7-91E1-46AA0FF16BC0").ToString()},
                new Orden{ Id = new Guid("0D4D759F-58F6-4DD2-A890-079F2FFEF68C"), Fecha = DateTime.Parse("2021-03-12"), Cantidad = 20, Estado = EstadoOrden.Confirmed, ProductoId = new Guid("6C875378-7BF3-4CA3-B056-BC360D65AC8C"), UsuarioId = new Guid("F6A8C8EB-309C-4CA7-91E1-46AA0FF16BC0").ToString()}
            };
            modelBuilder.Entity<Orden>().HasData(ordenes);
            #endregion
        }
    }
}
