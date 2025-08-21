using LearnWellUniversity.Application.Extensions;
using LearnWellUniversity.Application.Models.Data;
using LearnWellUniversity.Infrastructure.Extensions;
using LearnWellUniversity.WebApi.Extensions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

InitializeEnvironmentSetup.Initialize(builder.Configuration);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddApiDocumentations();

builder.Services.AddHttpContextAccessor();

builder.Services.AddForwardedHeaderConfig();

builder.Services.AddInfrastructureServices();

builder.Services.RegisterApplicationServices();

builder.Services.AddJwtAuthentication();

builder.Services.AddAuthorizationPolicy();

builder.Services.AddMapsterService();

builder.Services.AddRedis();

builder.Services.AddRabbitMq();

builder.Host.EnableSeqLoggerUsingSerilog();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{    
    app.UseDeveloperExceptionPage();
    app.UseDbMigration();
    app.UseSeedData();
}

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseGlobalExceptionHandler();

app.UseApiDocumentation();

app.UseForwardedHeaders();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program;