using System.Text.Json;
using System.Text.Json.Serialization;
using AutoMapper;
using CalculateTaxes.CrossCutting.DependencyInjection;
using CalculateTaxes.CrossCutting.Mappings;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Events;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(swg =>
{
    swg.SwaggerDoc("v1", new OpenApiInfo { Title = "Tier.API", Version = "1.0" });
});

var config = new MapperConfiguration(cfg =>
{
    cfg.AddProfile(new DtoToEntityProfile());
    cfg.AddProfile(new EntityToDtoProfile());
});

IMapper mapper = config.CreateMapper();

builder.Services.AddSingleton(mapper);

builder.Services.AddControllers()         
    .AddJsonOptions(options =>
    {   
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        options.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
    ConnectionMultiplexer.Connect(Environment.GetEnvironmentVariable("REDIS_CONNECTION")!) 
);


ConfigureRepository.ConfigureDependenciesRepository(builder.Services);
ConfigureService.ConfigureDependenciesService(builder.Services);

builder.Host.UseSerilog((context, services, configuration) =>
        {
            configuration
                .MinimumLevel.Information()
                .MinimumLevel.Override("Serilog.AspNetCore.RequestLoggingMiddleware", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.AspNetCore.Mvc.Infrastructure", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.AspNetCore.Routing.EndpointMiddleware", LogEventLevel.Warning)
                .WriteTo.Console(outputTemplate: "[{Level:u3}] - {Timestamp:dd-MM-yyyy-HH:mm:ss} - {Message:lj}{NewLine}{Exception}");
        });

var app = builder.Build();

if (!app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI(swg => {
        swg.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1");
    }); 
}


if (!app.Environment.IsProduction())
    app.MapSwagger();

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.UseSerilogRequestLogging();

app.MapControllers();

app.Run();
