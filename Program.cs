using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using QuickStartWebApi2.DBContext;
using QuickStartWebApi2.Interfaces;
using QuickStartWebApi2.Repositories;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);


//Serviço do o banco de dados
builder.Services.AddEntityFrameworkSqlServer()
                .AddDbContext<DataContext>(
                    options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Registro de repositórios
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IPessoaRepository, PessoaRepository>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// Configuração do Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Minha API",
        Version = "v1",
        Description = "Uma descrição da API",
        Contact = new OpenApiContact
        {
            Name = "Nome",
            Email = "seuemail@example.com",
            Url = new Uri("https://site.com")
        }
    });

    // Configurar os comentários XML para o Swagger
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

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
