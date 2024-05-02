
using LabApp_.Context;
using LabApp_.Interface;
using LabApp_.Repository;
using LabApp_.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore; // Certifique-se de que esta linha est� presente
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OfficeOpenXml;

namespace LabApp_
{
    public class Program
    {
        public static void Main(string[] args)
        {

             static void CreateSchoolsTable()
            {
                string scriptPath = "create_table.sql"; // Caminho para o arquivo SQL de criação da tabela
                string connectionString = "Data Source=labapp.db"; // String de conexão com o banco de dados

                try
                {
                    // Ler o conteúdo do script SQL
                    string script = File.ReadAllText(scriptPath);

                    // Conectar ao banco de dados SQLite
                    using (var connection = new SqliteConnection(connectionString))
                    {
                        connection.Open();

                        // Criar um comando SQL e executar o script
                        using (var command = connection.CreateCommand())
                        {
                            command.CommandText = script;
                            command.ExecuteNonQuery();
                        }
                    }

                    Console.WriteLine("Tabela Schools criada com sucesso!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro ao criar a tabela Schools: {ex.Message}");
                }
            }

             ExcelPackage.LicenseContext = LicenseContext.NonCommercial;












            CreateSchoolsTable();
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            // Adiciona inje��es de depend�ncia para servi�os
            builder.Services.AddScoped<ISchoolRepository, SchoolRepository>();
            builder.Services.AddScoped<SchoolService>();

            // Adiciona inje��es de depend�ncia para o contexto do banco de dados
            // Add SQLite DbContext
            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlite(builder.Configuration.GetConnectionString("AppDbContext"));
            });


            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Create tables if they don't exist
           // Certifique-se de que a tabela Schools seja criada
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<AppDbContext>();
                context.EnsureSchoolsTableCreated();
            }


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}