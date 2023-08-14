using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using UsuariosApi.Models;

namespace UsuariosApi.Services
{
    public class TokenService
    {
        public string GenerateToken(Usuario usuario)
        {
            Claim[] claims = new Claim[]
            {
                new Claim("username", usuario.UserName),
                new Claim("id", usuario.Id),
                new Claim(ClaimTypes.DateOfBirth, usuario.DataNascimento.ToString()),
                new Claim("loginTimeStmp", DateTime.UtcNow.ToString()),
                new Claim("tipoCargo", usuario.TipoCargo.ToString()) 
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("fjdik4343493ADFJFAK933432FDxxs&$#33444fsjdbabaii(9%22"));

            var signingCredentials = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                expires: DateTime.Now.AddMinutes(10),
                claims: claims,
                signingCredentials: signingCredentials
            );
            //retorna o token em forma de cadeia de caracteres
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
