namespace ProjetoOdontologico.Api.Models
{
    public class AgendamentoResponse
    {
        public int Id { get; set; }
        public int PacienteId { get; set; }
        public DateTime DataHora { get; set; }
        public string Status { get; set; }
        public string Descricao { get; set; }

    }
}