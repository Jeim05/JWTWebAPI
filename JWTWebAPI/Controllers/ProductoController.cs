using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JWTWebAPI.Custom;
using JWTWebAPI.Models;
using Microsoft.AspNetCore.Authorization;

namespace JWTWebAPI.Controllers
{
    [Route("api/[controller]")]
    [Authorize()] // Solo pueden acceder usuarios Autorizados
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly BdapijwtContext _context;

        public ProductoController(BdapijwtContext context, Utilidades utilidades)
        {
            _context = context;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            var lista = await _context.Productos.ToListAsync();
            return StatusCode(StatusCodes.Status200OK, new {value = lista});

        }
        
        }
}
