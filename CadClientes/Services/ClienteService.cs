using CadClientes.Common;
using CadClientes.IServices;
using CadClientes.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace CadClientes.Services
{
    public class ClienteService : IClienteService
    {
        Cliente _cliente = new Cliente();
        List<Cliente> _clientes = new List<Cliente>();

        public async Task<string> Delete(int clienteId)
        {
            string message = "";

            try
            {
                _cliente = new Cliente()
                {
                    ClienteId = clienteId
                };

                using (IDbConnection con = new SqlConnection(Global.ConnectionString))
                {
                    if (con.State == ConnectionState.Closed) con.Open();

                    var clientes = await con.QueryAsync<Cliente>("SP_Cliente", this.SetParameters(_cliente, (int)OperationType.Deletar), commandType: CommandType.StoredProcedure);

                    message = "Deletado!";
                }
            }
            catch(Exception ex)
            {
                message = ex.Message;
            }
            return message;
        }

        public async Task<Cliente> Get(int clienteId)
        {
            _cliente = new Cliente();
            using (IDbConnection con = new SqlConnection(Global.ConnectionString))
            {
                if (con.State == ConnectionState.Closed) con.Open();

                var clientes = await con.QueryAsync<Cliente>("SELECT * FROM tb_clientes WHERE clienteId = " + clienteId);

                if (clientes != null && clientes.Count() > 0)
                {
                    _cliente = clientes.SingleOrDefault();
                }
            }
           
            return _cliente;
        }

        public async Task<IEnumerable<Cliente>> Gets()
        {
            _clientes = new List<Cliente>();
            
            using (IDbConnection con = new SqlConnection(Global.ConnectionString))
            {
                if (con.State == ConnectionState.Closed) con.Open();

                var clientes = await con.QueryAsync<Cliente>("SELECT * FROM tb_clientes");

                if (clientes != null && clientes.Count() > 0)
                {
                    _clientes = (List<Cliente>)clientes;
                }
            }
            
            return _clientes.ToList();
        }

        public async Task<Cliente> Save(Cliente cliente)
        {
            _cliente = new Cliente();
            try
            {
                int operationType = Convert.ToInt32(cliente.ClienteId == 0 ? OperationType.Inserir : OperationType.Atualizar);

                using(IDbConnection con = new SqlConnection(Global.ConnectionString))
                {
                    if (con.State == ConnectionState.Closed) con.Open();

                    var clientes = await con.QueryAsync<Cliente>("SP_Cliente", this.SetParameters(cliente, operationType), commandType: CommandType.StoredProcedure);

                    if(clientes != null && clientes.Count() > 0)
                    {
                        _cliente = clientes.FirstOrDefault();
                    }
                }
            }
            catch(Exception ex)
            {
                _cliente.Message = ex.Message;
            }
            
            return _cliente;
        }

        private DynamicParameters SetParameters(Cliente cliente, int operationType)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@ClienteId", cliente.ClienteId);
            parameters.Add("@Nome", cliente.Nome);
            parameters.Add("@Cpf", cliente.Cpf);
            parameters.Add("@Email", cliente.Email);
            parameters.Add("@Telefone", cliente.Telefone);
            parameters.Add("@OperationType", operationType);
            return parameters;
        }
    }
}
