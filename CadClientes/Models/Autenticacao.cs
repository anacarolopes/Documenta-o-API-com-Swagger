using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CadClientes.Models
{
    public class Autenticacao
    {
        [Required]
        public string NomeUsuario { get; set; }
        [Required]
        public string Senha { get; set; }
    }
}
