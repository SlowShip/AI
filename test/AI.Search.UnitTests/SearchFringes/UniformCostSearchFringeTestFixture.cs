using AI.Search.SearchFringes;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;
using Xunit;

namespace AI.Search.UnitTests.SearchFringes
{
    public class UniformCostSearchFringeTestFixture
    {
        IFixture fixture;

        public class TestState : ICostTracked
        {
            public int Cost { get; }

            public TestState(int cost)
            {
                Cost = cost;
            }
        }

        public UniformCostSearchFringeTestFixture()
        {
            fixture = new Fixture().Customize(new AutoMoqCustomization());
        }

        [Fact]
        public void GetQueueOrder_ShouldReturnAStatesHuristicValue()
        {
            // Arrange
            var state = fixture.Create<TestState>();
            var subject = fixture.Create<UniformCostSearchFringe<TestState>>();

            // Act
            var result = subject.GetQueueOrder(state);

            // Assert
            Assert.Equal(state.Cost, result);
        }
    }
}
