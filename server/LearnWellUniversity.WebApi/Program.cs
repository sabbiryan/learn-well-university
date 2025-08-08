using LearnWellUniversity.WebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddApiDocumentations();

builder.Services.AddApplicationServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{    
    app.UseDeveloperExceptionPage();
}

app.UseApiDocumentation();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
