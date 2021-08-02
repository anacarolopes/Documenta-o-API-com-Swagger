namespace CadClientes.Models
{
    public class Cliente
    {
        public int ClienteId { get; set; }
        public string Nome { get; set; }
        public long Cpf { get; set; }
        public string Email { get; set; }
        public long Telefone { get; set; }
        public string Message { get; set; }
    }
}
