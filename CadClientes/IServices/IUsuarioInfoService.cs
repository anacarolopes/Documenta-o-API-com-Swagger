using CadClientes.Models;
using System.Threading.Tasks;

namespace CadClientes.IServices
{
    public interface IUsuarioInfoService
    {
        Task<UsuarioInfo> Autenticacao(string nomeUsuario, string senha);
    }
}
