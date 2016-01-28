using AI.Search.SearchFringes;
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
    public class PriorityQueueDrivenFringeBaseTestFixture
    {
        IFixture fixture;

        public class TestState
        {
            public int Order { get; set; }
        }

        public class TestFringe : PriorityQueueDrivenFringeBase<TestState>
        {
            public override int GetQueueOrder(TestState state)
            {
                return state.Order;
            }
        }

        public PriorityQueueDrivenFringeBaseTestFixture()
        {
            fixture = new Fixture().Customize(new AutoMoqCustomization());
        }

        [Fact]
        public void Add_WithNullNode_ThrowsAnArgumentNullException()
        {
            // Arrange
            var subject = fixture.Create<TestFringe>();

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
            var node = fixture.Create<TestState>();
            var subject = fixture.Create<TestFringe>();

            // Act
            Action act = () => subject.Add(node);

            // Assert
            act();
        }

        [Fact]
        public void GetNext_WhenTheFringeIsEmpty_ShouldThrowAnInvalidOpperationException()
        {
            // Arrange
            var subject = fixture.Create<TestFringe>();

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
            var expected = fixture.Create<TestState>();

            var subject = fixture.Create<TestFringe>();
            subject.Add(expected);

            // Act
            var result = subject.GetNext();

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void GetNext_WhenTheFringeIsNotEmpty_ShouldReturnItemsOrderOfTheirPriority()
        {
            // Arrange
            var count = 100;
            var inItems = fixture.CreateMany<TestState>(count).ToArray(); ;

            var subject = fixture.Create<TestFringe>();
            foreach (var item in inItems)
            {
                subject.Add(item);
            }

            // Act
            var outItems = Enumerable.Range(1, count).Select(x => subject.GetNext()).ToArray();

            // Assert
            var inItemsOrdered = inItems.OrderBy(i => i.Order).ToArray();

            for (int i = 0; i < count; i++)
            {
                Assert.Equal(outItems[i], inItemsOrdered[i]);
            }
        }

        [Fact]
        public void IsEmpty_StraightAfterCreation_ShouldReturnTrue()
        {
            // Arrange
            var subject = fixture.Create<TestFringe>();

            // Act
            var result = subject.IsEmpty();

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsEmpty_AfterSomeItemsHaveBeenAdded_ShouldReturnFalse()
        {
            // Arrange
            var subject = fixture.Create<TestFringe>();
            subject.Add(fixture.Create<TestState>());

            // Act
            var result = subject.IsEmpty();

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void IsEmpty_WhenTheSameNumberOfItemsHaveBeenRemovedAsWereAdded_ShouldReturnTrue()
        {
            // Arrange
            var subject = fixture.Create<TestFringe>();

            subject.Add(fixture.Create<TestState>());
            subject.Add(fixture.Create<TestState>());
            subject.Add(fixture.Create<TestState>());

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
            var subject = fixture.Create<TestFringe>();

            subject.Add(fixture.Create<TestState>());
            subject.Add(fixture.Create<TestState>());
            subject.Add(fixture.Create<TestState>());

            subject.GetNext();
            subject.GetNext();

            // Act
            var result = subject.IsEmpty();

            // Assert
            Assert.False(result);
        }
    }
}
