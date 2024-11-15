namespace ProjetoOdontologico.Dominio.Entidades
{
    public class Agendamento
    {
        #region Propiedades
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public DateTime DataHora { get; set; }
        public string Status { get; set; }
        public int PacienteId { get; set; }
        public string Descricao { get; set; }
        public bool Ativo { get; set; }

        #endregion



        #region Relacionamento
        public Usuario Usuario { get; set; }
        public Paciente Paciente { get; set; }

    
        #endregion


        #region Construtores
        public Agendamento()
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