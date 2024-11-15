namespace ProjetoOdontologico.Dominio.Entidades
{
    public class OrcamentoProcedimento

    {
        #region Propiedades
        public int Id { get; set; }
        public int OrcamentoId { get; set; }
        public int ProcedimentoId { get; set; }
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
        public bool Ativo { get; set; }

        #endregion



        #region Relacionamento

        public Orcamento Orcamento { get; set; }

        public Procedimento Procedimento { get; set; }

        #endregion


        #region Construtores
        public OrcamentoProcedimento()
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