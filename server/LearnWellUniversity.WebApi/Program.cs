using LearnWellUniversity.Infrastructure.Extensions;
using LearnWellUniversity.WebApi.Extensions;

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

builder.Services.AddMapsterService();

builder.Host.EnableSeqLoggerUsingSerilog();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{    
    app.UseDeveloperExceptionPage();
    app.UseDbMigration();
    app.UseSeedData();
}

app.UseHttpsRedirection();

app.UseGlobalExceptionHandler();

app.UseApiDocumentation();

app.UseForwardedHeaders();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
