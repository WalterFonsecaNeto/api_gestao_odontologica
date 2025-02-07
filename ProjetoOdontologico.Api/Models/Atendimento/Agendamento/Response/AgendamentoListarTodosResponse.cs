using ProjetoOdontologico.Dominio.Entidades;

namespace ProjetoOdontologico.Api.Models
{
public class AgendamentoListarTodosResponse
    {
        public int Id { get; set; }
        public string PacienteNome { get; set; }
        public DateTime DataHora { get; set; }
        public string Status { get; set; }
        public string Descricao { get; set; }

    }
}