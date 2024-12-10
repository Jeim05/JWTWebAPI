using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using JWTWebAPI.Models;

namespace JWTWebAPI.Custom
{
    public class Utilidades
    {
        private readonly IConfiguration _configuration;

        public Utilidades(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string encriptarSHA256(string text)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                //Computar el Hash
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(text));

                //Convertir el array de bytes a string
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        public string generarJWT(Usuario modelo)
        {
            //crear la información del usuario para el token
            var userClaims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, modelo.IdUsuario.ToString()),
                new Claim(ClaimTypes.Email,modelo.Correo!)
            };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:key"]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            //crear detalle del Token
            var jwtConfig = new JwtSecurityToken(
                claims: userClaims,
                expires: DateTime.UtcNow.AddMinutes(10),
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(jwtConfig);

        }
    }
}
