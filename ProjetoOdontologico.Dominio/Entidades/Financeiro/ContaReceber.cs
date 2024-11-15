namespace ProjetoOdontologico.Dominio.Entidades
{
    public class ContaReceber
    {
        #region Propiedades
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public int PacienteId { get; set; }
        public DateTime DataVencimento { get; set; }
        public decimal ValorReceber { get; set; }
        public string Status { get; set; } //! Pendente ou PAgo
        public bool Ativo { get; set; }

        #endregion



        #region Relacionamento
        public Usuario Usuario { get; set; }
        public Paciente Paciente { get; set; }

        #endregion


        #region Construtores
        public ContaReceber()
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