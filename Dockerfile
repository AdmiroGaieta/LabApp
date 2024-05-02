FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Criação do arquivo create_table.sql diretamente no Dockerfile
RUN echo "CREATE TABLE IF NOT EXISTS Schools (" > /app/create_table.sql && \
    echo "    Id INTEGER PRIMARY KEY AUTOINCREMENT," >> /app/create_table.sql && \
    echo "    Name TEXT NOT NULL," >> /app/create_table.sql && \
    echo "    Email TEXT NOT NULL," >> /app/create_table.sql && \
    echo "    NumberOfClassrooms INTEGER NOT NULL," >> /app/create_table.sql && \
    echo "    Province TEXT NOT NULL" >> /app/create_table.sql && \
    echo ");" >> /app/create_table.sql

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["LabApp_.csproj", "."]
RUN dotnet restore "./LabApp_.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "LabApp_.csproj" -c Release -o /app/build

# Copie o arquivo create_table.sql localmente
COPY create_table.sql /app/create_table.sql

FROM build AS publish
RUN dotnet publish "LabApp_.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LabApp_.dll"]
