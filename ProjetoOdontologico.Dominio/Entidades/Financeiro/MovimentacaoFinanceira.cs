namespace ProjetoOdontologico.Dominio.Entidades
{
    public class MovimentacaoFinanceira
    {
        #region Propiedades
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public string TipoMovimento { get; set; } //! Entrada ou Saida
        public decimal Valor { get; set; }
        public DateTime DataMovimentacao { get; set; }
        public string Descricao { get; set; } 
        public int FormaPagamentoId { get; set; } 
        public bool Ativo { get; set; }

        #endregion



        #region Relacionamento
        public Usuario Usuario { get; set; }
        public FormaPagamento FormaPagamento { get; set; }

        #endregion


        #region Construtores
        public MovimentacaoFinanceira()
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