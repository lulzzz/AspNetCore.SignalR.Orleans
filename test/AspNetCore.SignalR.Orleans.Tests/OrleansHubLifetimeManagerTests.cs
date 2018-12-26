using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using Orleans.Messaging.SignalR;
using Xunit;
using Xunit.Abstractions;

namespace AspNetCore.SignalR.Orleans.Tests
{
    public partial class OrleansHubLifetimeManagerTests : IClassFixture<TestClusterFixture>
    {
        private readonly TestClusterFixture _fixture;
        private readonly ITestOutputHelper _output;

        public OrleansHubLifetimeManagerTests(TestClusterFixture fixture, ITestOutputHelper output)
        {
            _fixture = fixture;
            _output = output;
        }

        public OrleansHubLifetimeManager<THub> CreateNewHubLifetimeManager<THub>() where THub : Hub
        {
            var options = new OrleansOptions<THub> { ClusterClient = _fixture.TestCluster.Client };

            return new OrleansHubLifetimeManager<THub>(Options.Create(options),
                new HubProxy<THub>(options.ClusterClient),
                NullLogger<OrleansHubLifetimeManager<THub>>.Instance);
        }
    }
}
