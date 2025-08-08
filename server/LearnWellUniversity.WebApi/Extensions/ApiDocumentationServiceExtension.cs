using Microsoft.OpenApi.Models;

namespace LearnWellUniversity.WebApi.Extensions
{
    public static class ApiDocumentationServiceExtension
    {
        public static IServiceCollection AddApiDocumentations(this IServiceCollection services)
        {
            services.AddOpenApi();

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "LearnWellUniversity API",
                    Version = "v1",
                    Description = "API documentation for LearnWellUniversity"
                });
            });

            return services;
        }

        public static WebApplication UseApiDocumentation(this WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "LearnWellUniversity API V1");
                c.RoutePrefix = "swagger";
            });



            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();

                app.MapGet("/", context =>
                {
                    context.Response.Redirect("/swagger/index.html");
                    return Task.CompletedTask;
                });
            }           

            return app;
        }
    }
}
