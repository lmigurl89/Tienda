using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using ProyectoAPI.Modelos_para_Vistas;
using ProyectoAPI.Modelos_para_Vistas.Mod_Seguridad;
using ProyectoDATA.Modelo_de_Datos.Mod_Seguridad;
using ProyectoDATA.Repositorios.Interfaces_de_Repositorios;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using ProyectoDATA.Entidades.Mod_Seguridad;

namespace ProyectoAPI.Controladores.Mod_Seguridad
{
    [Produces("application/json")]
    [Consumes("application/json")]
    [EnableCors("AllowAnyCorsPolicy")]
    [Route("api/Login")]
    [ApiController]
    public class AutorizacionController : BaseController
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly SignInManager<Usuario> _signInManager;

        public AutorizacionController(IUnitOfWork context, IMapper mapper, IConfiguration configuration, ILogger<AutorizacionController> logger, UserManager<Usuario> userManager, SignInManager<Usuario> signInManager)
            : base(context, mapper, configuration, logger)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        

        // POST: api/Login
        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel login)
        {
            try
            {
                if ((await _signInManager.PasswordSignInAsync(login.UserName, login.Password, false, false)).Succeeded)
                {
                    Task crearTrazaTask = CrearTraza("Logiarse", "Usuario ha iniciado sesión.", username: login.UserName);
                    Task<ActionResult> construirTokenTask = ConstruirToken(login);
                    await Task.WhenAll( construirTokenTask, crearTrazaTask);

                    return construirTokenTask.Result;
                }
                else
                    return Unauthorized(new { message = "Usuario o contraseña no valido." });
            }
            catch (Exception error)
            {
                _logger.LogError(error, "Error al autenticar usuario");
                return BadRequest(new ErrorViewModel { Titulo = "Error al autenticar usuario", Mensaje = $"{error.Message}{error.InnerException?.Message}" });
            }
        }

        private bool VerificarPasswordEnciptada(string contrasennaEncriptada, string contrasenna)
        {
            byte[] buffer4;
            if (contrasennaEncriptada == null)
                return false;
            if (contrasenna == null)
                throw new ArgumentNullException(nameof(contrasenna));

            byte[] src = Convert.FromBase64String(contrasennaEncriptada);
            if ((src.Length != 49) || (src[0] != 0))
                return false;

            byte[] dst = new byte[16];
            Buffer.BlockCopy(src, 1, dst, 0, 16);
            byte[] buffer3 = new byte[32];
            Buffer.BlockCopy(src, 17, buffer3, 0, 32);
            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(contrasenna, dst, 1000))
            {
                buffer4 = bytes.GetBytes(32);
            }
            return buffer3.SequenceEqual(buffer4);
        }

        private async Task<ActionResult> ConstruirToken(LoginViewModel login)
        {
            List<string> roles = (await _userManager.GetRolesAsync(await _userManager.FindByNameAsync(login.UserName))).ToList();

            //create claims
            List <Claim> claims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.UniqueName, login.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())   
                };

            roles.ForEach(rol => claims.Add(new Claim(rol, rol)));

            //build token
            var llaveSecreta = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["SecretKey"]));
            var credenciales = new SigningCredentials(llaveSecreta, SecurityAlgorithms.HmacSha256);
            var fechaExpiracion = DateTime.UtcNow.AddHours(double.Parse(_configuration["ValidationParameters:TimeSpan"]));

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: _configuration["ValidationParameters:Issuer"],
                audience: _configuration["ValidationParameters:Audience"],
                claims: claims,
                expires: fechaExpiracion,
                signingCredentials: credenciales
                );

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                fechaExpiracion
            });
        }
    }
}
