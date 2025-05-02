using Microsoft.EntityFrameworkCore;
using ProjetoOdontologico.Dominio.Entidades;
using ProjetoOdontologico.Repositorio.Configurations;

public class ProjetoOdontologicoContexto : DbContext
{
    #region Atributos
    
    private string _stringConexao = "Host=dpg-d0a2u4buibrs73anoe20-a.oregon-postgres.render.com;Database=postgresql_projeto_odontologico;Username=postgresql_projeto_odontologico_user;Password=ZuYAQWGZGLmPAppPO4GE8GGGytsjaD7P;Port=5432;SSL Mode=Require;Trust Server Certificate=true;";
    private DbContextOptions _options; //Uma variavel vazia do tipo DbContextoptions que vai receber uma option

    #endregion



    #region Propiedades
    public DbSet<Paciente> Pacientes { get; set; }
    public DbSet<FormaPagamento> FormasPagamento { get; set; }
    public DbSet<Procedimento> Procedimentos { get; set; }
    public DbSet<Especialidade> Especialidades { get; set; }
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Agendamento> Agendamentos { get; set; }
    public DbSet<Orcamento> Orcamentos { get; set; }
    public DbSet<ContaReceber> ContasReceber { get; set; }
    public DbSet<MovimentacaoFinanceira> MovimentacoesFinanceiras { get; set; }
    public DbSet<OrcamentoProcedimento> OrcamentosProcedimentos { get; set; }


    #endregion

    public ProjetoOdontologicoContexto()
    {
    }

    public ProjetoOdontologicoContexto(DbContextOptions<ProjetoOdontologicoContexto> options) : base(options)
    {
        _options = options;
    }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (_options == null)
        {
            optionsBuilder.UseNpgsql(_stringConexao);
        }

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new PacienteConfiguration());
        modelBuilder.ApplyConfiguration(new FormaPagamentoConfiguration());
        modelBuilder.ApplyConfiguration(new ProcedimentoConfiguration());
        modelBuilder.ApplyConfiguration(new EspecialidadeConfiguration());
        modelBuilder.ApplyConfiguration(new UsuarioConfiguration());
        modelBuilder.ApplyConfiguration(new AgendamentoConfiguration());
        modelBuilder.ApplyConfiguration(new OrcamentoConfiguration());
        modelBuilder.ApplyConfiguration(new ContaReceberConfiguration());
        modelBuilder.ApplyConfiguration(new MovimentacaoFinanceiraConfiguration());
        modelBuilder.ApplyConfiguration(new OrcamentoProcedimentoConfiguration());

    }
}