using Microsoft.EntityFrameworkCore;
using ProjetoOdontologico.Dominio.Entidades;

namespace ProjetoOdontologico.Repositorio
{
    public class OrcamentoProcedimentoRepositorio : BaseRepositorio, IOrcamentoProcedimentoRepositorio
    {
        public OrcamentoProcedimentoRepositorio(ProjetoOdontologicoContexto contexto) : base(contexto)
        {
        }

        public async Task<int> SalvarAsync(OrcamentoProcedimento orcamentoProcedimento)
        {
            await _contexto.OrcamentosProcedimentos.AddAsync(orcamentoProcedimento);
            await _contexto.SaveChangesAsync();

            return orcamentoProcedimento.Id;
        }

        public async Task AtualizarAsync(OrcamentoProcedimento orcamentoProcedimento)
        {
            _contexto.OrcamentosProcedimentos.Update(orcamentoProcedimento);
            await _contexto.SaveChangesAsync();
        }

        public async Task<OrcamentoProcedimento> ObterPorIdAsync(int orcamentoProcedimentoID, bool ativo)
        {
            return await _contexto.OrcamentosProcedimentos
                .Where(op => op.Ativo == ativo)
                .FirstOrDefaultAsync(op => op.Id == orcamentoProcedimentoID);
        }

        public async Task DeletarAsync(OrcamentoProcedimento orcamentoProcedimento)
        {
            orcamentoProcedimento.Deletar();
            _contexto.OrcamentosProcedimentos.Update(orcamentoProcedimento);
            await _contexto.SaveChangesAsync();
        }

        public async Task RestaurarAsync(OrcamentoProcedimento orcamentoProcedimento)
        {
            orcamentoProcedimento.Restaurar();
            _contexto.OrcamentosProcedimentos.Update(orcamentoProcedimento);
            await _contexto.SaveChangesAsync();
        }

        public async Task<IEnumerable<OrcamentoProcedimento>> ListarAsync(bool ativo)
        {
            return await _contexto.OrcamentosProcedimentos
                .Where(op => op.Ativo == ativo)
                .ToListAsync();
        }
        
        public async Task<IEnumerable<OrcamentoProcedimento>> ListarPorOrcamentoAsync(int orcamentoId, bool ativo)
        {
            return await _contexto.OrcamentosProcedimentos
                .Where(op => op.Ativo == ativo && op.OrcamentoId == orcamentoId)
                .ToListAsync();
        }
    }
}
