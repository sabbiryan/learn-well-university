using LearnWellUniversitry.Application.IntegrationTests.Setups;
using LearnWellUniversity.Application.Contracts.Auths;
using LearnWellUniversity.Application.Contracts.Bases;
using LearnWellUniversity.Application.Contracts.UoW;
using LearnWellUniversity.Application.Models.Requestes.Auths;
using LearnWellUniversity.Application.Models.Statics;
using LearnWellUniversity.Application.Services;
using LearnWellUniversity.Infrastructure.Extensions;
using LearnWellUniversity.Infrastructure.Interceptors;
using LearnWellUniversity.Infrastructure.Persistences;
using LearnWellUniversity.Infrastructure.Persistences.Seeds;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;

namespace LearnWellUniversitry.Application.IntegrationTests.Bases
{
    public abstract class ApplicationIntegrationTestBase : IClassFixture<ApplicationIntegrationTestFixture>, IAsyncDisposable
    {

        protected readonly ServiceProvider ServiceProvider = default!;
        protected readonly AppDbContext DbContext;
        protected readonly IUnitOfWork UnitOfWork;


        private TokenResponse? _token;
        private readonly IAuthService _authService;


        public ApplicationIntegrationTestBase(ApplicationIntegrationTestFixture fixture)
        {
            var services = new ServiceCollection();

            services.AddInfrastructureServices();
            services.AddRedis();
            services.AddRabbitMq();

            services.RemoveAll<DbContextOptions<AppDbContext>>();

            services.AddHttpContextAccessor();
            services.AddLogging();

            services.AddDbContext<AppDbContext>((serviceProvider, options) =>
            {
                var interceptor = serviceProvider.GetRequiredService<AuditSaveChangesInterceptor>();
                options.AddInterceptors(interceptor);

                options.UseNpgsql(fixture.PostgresContainer.ConnectionString);
                options.UseSnakeCaseNamingConvention();
            });

            var servicesNamespace = typeof(ApplicationService).Namespace!;

            services.Scan(services =>
                services.FromAssemblyOf<IApplicationService>()
                    .AddClasses(classes => classes.InNamespaces(servicesNamespace))
                    .AsImplementedInterfaces()
                    .WithScopedLifetime()
            );



            ServiceProvider = services.BuildServiceProvider();
           
            DbContext = ServiceProvider.GetRequiredService<AppDbContext>();            
            UnitOfWork = ServiceProvider.GetRequiredService<IUnitOfWork>();
            var logger = ServiceProvider.GetRequiredService<ILogger<AppDbContext>>();

            _authService = ServiceProvider.GetRequiredService<IAuthService>();

            DbContext.Database.Migrate();

            DbInitializer.Initialize(DbContext, logger);

        }



        private async Task<TokenResponse> GetToken(string email, string password)
        {
            var response = await _authService.LoginAsync(new TokenRequest(email, password), "127.0.0.1");

            return response;
        }


        protected async Task<TokenResponse> GetAdminToken()
        {
            _token = await GetToken(StaticUser.Admin.Email, StaticUser.Admin.Password);

            return _token;
        }

        protected async Task<TokenResponse> GetStaffToken()
        {
            _token = await GetToken(StaticUser.Staff.Email, StaticUser.Staff.Password);

            return _token;
        }

        protected async Task<TokenResponse> GetStudentToken()
        {
            _token = await GetToken(StaticUser.Student.Email, StaticUser.Student.Password);

            return _token;
        }


        private async Task Dispose()
        {
            if (DbContext != null)
            {
                await DbContext.DisposeAsync();
            }
        }

        public async ValueTask DisposeAsync()
        {
            GC.SuppressFinalize(this);
            await Dispose();
        }
    }
}
