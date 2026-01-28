using Orleans.TestingHost;

namespace UserService.Domain.Tests
{

    public sealed class TestSiloConfigurator : ISiloConfigurator
    {
        public void Configure(ISiloBuilder siloBuilder)
        {
            siloBuilder
                .AddMemoryGrainStorageAsDefault();
        }
    }

    public class UserGrainTests
    {
        private readonly IGrainFactory _grainFactory;

        public UserGrainTests()
        {
            var builder = new TestClusterBuilder();
            builder.AddSiloBuilderConfigurator<TestSiloConfigurator>();
            var cluster = builder.Build();
            cluster.Deploy();

            _grainFactory = cluster.GrainFactory;
        }

        [Fact]
        public async Task Constructor_WhenUserCreated_ShouldBeTrue()
        {
            var userGrain = _grainFactory.GetGrain<IUserGrain>(Guid.NewGuid());

            var userName = Username.GenerateRandom();

            var dateTimeBefore = DateTime.UtcNow;
            await userGrain.Create(userName);
            var user = await userGrain.Get();
            var dateTimeAfter = DateTime.UtcNow;

            Assert.Equal(userName.Value, user.Username.Value);
            Assert.Equal(1, user.Version);
            Assert.True(user.CreatedAt >= dateTimeBefore && user.CreatedAt <= dateTimeAfter);
            Assert.Equal(Gender.Unknown(), user.Gender);
            Assert.Null(user.Name);
            Assert.Null(user.UpdatedAt);
            Assert.Empty(user.Media);
            Assert.False(user.IsDeleted);
        }
    }
}
