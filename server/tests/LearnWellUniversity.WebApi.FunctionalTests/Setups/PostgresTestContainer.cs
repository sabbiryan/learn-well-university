using Testcontainers.PostgreSql;

namespace LearnWellUniversity.WebApi.FunctionalTests.Setups
{
    public class PostgresTestContainer : IAsyncLifetime
    {
        public PostgreSqlContainer Container { get; private set; } = null!;
        public string ConnectionString => Container.GetConnectionString();

        public PostgresTestContainer()
        {
            Container = new PostgreSqlBuilder()
              .WithImage("postgres:latest")
              .WithDatabase("learn_well_university_test_db")
              .WithUsername("postgres")
              .WithPassword("postgres")
              .WithCleanUp(true)
              .Build();
        }


        public async Task InitializeAsync()
        {          
            await Container.StartAsync();
        }

        public async Task DisposeAsync()
        {
            await Container.StopAsync();
        }
    }
}
