using LearnWellUniversity.Shared.Tests.Setups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnWellUniversitry.Application.IntegrationTests.Setups
{
    public class ApplicationIntegrationTestFixture : IAsyncLifetime
    {
        public PostgresTestContainer PostgresContainer { get; private set; } = null!;


        public async Task InitializeAsync()
        {
            PostgresContainer = new PostgresTestContainer();
            
            await PostgresContainer.InitializeAsync();
        }


        public async Task DisposeAsync()
        {
            await PostgresContainer.DisposeAsync();
        }

    }
}
