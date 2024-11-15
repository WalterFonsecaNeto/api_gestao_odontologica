using ProjetoOdontologico.Dominio.Entidades;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjetoOdontologico.Repositorio
{
    public interface IContaReceberRepositorio
    {
        Task<int> SalvarAsync(ContaReceber contaReceber);
        Task AtualizarAsync(ContaReceber contaReceber);
        Task<ContaReceber> ObterPorIdAsync(int contaReceberID, int UsuarioId, bool ativo);
        Task DeletarAsync(ContaReceber contaReceber);
        Task RestaurarAsync(ContaReceber contaReceber);
        Task<IEnumerable<ContaReceber>> ListarAsync(int UsuarioId, bool ativo);
        Task<IEnumerable<ContaReceber>> ListarPorPacienteAsync(int UsuarioId, int PacienteId, bool ativo);
    }
}
