namespace ProjetoOdontologico.Api.Models
{
    public class AgendamentoAtualizar
    {
        public int PacienteId { get; set; }
        public DateTime DataHora { get; set; }
        public string Status { get; set; }
        public string Descricao { get; set; }
    }
}