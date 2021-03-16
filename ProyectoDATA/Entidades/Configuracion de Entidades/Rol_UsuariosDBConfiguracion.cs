using Microsoft.EntityFrameworkCore;
using ProyectoDATA.Entidades.Mod_Seguridad;
using System;
using System.Collections.Generic;

namespace ProyectoDATA.Entidades.Configuracion_de_Entidades
{
    public class Rol_UsuariosDBConfiguracion
    {
        public static void SetEntityBuilder(ModelBuilder modelBuilder)
        {
            #region Configurando Entidad            


            #endregion
            #region Insertando Datos Iniciales

            List<Rol_Usuarios> rol_usuario = new List<Rol_Usuarios>
            {
                new Rol_Usuarios{ RoleId=new Guid("91E0F2A3-3A4C-46E4-83A6-94356CD46725").ToString(), UserId=new Guid("F6A8C8EB-309C-4CA7-91E1-46AA0FF16BC0").ToString() },
                new Rol_Usuarios{ RoleId=new Guid("F56DC294-76E6-4277-9685-B4CA524F61D8").ToString(), UserId=new Guid("549889BF-FEC3-4403-A536-133CD8CDEAC5").ToString() },
                new Rol_Usuarios{ RoleId=new Guid("49EFD009-CED5-4FB9-868D-CF365A34765F").ToString(), UserId=new Guid("787D0BC9-048C-4E9F-B6C3-DF267442A3AA").ToString() },
               
            };

            modelBuilder.Entity<Rol_Usuarios>().HasData(rol_usuario);

            #endregion
        }
    }
}
