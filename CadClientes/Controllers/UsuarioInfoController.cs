using CadClientes.IServices;
using CadClientes.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CadClientes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioInfoController : ControllerBase
    {
        private IUsuarioInfoService _usuarioInfoService;

        public UsuarioInfoController(IUsuarioInfoService usuarioInfoService)
        {
            _usuarioInfoService = usuarioInfoService;
        }

        [HttpPost("autenticacao")]
        [AllowAnonymous]
        public async Task<IActionResult> Autenticar([FromBody]Autenticacao model)
        {
            var usuario = await _usuarioInfoService.Autenticacao(model.NomeUsuario, model.Senha);
            if (usuario == null) 
                return BadRequest(new { message = "Nome de usuário e senha inválidos" });
            return Ok(usuario);
        }


        // GET: api/<UsuarioInfoController>
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET api/<UsuarioInfoController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST api/<UsuarioInfoController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        // PUT api/<UsuarioInfoController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        // DELETE api/<UsuarioInfoController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
