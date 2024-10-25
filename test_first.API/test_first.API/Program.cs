using Microsoft.EntityFrameworkCore;
using Serilog;
using test_first.Application.Services;
using test_first.DataAccess;
using test_first.DataAccess.Repositories;
using test_first.Infrastructure;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddAutoMapper(typeof(MapperProfile));

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "My API", Version = "v1" });
});

builder.Services.AddDbContext<PostgreDbContext>(
    options =>
    {
        options.UseNpgsql(builder.Configuration.GetConnectionString("OnionTestDbContext"));
    });


builder.Host.UseSerilog();

builder.Host.UseSerilog((context, services, configuration) =>
        configuration.ReadFrom.Configuration(context.Configuration)
            .MinimumLevel.Information()
            .WriteTo.Console()
            .WriteTo.File("logs/app.log"));


builder.Services.AddScoped<IDeliveryRepository, DeliveryRepository>();
builder.Services.AddScoped<IDeliveryService, DeliveryService>();
builder.Services.AddScoped<IDeliveryResultReportsRepository, DeliveryResultReportsRepository>();
builder.Services.AddScoped<IInputDataValidator, InputDataValidator>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My Service V1");
    });
}



app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
