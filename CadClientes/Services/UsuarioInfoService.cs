using CadClientes.Helpers;
using CadClientes.IServices;
using CadClientes.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CadClientes.Services
{
    public class UsuarioInfoService : IUsuarioInfoService
    {
        private IEnumerable<UsuarioInfo> _usuario = new List<UsuarioInfo>
        {
            new UsuarioInfo{ UsuarioInfoId = Guid.NewGuid(), NomeCompleto="Ana Carolina", NomeUsuario="AnaCarol", Senha="teste"}
        };

        private readonly AppSettings _appSettings;

        public UsuarioInfoService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public async Task<UsuarioInfo> Autenticacao(string nomeUsuario, string senha)
        {
            var usuario =  _usuario.SingleOrDefault(u => u.NomeUsuario == nomeUsuario && u.Senha == senha);

            if (usuario == null)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Segredo);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, usuario.UsuarioInfoId.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)

            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            usuario.Token = tokenHandler.WriteToken(token);

            await Task.CompletedTask;
            return usuario;
        }

        public IEnumerable<UsuarioInfo>GetAll()
        {
            return _usuario;
        }
    }
}
