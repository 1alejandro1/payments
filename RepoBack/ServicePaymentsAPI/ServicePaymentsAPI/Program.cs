using Microsoft.Identity.Client;
using Microsoft.OpenApi.Models;
using PAY.CROSS.DATAACCESS;
using PAY.CROSS.DATAACCESS.Repositorie;
using PAY.CROSS.LOGGER;
using ServicePaymentsAPI;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.Configure<DBOptions>(builder.Configuration.GetSection("DBOptions"));
builder.Services.AddScoped<IDataAccess, DataAccess>();
builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
builder.Services.AddScoped<IPaymentService, PaymentService>();
builder.Services.AddScoped<PAY.CROSS.LOGGER.ILogger>(provider =>
{
    var config = provider.GetRequiredService<IConfiguration>();
    var logFilePath = config.GetValue<string>("Logs:Path_Log_File") ?? "logs/app.log";
    var logLevel = config.GetValue<string>("Logs:Level") ?? "ERROR";
    return new Logger(logFilePath, logLevel);
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "ServicePaymentsAPI",
        Version = "v1"
    });

    //c.AddSecurityDefinition("Token", new OpenApiSecurityScheme
    //{
    //    Name = "Token",
    //    In = ParameterLocation.Header,
    //    Type = SecuritySchemeType.ApiKey,
    //    Description = "Token de autenticación (colocar el valor aquí)"
    //});

    //c.AddSecurityDefinition("Channel", new OpenApiSecurityScheme
    //{
    //    Name = "Channel",
    //    In = ParameterLocation.Header,
    //    Type = SecuritySchemeType.ApiKey,
    //    Description = "Canal de origen (colocar el valor aquí)"
    //});
       

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
