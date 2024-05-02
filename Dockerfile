# Definir a imagem base
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

# Copiar os arquivos de projeto e restaurar as dependências
COPY ["LabApp_.csproj", "."]
RUN dotnet restore "./LabApp_.csproj"

# Copiar todo o código fonte e construir o aplicativo
COPY . .
WORKDIR "/src/."
RUN dotnet build "LabApp_.csproj" -c Release -o /app/build

# Definir a etapa de publicação
FROM build AS publish
RUN dotnet publish "LabApp_.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Definir a imagem final
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LabApp_.dll"]
