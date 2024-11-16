namespace ProjetoOdontologico.Api.Models
{
    public class ProcedimentoCriar
    {
        public int UsuarioId { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
        public int EspecialidadeId { get; set; }
    }
}