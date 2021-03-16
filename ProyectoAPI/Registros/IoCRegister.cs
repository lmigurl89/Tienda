using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ProyectoAPI.AutoMapper;
using ProyectoAPI.Handlers;
using ProyectoDATA.AutoMapper;
using ProyectoDATA.DBContext;
using ProyectoDATA.DBContext.Interfaces;
using ProyectoDATA.Entidades.Mod_Seguridad;
using ProyectoDATA.Modelo_de_Datos.Mod_Seguridad;
using ProyectoDATA.Repositorios;
using ProyectoDATA.Repositorios.Interfaces_de_Repositorios;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoAPI.Registros
{
    public static class IoCRegister
    {
        public static IServiceCollection AddRegistration(this IServiceCollection services, IConfiguration configuration)
        {
            AddRegisterDataContext(services, configuration);
            AddRegisterServices(services, configuration);
            AddRegisterRepositories(services);
            AddRegisterPolicies(services);

            return services;
        }

        public static IServiceCollection AddRegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(new Assembly[] { typeof(DataAutoMapperProfile).GetTypeInfo().Assembly, typeof(ApiAutoMapperProfile).GetTypeInfo().Assembly });



            // Cors
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAnyCorsPolicy", builder =>
                {
                    builder.AllowAnyHeader();
                    builder.AllowAnyOrigin();
                    builder.AllowAnyMethod();
                });
            });

            services.AddControllers();


            //Swagger
            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new OpenApiInfo { Title = "Tienda API", Version = "v1" });
                s.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                s.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                      new OpenApiSecurityScheme
                      {
                        Reference = new OpenApiReference
                          {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                          },
                          Scheme = "oauth2",
                          Name = "Bearer",
                          In = ParameterLocation.Header,

                        },
                        new List<string>()
                    }
                });
         
            });

            //registration for JWT
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                // Configure JWT Bearer Auth to expect our security key
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["ValidationParameters:Issuer"],
                    ValidAudience = configuration["ValidationParameters:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["SecretKey"])),
                    ClockSkew = TimeSpan.Zero
                };

                // Authentication Personalizada
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        //Validando la fecha de expiracion del token
                        string tokenExpiration = context.Request.Headers["tokenExpiration"];
                        var path = context.HttpContext.Request.Path;
                        if (!string.IsNullOrEmpty(tokenExpiration) && DateTime.Parse(tokenExpiration) < DateTime.Now)
                        {
                            context.Fail(new SecurityTokenExpiredException("La sesion ha expirado."));
                            return Task.CompletedTask;
                        }

                        return Task.CompletedTask;
                    }
                };

            });

            services.AddIdentity<Usuario, Rol>().AddRoles<Rol>().AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddRazorPages();
            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;
                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;
                // User settings.
                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true;
            });


            return services;
        }

        public static IApplicationBuilder AddRegistration(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Tienda v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCors("AllowAnyCorsPolicy");
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });


            return app;
        }

        public static IServiceCollection AddRegisterDataContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseMySql(connectionString: configuration.GetConnectionString("DefaultConnection"), serverVersion: ServerVersion.AutoDetect(connectionString: configuration.GetConnectionString("DefaultConnection"))));
            services.AddScoped<IApplicationDbContext, ApplicationDbContext>();

            return services;
        }

        public static IServiceCollection AddRegisterRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IRepositorio<Traza, TrazaModel>, Repositorio<Traza, TrazaModel>>();

            return services;
        }

        public static IServiceCollection AddRegisterPolicies(this IServiceCollection services)
        {
            // Agregando las Politicas para la autorizacion
            services.AddAuthorization( options =>
            {
                var context = services
                    .BuildServiceProvider()
                    .GetRequiredService<ApplicationDbContext>();

                List<Rol> roles = context.Roles.ToList();

                foreach (var rol in roles)
                {
                    options.AddPolicy(rol.Name, policy => policy.Requirements.Add(new AccionRequirement(rol.Name)));
                }
            });
            return services;
        }
    }
}