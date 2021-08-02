using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CadClientes.Models
{
    public class UsuarioInfo
    {
        [Key]
        public Guid UsuarioInfoId { get; set; }
        [Required]
        public string NomeCompleto { get; set; }
        [Required]
        public string EmailId { get; set; }
        [Required]
        public string NomeUsuario { get; set; }
        [Required]
        public string Senha { get; set; }
        [Required]
        [Compare("Senha")]
        public string ConfirmaSenha { get; set; }
        public string Token { get; set; }
    }
}
