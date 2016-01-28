using AI.Search.SearchFringes;
using System;
using Xunit;

namespace AI.Search.UnitTests.SearchFringes
{
    public class BreadthFirstSearchTestFixture
    {
        public class TestState { }

        [Fact]
        public void Add_WithNullState_ThrowsAnArgumentNullException()
        {
            // Arrange
            var subject = new BreadthFirstSearchFringe<TestState>();

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
            var subject = new BreadthFirstSearchFringe<TestState>();

            // Act
            Action act = () => subject.Add(state);

            // Assert
            act();
        }

        [Fact]
        public void GetNext_WhenTheFringeIsEmpty_ShouldThrowAnInvalidOpperationException()
        {
            // Arrange
            var subject = new BreadthFirstSearchFringe<TestState>();

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

            var subject = new BreadthFirstSearchFringe<TestState>();
            subject.Add(expected);

            // Act
            var result = subject.GetNext();

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void GetNext_WhenTheFringeIsNotEmpty_ShouldReturnItemsInAFirstInFirstOutOrder()
        {
            // Arrange
            var in_1 = new TestState();
            var in_2 = new TestState();
            var in_3 = new TestState();

            var subject = new BreadthFirstSearchFringe<TestState>();
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
        public void IsEmpty_StraightAfterCreation_ShouldReturnTrue()
        {
            // Arrange
            var subject = new BreadthFirstSearchFringe<TestState>();

            // Act
            var result = subject.IsEmpty();

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsEmpty_AfterSomeItemsHaveBeenAdded_ShouldReturnFalse()
        {
            // Arrange
            var subject = new BreadthFirstSearchFringe<TestState>();
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
            var subject = new BreadthFirstSearchFringe<TestState>();

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
            var subject = new BreadthFirstSearchFringe<TestState>();

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
