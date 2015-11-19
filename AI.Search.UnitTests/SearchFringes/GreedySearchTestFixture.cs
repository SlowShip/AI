using AI.Search.SearchFringes;
using C5;
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
    public class GreedySearchTestFixture
    {
        IFixture fixture;
        Mock<IHuristic<TestState>> huristicMock; 

        public class TestState { }

        public GreedySearchTestFixture()
        {
            fixture = new Fixture().Customize(new AutoMoqCustomization());

            huristicMock = fixture.Freeze<Mock<IHuristic<TestState>>>();
        }

        [Fact]
        public void Add_WithNullNode_ThrowsAnArgumentNullException()
        {
            // Arrange
            var subject = fixture.Create<GreedySearchFringe<TestState>>();

            // Act
            Action act = () => subject.Add(null);

            // Assert
            var ex = Assert.Throws<ArgumentNullException>(act);
            Assert.Equal("node", ex.ParamName);
            Assert.NotNull(ex.Message);
        }

        [Fact]
        public void Add_WithoutNullNode_DoesNotThrowAnException()
        {
            // Arrange
            var node = new TestState();
            var subject = fixture.Create<GreedySearchFringe<TestState>>();

            // Act
            Action act = () => subject.Add(node);

            // Assert
            act();
        }

        [Fact]
        public void GetNext_WhenTheFringeIsEmpty_ShouldThrowAnInvalidOpperationException()
        {
            // Arrange
            var subject = fixture.Create<GreedySearchFringe<TestState>>();

            // Act
            Action act = () => subject.GetNext();

            // Assert
            var ex = Assert.Throws<InvalidOperationException>(act);
            Assert.NotNull(ex.Message);
        }

        [Fact]
        public void GetNext_WhenTheFringeIsNotEmpty_ShouldReturnAnAddedItem()
        {
            // Arrange
            var expected = new TestState();

            var subject = fixture.Create<GreedySearchFringe<TestState>>();
            subject.Add(expected);

            // Act
            var result = subject.GetNext();

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void GetNext_WhenTheFringeIsNotEmpty_ShouldReturnItemsOrderOfTheirHuristicValue()
        {
            // Arrange
            var in_1 = new TestState();
            var in_2 = new TestState();
            var in_3 = new TestState();
            var in_4 = new TestState();
            var in_5 = new TestState();

            huristicMock.Setup(h => h.GetHuristicValue(in_1)).Returns(4);
            huristicMock.Setup(h => h.GetHuristicValue(in_2)).Returns(5);
            huristicMock.Setup(h => h.GetHuristicValue(in_3)).Returns(2);
            huristicMock.Setup(h => h.GetHuristicValue(in_4)).Returns(1);
            huristicMock.Setup(h => h.GetHuristicValue(in_5)).Returns(3);

            var subject = fixture.Create<GreedySearchFringe<TestState>>();
            subject.Add(in_1);
            subject.Add(in_2);
            subject.Add(in_3);
            subject.Add(in_4);
            subject.Add(in_5);

            // Act
            var out_1 = subject.GetNext();
            var out_2 = subject.GetNext();
            var out_3 = subject.GetNext();
            var out_4 = subject.GetNext();
            var out_5 = subject.GetNext();

            // Assert
            Assert.Equal(out_1, in_4);
            Assert.Equal(out_2, in_3);
            Assert.Equal(out_3, in_5);
            Assert.Equal(out_4, in_1);
            Assert.Equal(out_5, in_2);
        }

        [Fact]
        public void IsEmpty_StraightAfterCreation_ShouldReturnTrue()
        {
            // Arrange
            var subject = fixture.Create<GreedySearchFringe<TestState>>();

            // Act
            var result = subject.IsEmpty();

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsEmpty_AfterSomeItemsHaveBeenAdded_ShouldReturnFalse()
        {
            // Arrange
            var subject = fixture.Create<GreedySearchFringe<TestState>>();
            subject.Add(new TestState());

            // Act
            var result = subject.IsEmpty();

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void IsEmpty_WhenTheSameNumberOfItemsHaveBeenRemovedAsWereAdded_ShouldReturnTrue()
        {
            // Arrange
            var subject = fixture.Create<GreedySearchFringe<TestState>>();

            subject.Add(new TestState());
            subject.Add(new TestState());
            subject.Add(new TestState());

            subject.GetNext();
            subject.GetNext();
            subject.GetNext();

            // Act
            var result = subject.IsEmpty();

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsEmpty_AfterMoreItemsHaveBeenAddedThanRemoved_ShouldReturnFalse()
        {
            // Arrange
            var subject = fixture.Create<GreedySearchFringe<TestState>>();

            subject.Add(new TestState());
            subject.Add(new TestState());
            subject.Add(new TestState());

            subject.GetNext();
            subject.GetNext();

            // Act
            var result = subject.IsEmpty();

            // Assert
            Assert.False(result);
        }
    }
}
