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
    }
}
