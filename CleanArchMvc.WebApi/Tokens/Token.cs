using CleanArchMvc.Domain.Account;
using CleanArchMvc.WebApi.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CleanArchMvc.WebApi.Tokens
{
    public class Token
    {
        private readonly IConfiguration _configuration;

        public Token(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public ActionResult<UserToken> GenerateToken(string email)
        {
            //declarações do usuário
            var claims = new[]
            {
                new Claim("email", email), //claim padrão
                new Claim("meuvalor", "New token"), //claim personalizada (mensagem)
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()) //jti é o identificador do token
            };

            //gerar chave privada para assinar o token
            var privateKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));

            //gerar a assinatura digital
            var credentials = new SigningCredentials(privateKey, SecurityAlgorithms.HmacSha256);

            //definir o tempo de expiração
            int time = int.TryParse(_configuration["TimeJwt:time"], out var parsed) ? parsed : 10;
            var expiration = DateTime.UtcNow.AddMinutes(time);

            //gerar o token
            JwtSecurityToken token = new JwtSecurityToken(
                    //emissor
                    issuer: _configuration["Jwt:Issuer"],
                    //audiencia
                    audience: _configuration["Jwt:Audience"],
                    //claims
                    claims: claims,
                    //data de expiracao
                    expires: expiration,
                    //assinatura digital
                    signingCredentials: credentials
                );

            return new UserToken()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration
            };
        }
    }
}
