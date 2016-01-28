using AI.Search.SearchFringes;
using Moq;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;
using Xunit;

namespace AI.Search.UnitTests.SearchFringes
{
    public class GreedySearchFringeTestFixture
    {
        IFixture fixture;
        Mock<IHuristic<TestState>> huristicMock; 

        public class TestState { }

        public GreedySearchFringeTestFixture()
        {
            fixture = new Fixture().Customize(new AutoMoqCustomization());
            huristicMock = fixture.Freeze<Mock<IHuristic<TestState>>>();
        }

        [Fact]
        public void GetQueueOrder_ShouldReturnAStatesHuristicValue()
        {
            // Arrange
            var expected = fixture.Create<int>();

            var state = new TestState();
            huristicMock.Setup(h => h.GetHuristicValue(state)).Returns(expected);

            var subject = fixture.Create<GreedySearchFringe<TestState>>();

            // Act
            var result = subject.GetQueueOrder(state);

            // Assert
            Assert.Equal(expected, result);
        }
    }
}
