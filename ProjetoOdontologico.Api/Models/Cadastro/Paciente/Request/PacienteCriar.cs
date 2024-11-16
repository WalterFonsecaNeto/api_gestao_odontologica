namespace ProjetoOdontologico.Api.Models
{
    public class PacienteCriar
    {
        public int UsuarioId { get; set; }
        public string Nome { get; set; }
        public DateTime? DataNascimento { get; set; }
        public string Genero { get; set; }
        public string CPF { get; set; }
        public string Endereco { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public string HistoricoMedico { get; set; }
    }
}

