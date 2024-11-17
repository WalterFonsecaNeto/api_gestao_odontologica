# Use uma imagem do .NET SDK para buildar o projeto
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app

# Copie os arquivos de projeto e restaure as dependências
COPY *.csproj ./
RUN dotnet restore

# Copie o restante dos arquivos e compile a aplicação
COPY . ./
RUN dotnet publish -c Release -o out

# Use uma imagem do .NET Runtime para rodar a aplicação
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build-env /app/out .

# Comando de start da aplicação
ENTRYPOINT ["dotnet", "ProjetoOdontologico.Api.dll"]
