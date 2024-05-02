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
