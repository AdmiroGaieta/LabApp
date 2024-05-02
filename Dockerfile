FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["LabApp_.csproj", "."]
RUN dotnet restore "./LabApp_.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "LabApp_.csproj" -c Release -o /app/build

# Adicionando a instrução COPY para copiar o arquivo create_table.sql do GitHub
# Certifique-se de usar o URL raw do arquivo
COPY https://raw.githubusercontent.com/AdmiroGaieta/LabApp/master/create_table.sql /app/create_table.sql

FROM build AS publish
RUN dotnet publish "LabApp_.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LabApp_.dll"]
