using Microsoft.EntityFrameworkCore;
using ProjetoOdontologico.Dominio.Entidades;

namespace ProjetoOdontologico.Repositorio
{
    public class UsuarioRepositorio : BaseRepositorio, IUsuarioRepositorio
    {
        public UsuarioRepositorio(ProjetoOdontologicoContexto contexto) : base(contexto)
        {
        }

        public async Task<int> SalvarAsync(Usuario usuario)
        {
            await _contexto.Usuarios.AddAsync(usuario);
            await _contexto.SaveChangesAsync();

            return usuario.Id;
        }

        public async Task AtualizarAsync(Usuario usuario)
        {
            _contexto.Usuarios.Update(usuario);
            await _contexto.SaveChangesAsync();
        }

        public async Task<Usuario> ObterPorIdAsync(int usuarioID, bool ativo)
        {
            return await _contexto.Usuarios
                .Where(u => u.Ativo == ativo)
                .FirstOrDefaultAsync(u => u.Id == usuarioID);
        }

        public async Task DeletarAsync(Usuario usuario)
        {
            usuario.Deletar();
            _contexto.Usuarios.Update(usuario);
            await _contexto.SaveChangesAsync();
        }

        public async Task RestaurarAsync(Usuario usuario)
        {
            usuario.Restaurar();
            _contexto.Usuarios.Update(usuario);
            await _contexto.SaveChangesAsync();
        }

        public async Task<IEnumerable<Usuario>> ListarAsync(bool ativo)
        {
            return await _contexto.Usuarios
                .Where(u => u.Ativo == ativo)
                .ToListAsync();
        }

        public async Task<Usuario> ValidarUsuario(Usuario usuario, bool ativo)
        {
            return await _contexto.Usuarios
                .FirstOrDefaultAsync(u => u.Email == usuario.Email && u.Senha == usuario.Senha && u.Ativo == ativo);
        }
        
    }
}
