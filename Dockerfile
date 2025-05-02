# Use the official .NET image as the base image
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

# Copy the publish output and set entrypoint
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

# Copiar os arquivos csproj de todos os projetos
COPY ["ProjetoOdontologico.Api/ProjetoOdontologico.Api.csproj", "ProjetoOdontologico.Api/"]
COPY ["ProjetoOdontologico.Aplicacao/ProjetoOdontologico.Aplicacao.csproj", "ProjetoOdontologico.Aplicacao/"]
COPY ["ProjetoOdontologico.Repositorio/ProjetoOdontologico.Repositorio.csproj", "ProjetoOdontologico.Repositorio/"]
COPY ["ProjetoOdontologico.Dominio/ProjetoOdontologico.Dominio.csproj", "ProjetoOdontologico.Dominio/"]

# Restaurar as dependências de todos os projetos
RUN dotnet restore "ProjetoOdontologico.Api/ProjetoOdontologico.Api.csproj"

# Copiar o código fonte
COPY . .

# Build do projeto
WORKDIR "/src/ProjetoOdontologico.Api"
RUN dotnet build "ProjetoOdontologico.Api.csproj" -c Release -o /app/build

# Publish para produção
FROM build AS publish
RUN dotnet publish "ProjetoOdontologico.Api.csproj" -c Release -o /app/publish

# Finaliza a imagem com o ambiente de execução
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ProjetoOdontologico.Api.dll"]
