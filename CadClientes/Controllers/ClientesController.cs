using CadClientes.IServices;
using CadClientes.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CadClientes.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    [Authorize]
    public class ClientesController : ControllerBase
    {
        private IClienteService _clienteService;
        
        public ClientesController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }


        // GET: api/<ClientesController>
        [HttpGet("Clientes")]
        
        public async Task<IActionResult> ObterListaClientes()
        {
            return Ok(await _clienteService.Gets());
        }

        // GET api/<ClientesController>/5
        [HttpGet("Clientes/{id}")]

        public async Task<IActionResult> ObterCliente(int id)
        {           
             return Ok(await _clienteService.Get(id));   
        }

        //POST api/Clientes
        [HttpPost("Cliente")]
       
        public async Task<IActionResult> CadastrarCliente([FromBody] Cliente cliente)
        {
            if (ModelState.IsValid)
                return Ok(await _clienteService.Save(cliente));
            return BadRequest();
        }

        //// PUT api/<ClientesController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{

        //}

        //// DELETE api/<ClientesController>/5
        [HttpDelete("Cliente/{id}")]
        public async Task<IActionResult>  DeletarCliente(int id)
        {
            return Ok(await _clienteService.Delete(id));
        }
    }
}
