namespace LearnWellUniversity.WebApi.FunctionalTests.Setups
{
    public class FunctionalTestFixture : IAsyncLifetime
    {
        public PostgresTestContainer PostgresContainer { get; private set; } = null!;
        public FunctionalTestWebApplicationFactory Factory { get; private set; } = null!;

        public async Task InitializeAsync()
        {
            PostgresContainer = new PostgresTestContainer();
            await PostgresContainer.InitializeAsync();
            
            Factory = new FunctionalTestWebApplicationFactory(PostgresContainer.ConnectionString);
        }

        public async Task DisposeAsync()
        {
            await PostgresContainer.DisposeAsync();
        }
    }
}
