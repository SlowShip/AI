using AI.Search.SearchFringes;
using Moq;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AI.Search.UnitTests.SearchFringes
{
    public class AStarSearchFringeTestFixture
    {
        IFixture fixture;
        Mock<IHuristic<TestState>> huristicMock;

        public class TestState : ICostTracked
        {
            public int Cost { get; }

            public TestState(int cost)
            {
                Cost = cost;
            }
        }

        public AStarSearchFringeTestFixture()
        {
            fixture = new Fixture().Customize(new AutoMoqCustomization());
            huristicMock = fixture.Freeze<Mock<IHuristic<TestState>>>();
        }

        [Fact]
        public void GetQueueOrder_ShouldReturnAStatesHuristicValue()
        {
            // Arrange
            var huristicVal = fixture.Create<int>();

            var state = new TestState(huristicVal);
            huristicMock.Setup(h => h.GetHuristicValue(state)).Returns(huristicVal);

            var subject = fixture.Create<AStarSearchFringe<TestState>>();

            // Act
            var result = subject.GetQueueOrder(state);

            // Assert
            Assert.Equal(state.Cost + huristicVal, result);
        }
    }
}
