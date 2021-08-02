using CadClientes.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CadClientes.IServices
{
    public interface IClienteService
    {
        Task<Cliente> Save(Cliente cliente);
        Task<IEnumerable<Cliente>> Gets();
        Task<Cliente> Get(int clienteId);
        Task<string> Delete(int clienteId);

    }
}
