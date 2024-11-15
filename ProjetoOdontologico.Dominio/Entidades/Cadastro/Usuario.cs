
namespace ProjetoOdontologico.Dominio.Entidades;

    public class Usuario
    {

        #region Propiedades
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public bool Ativo { get; set; }

        #endregion

        #region Relacionamentos
        public  List<Procedimento> Procedimentos { get; set; }
        public  List<Paciente> Pacientes { get; set; }
        public  List<Especialidade> Especialidades { get; set; }
        public  List<FormaPagamento> FormasPagamento { get; set; }
        public  List<Agendamento> Agendamentos { get; set; }
        public  List<Orcamento> Orcamentos { get; set; }
        public  List<ContaReceber> ContasReceber { get; set; }
        public  List<MovimentacaoFinanceira> MovimentacoesFinanceiras { get; set; }


        #endregion

        #region Construtores
        public Usuario()
        {
            Ativo = true;
        }
        #endregion

        #region Métodos
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
