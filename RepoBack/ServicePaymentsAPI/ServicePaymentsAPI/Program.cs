using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "ServicePaymentsAPI",
        Version = "v1"
    });

    c.AddSecurityDefinition("Token", new OpenApiSecurityScheme
    {
        Name = "Token",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Description = "Token de autenticación (colocar el valor aquí)"
    });

    c.AddSecurityDefinition("Channel", new OpenApiSecurityScheme
    {
        Name = "Channel",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Description = "Canal de origen (colocar el valor aquí)"
    });
       

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
    {
        c.IncludeXmlComments(xmlPath);
    }
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "ServicePaymentsAPI v1");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
