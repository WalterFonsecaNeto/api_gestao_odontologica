using ProjetoOdontologico.Dominio.Entidades;

namespace ProjetoOdontologico.Aplicacao
{
    public interface IProcedimentoAplicacao
    {
        Task<int> CriarProcedimentoAsync(Procedimento procedimento);
        Task AtualizarProcedimentoAsync(Procedimento procedimento, int usuarioId, int procedimentoId);
        Task<Procedimento> ObterProcedimentoPorIdAsync(int procedimentoId, int usuarioId, bool ativo);
        Task DeletarProcedimentoAsync(int procedimentoId, int usuarioId);
        Task RestaurarProcedimentoAsync(int procedimentoId, int usuarioId);
        Task<IEnumerable<Procedimento>> ListarProcedimentosPorUsuarioAsync(int usuarioId, bool ativo);
    }

}