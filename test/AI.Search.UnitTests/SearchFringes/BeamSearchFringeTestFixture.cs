using AI.Search.SearchFringes;
using Moq;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;
using System;
using Xunit;

namespace AI.Search.UnitTests.SearchFringes
{
    public class BeamSearchFringeTestFixture
    {
        IFixture _fixture;
        Mock<IHuristic<TestState>> _huristicMock;

        public class TestState { }

        public BeamSearchFringeTestFixture()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());
            _huristicMock = _fixture.Freeze<Mock<IHuristic<TestState>>>();
        }

        private BeamSearchFringe<TestState> CreateSubject(int beamWidth = 5)
        {
            return new BeamSearchFringe<TestState>(beamWidth, _huristicMock.Object);
        }
        
        private TestState CreateTestState(int value)
        {
            var state = new TestState();
            _huristicMock.Setup(h => h.GetHuristicValue(state)).Returns(value);
            return state;
        } 

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public void Ctor_WithBeamWidthLessThanOrEqualToZero_ThrowsAnArgumentOutOfRangeExpection(int beamWidth)
        {
            // Act
            Action act = () => new BeamSearchFringe<TestState>(beamWidth, _huristicMock.Object);

            // Assert
            var ex = Assert.Throws<ArgumentOutOfRangeException>(act);
            Assert.Equal("beamWidth", ex.ParamName);
            Assert.NotNull(ex.Message);
        }

        [Fact]
        public void Ctor_WithNullHuristic_ThrowsAnArgumentNullExpection()
        {
            // Act
            Action act = () => new BeamSearchFringe<TestState>(10, null);

            // Assert
            var ex = Assert.Throws<ArgumentNullException>(act);
            Assert.Equal("huristic", ex.ParamName);
            Assert.NotNull(ex.Message);
        }

        [Fact]
        public void Add_WithNullState_ThrowsAnArgumentNullException()
        {
            // Arrange
            var subject = CreateSubject();

            // Act
            Action act = () => subject.Add(null);

            // Assert
            var ex = Assert.Throws<ArgumentNullException>(act);
            Assert.Equal("state", ex.ParamName);
            Assert.NotNull(ex.Message);
        }

        [Fact]
        public void Add_WithoutNullState_DoesNotThrowAnException()
        {
            // Arrange
            var state = new TestState();
            var subject = CreateSubject();

            // Act
            Action act = () => subject.Add(state);

            // Assert
            act();
        }

        [Fact]
        public void GetNext_WhenTheFringeIsEmpty_ShouldThrowAnInvalidOpperationException()
        {
            // Arrange
            var subject = CreateSubject();

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

            var subject = CreateSubject(5);
            subject.Add(expected);

            // Act
            var result = subject.GetNext();

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void GetNext_WhenTheFringeIsNotEmptyAndTheMaximumNumberOfAddedStatesHasNotExceedTheBeamWidth_ShouldReturnItemsInAFirstInFirstOutOrder()
        {
            // Arrange
            var in_1 = new TestState();
            var in_2 = new TestState();
            var in_3 = new TestState();

            var subject = CreateSubject();
            subject.Add(in_1);
            subject.Add(in_2);
            subject.Add(in_3);

            // Act
            var out_1 = subject.GetNext();
            var out_2 = subject.GetNext();
            var out_3 = subject.GetNext();

            // Assert
            Assert.Equal(out_1, in_1);
            Assert.Equal(out_2, in_2);
            Assert.Equal(out_3, in_3);
        }

        [Fact]
        public void GetNext_WhenTheFringeSizeHasExceededTheBeamWidth_AndWeHaveSubsequentlyRemovedAllTheKeptItems_ShouldThrowAnInvalidOpperationException()
        {
            // Arrange
            var in_1 = new TestState();
            var in_2 = new TestState();
            var in_3 = new TestState();

            var subject = CreateSubject(beamWidth: 2);
            subject.Add(in_1);
            subject.Add(in_2);
            subject.Add(in_3);

            subject.GetNext();
            subject.GetNext();

            // Act
            Action act = () => subject.GetNext();

            // Assert
            var ex = Assert.Throws<InvalidOperationException>(act);
            Assert.NotNull(ex.Message);
        }

        [Fact]
        public void GetNext_WhenTheFringeSizeHasExceededTheBeamWidth_ShouldReturnTheItemsWithTheHighestHuristicValueInAFirstInFirstOutOrder()
        {
            // Arrange
            var in_1 = CreateTestState(10);
            var in_2 = CreateTestState(1);
            var in_3 = CreateTestState(9);
            var in_4 = CreateTestState(3);
            var in_5 = CreateTestState(11);

            var subject = CreateSubject(beamWidth: 3);
            subject.Add(in_1);
            subject.Add(in_2);
            subject.Add(in_3);
            subject.Add(in_4);
            subject.Add(in_5);

            // Act
            var out_1 = subject.GetNext();
            var out_2 = subject.GetNext();
            var out_3 = subject.GetNext();

            // Assert
            Assert.Equal(in_1, out_1);
            Assert.Equal(in_3, out_2);
            Assert.Equal(in_5, out_3);
        }

        [Fact]
        public void IsEmpty_StraightAfterCreation_ShouldReturnTrue()
        {
            // Arrange
            var subject = CreateSubject();

            // Act
            var result = subject.IsEmpty();

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsEmpty_AfterSomeItemsHaveBeenAdded_ShouldReturnFalse()
        {
            // Arrange
            var subject = CreateSubject();
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
            var subject = CreateSubject();

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
        public void IsEmpty_AfterMoreItemsHaveBeenAddedThanRemovedAndTheMaxNumberOfItemsWasLessThanTheBeamWidth_ShouldReturnFalse()
        {
            // Arrange
            var subject = CreateSubject();

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

        [Fact]
        public void IsEmpty_AfterTheBeamWidthsNumberOfItemsHaveBeenRemoved_ShouldReturnTrue()
        {
            // Arrange
            var subject = CreateSubject(beamWidth: 3);

            subject.Add(new TestState());
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
    }
}
