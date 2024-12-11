using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JWTWebAPI.Custom;
using JWTWebAPI.Models;
using JWTWebAPI.Models.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace JWTWebAPI.Controllers
{
    [Route("api/[controller]")]
    [AllowAnonymous] //Indica que se puede acceder sin estar registrado
    [ApiController]
    public class AccesoController : ControllerBase
    {
        private readonly BdapijwtContext _context;
        private readonly Utilidades _utilidades;

        public AccesoController(BdapijwtContext context, Utilidades utilidades)
        {
            _context = context;
            _utilidades = utilidades;
        }

        [HttpPost]
        [Route("Registrarse")]
        public async Task<IActionResult> Registrarse(UsuarioDTO objeto)
        {
            var modeloUsario = new Usuario
            {
                Nombre = objeto.Nombre,
                Correo = objeto.Correo,
                Clave = _utilidades.encriptarSHA256(objeto.Clave)
            };

            await _context.Usuarios.AddAsync(modeloUsario);
            await _context.SaveChangesAsync();

            if (modeloUsario.IdUsuario != 0)
                return StatusCode(StatusCodes.Status200OK, new {isSuccess = true });
            else 
                return StatusCode(StatusCodes.Status200OK, new { isSuccess = false });

        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(UsuarioDTO objeto)
        {
            var usuarioEncontrado = await _context.Usuarios
                .Where(u=>
                u.Correo==objeto.Correo &&
                u.Clave == _utilidades.encriptarSHA256(objeto.Clave)
                ).FirstOrDefaultAsync();

            if(usuarioEncontrado == null)
                return StatusCode(StatusCodes.Status200OK, new {isSucces=false, token = ""});
            else
                return StatusCode(StatusCodes.Status200OK, new { isSucces = false, token = _utilidades.generarJWT(usuarioEncontrado) });
        }
    }
}
