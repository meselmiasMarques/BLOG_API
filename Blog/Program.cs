using Blog.Data;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers().ConfigureApiBehaviorOptions(options => 
    options.SuppressModelStateInvalidFilter = true);
builder.Services.AddDbContext<BlogDataContext>();


// Configuração do Swagger
builder.Services.AddSwaggerGen(c => 
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "BLOG", Version = "v1" });
});

var app = builder.Build();

// Habilita o middleware do Swagger
app.UseSwagger();

// Especifica o endpoint do Swagger JSON
app.UseSwaggerUI(c => 
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "BLOG V1");
    // Pode configurar outras opções do Swagger UI aqui, se necessário
});

app.MapControllers();
app.Run();
