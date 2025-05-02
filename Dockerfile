# Use the official .NET image as the base image
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

# Copy the publish output and set entrypoint
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ProjetoOdontologico.Api/ProjetoOdontologico.Api.csproj", "ProjetoOdontologico.Api/"]
COPY ["ProjetoOdontologico.Aplicacao/ProjetoOdontologico.Aplicacao.csproj", "ProjetoOdontologico.Aplicacao/"]
COPY ["ProjetoOdontologico.Repositorio/ProjetoOdontologico.Repositorio.csproj", "ProjetoOdontologico.Repositorio/"]
RUN dotnet restore "ProjetoOdontologico.Api/ProjetoOdontologico.Api.csproj"
COPY . .
WORKDIR "/src/ProjetoOdontologico.Api"
RUN dotnet build "ProjetoOdontologico.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ProjetoOdontologico.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ProjetoOdontologico.Api.dll"]
