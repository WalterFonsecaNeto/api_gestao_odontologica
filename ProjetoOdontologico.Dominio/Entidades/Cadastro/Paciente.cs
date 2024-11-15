namespace ProjetoOdontologico.Dominio.Entidades
{
    public class Paciente
    {
        #region Propiedades
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Genero { get; set; }
        public string CPF { get; set; }
        public string Endereco { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public string HistoricoMedico { get; set; }
        public bool Ativo { get; set; }

        #endregion

        #region Relacionamentos

        public Usuario Usuario { get; set; }
        public List<ContaReceber> ContasReceber { get; set; }
        public List<Orcamento> Orcamentos { get; set; }
        public List<Agendamento> Agendamentos { get; set; }
        #endregion



        #region Construtores
        public Paciente()
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