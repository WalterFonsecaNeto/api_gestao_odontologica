public abstract class BaseRepositorio
{
    protected readonly ProjetoOdontologicoContexto _contexto;

    protected BaseRepositorio(ProjetoOdontologicoContexto contexto)
    {
        _contexto = contexto;
    }
}