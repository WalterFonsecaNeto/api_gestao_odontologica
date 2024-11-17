# Defina a imagem base
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

# Defina a imagem para o build
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

# Copie o arquivo .csproj e restaure as dependências
COPY ["ProjetoOdontologico.Api/ProjetoOdontologico.Api.csproj", "ProjetoOdontologico.Api/"]
RUN dotnet restore "ProjetoOdontologico.Api/ProjetoOdontologico.Api.csproj"

# Copie o restante dos arquivos e construa o projeto
COPY . .
WORKDIR "/src/ProjetoOdontologico.Api"
RUN dotnet build "ProjetoOdontologico.Api.csproj" -c Release -o /app/build

# Publique a aplicação para uma pasta específica
RUN dotnet publish "ProjetoOdontologico.Api.csproj" -c Release -o /app/publish

# Defina a imagem final e copie os arquivos publicados
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "ProjetoOdontologico.Api.dll"]
