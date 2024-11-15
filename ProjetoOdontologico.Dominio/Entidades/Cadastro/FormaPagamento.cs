namespace ProjetoOdontologico.Dominio.Entidades
{
    public class FormaPagamento
    {
        #region Propiedades
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public string Nome { get; set; }
        public bool Ativo { get; set; }

        #endregion
        
        #region Relacionamentos

        public Usuario Usuario { get; set; }
        public List<MovimentacaoFinanceira> MovimentacoesFinanceiras { get; set; }

        #endregion

        #region Construtores
        public FormaPagamento()
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