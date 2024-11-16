using Microsoft.EntityFrameworkCore;
using ProjetoOdontologico.Repositorio;
using ProjetoOdontologico.Aplicacao;

var builder = WebApplication.CreateBuilder(args);

// Adicione aplicações
builder.Services.AddScoped<IEspecialidadeAplicacao, EspecialidadeAplicacao>();
builder.Services.AddScoped<IFormaPagamentoAplicacao,FormaPagamentoAplicacao>();
builder.Services.AddScoped<IPacienteAplicacao,PacienteAplicacao>();
builder.Services.AddScoped<IProcedimentoAplicacao,ProcedimentoAplicacao>();
builder.Services.AddScoped<IUsuarioAplicacao, UsuarioAplicacao>();


// Adicione as interfaces de banco de dados
builder.Services.AddScoped<IEspecialidadeRepositorio,EspecialidadeRepositorio>();
builder.Services.AddScoped<IFormaPagamentoRepositorio,FormaPagamentoRepositorio>();
builder.Services.AddScoped<IPacienteRepositorio,PacienteRepositorio>();
builder.Services.AddScoped<IProcedimentoRepositorio,ProcedimentoRepositorio>();
builder.Services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();



builder.Services.AddCors(options => 
{
	options.AddDefaultPolicy(builder =>
	{
		builder.WithOrigins("http://localhost:3000")
			.SetIsOriginAllowedToAllowWildcardSubdomains()
			.AllowAnyHeader()
			.AllowAnyMethod();
	});
});

builder.Services.AddControllers();

//Adicionar o serviço de banco de dados
builder.Services.AddDbContext<ProjetoOdontologicoContexto>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseCors();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
