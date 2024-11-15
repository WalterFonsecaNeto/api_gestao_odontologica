namespace ProjetoOdontologico.Dominio.Entidades
{
    public class Orcamento
    {
        #region Propiedades
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public int PacienteId { get; set; }
        public DateTime DataCriacao { get; set; }
        public decimal Total { get; set; }
        public bool Ativo { get; set; }

        #endregion



        #region Relacionamento
        public Usuario Usuario { get; set; }
        public Paciente Paciente { get; set; }

        public List<OrcamentoProcedimento> OrcamentosProcedimentos { get; set; }

        #endregion


        #region Construtores
        public Orcamento()
        {
            Ativo = true;
        }

        #endregion


        #region Metodos
            public void Deletar()
            {
                Ativo = false;
            }

            public void Restaurar()
            {
                Ativo = true;
            }
            
        #endregion
    }
}