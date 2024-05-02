# Use a imagem base
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Copie o script para dentro do contêiner
COPY create_table.sql /app/

# Sua etapa de construção
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["LabApp_.csproj", "."]
RUN dotnet restore "./LabApp_.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "LabApp_.csproj" -c Release -o /app/build

# Sua etapa de publicação
FROM build AS publish
RUN dotnet publish "LabApp_.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Etapa final, que será usada para executar a aplicação
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LabApp_.dll"]
