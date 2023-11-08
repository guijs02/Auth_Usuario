using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using UsuariosApi.Api.Domain.Models;

namespace UsuariosApi.Api.Application.Services
{
    public class TokenService
    {
        public string GenerateToken(Usuario usuario)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes("fjdik4343493ADFJFAK933432FDxxs&$#33444fsjdbabaii(9%22")
            );

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                    new Claim[]
                    {
                        new Claim(ClaimTypes.Name, usuario.UserName),
                        new Claim("id", usuario.Id),
                        new Claim(ClaimTypes.DateOfBirth, usuario.DataNascimento.ToString()),
                        new Claim("loginTimeStmp", DateTime.UtcNow.ToString()),
                        new Claim(ClaimTypes.Role, usuario.TipoCargo.ToString()),
                    }
                ),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            };

            //retorna o token em forma de cadeia de caracteres
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
