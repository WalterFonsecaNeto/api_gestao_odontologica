namespace ProjetoOdontologico.Dominio.Entidades
{
    public class Procedimento
    {
        #region Propiedades
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
        public int EspecialidadeId { get; set; }
        public bool Ativo { get; set; }

        #endregion



        #region Relacionamento
        public Especialidade Especialidade { get; set; }
        public Usuario Usuario { get; set; }
        public List<OrcamentoProcedimento> OrcamentosProcedimentos { get; set; }

        #endregion


        #region Construtores
        public Procedimento()
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