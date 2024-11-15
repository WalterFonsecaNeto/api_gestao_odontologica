namespace ProjetoOdontologico.Dominio.Entidades
{
    public class Especialidade
    {
        #region Propiedades

        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public string Nome { get; set; }
        public bool Ativo { get; set; }
        #endregion


        #region Relacionamentos
        public List<Procedimento> Procedimentos { get; set; }

        public Usuario Usuario  { get; set; }

        #endregion


        #region Construtores
        public Especialidade()
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