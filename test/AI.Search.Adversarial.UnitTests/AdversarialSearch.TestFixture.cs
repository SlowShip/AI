using AI.Search.Adversarial.UnitTests.TestStage;
using Moq;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;
using System;
using System.Linq;
using Xunit;

namespace AI.Search.Adversarial.UnitTests
{
    public class AdversarialSearchTestFixture
    {
        private readonly IFixture _fixture;
        private readonly Mock<IAdversarialSearchProblem<TreeState, TreeAction>> _problem;
        private readonly Mock<IStateEvaluator<TreeState>> _stateEvaluator;
        private readonly TestTree _tree; 

        public AdversarialSearchTestFixture()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());

            _problem = _fixture.Freeze<Mock<IAdversarialSearchProblem<TreeState, TreeAction>>>();
            _stateEvaluator = _fixture.Freeze<Mock<IStateEvaluator<TreeState>>>();
            _tree = new TestTree();
            _tree.SetupProblem(_problem, _stateEvaluator);
        }

        [Fact]
        public void RunMinimax_WithNullState_ThrowsAnArgumentNullException()
        {
            // Arrange
            var subject = _fixture.Create<AdversarialSearch<TreeState, TreeAction>>();

            // Act
            Action act = () => subject.RunMinimax(null, 1);

            // Assert
            var ex = Assert.Throws<ArgumentNullException>(act);
            Assert.Equal("startingState", ex.ParamName);
        }

        [Theory]
        [InlineData(-5)]
        [InlineData(-1)]
        public void RunMinimax_WithMaxDepthLessThanZero_ThrowsAnArgumentOutOfRangeException(int maxDepth)
        {
            // Arrange
            var subject = _fixture.Create<AdversarialSearch<TreeState, TreeAction>>();

            // Act
            Action act = () => subject.RunMinimax(new TreeState("Some State"), maxDepth);

            // Assert
            var ex = Assert.Throws<ArgumentOutOfRangeException>(act);
            Assert.Equal("maxDepth", ex.ParamName);
        }

        [Fact]
        public void RunMinimax_MaxDepthZero_ReturnsTheEvaluatedStartingState()
        {
            // Arrange
            var startingState = _tree.StartingState;
            var subject = _fixture.Create<AdversarialSearch<TreeState, TreeAction>>();

            // Act
            var result = subject.RunMinimax(startingState, 0);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(startingState, result.State);
            Assert.Equal(_tree.GetValue(startingState.Name), result.Value);
            Assert.Empty(result.ActionsToState);
        }

        [Fact]
        public void RunMinimax_MaxDepthOfOne_ReturnsTheMaximumOfTheExpandedStates()
        {
            // Arrange
            var startingState = _tree.StartingState;
            var expectedState = "C2";
            var subject = _fixture.Create<AdversarialSearch<TreeState, TreeAction>>();

            // Act
            var result = subject.RunMinimax(startingState, 1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedState, result.State.Name);
            Assert.Equal(_tree.GetValue(expectedState), result.Value);
            Assert.Equal(_tree.GetActionsToState(result.State), result.ActionsToState);
        }

        [Fact]
        public void RunMinimax_MaxDepthOfTwo_ReturnsTheCorrectAnswer()
        {
            // Arrange
            var startingState = _tree.StartingState;
            var expectedState = "H3";
            var subject = _fixture.Create<AdversarialSearch<TreeState, TreeAction>>();

            // Act
            var result = subject.RunMinimax(startingState, 2);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedState, result.State.Name);
            Assert.Equal(_tree.GetValue(expectedState), result.Value);
            Assert.Equal(_tree.GetActionsToState(result.State), result.ActionsToState);
        }

        [Fact]
        public void RunMinimax_MaxDepthOfThree_ReturnsTheCorrectAnswer()
        {
            // Arrange
            var startingState = _tree.StartingState;
            var expectedState = "X4";
            var subject = _fixture.Create<AdversarialSearch<TreeState, TreeAction>>();

            // Act
            var result = subject.RunMinimax(startingState, 3);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedState, result.State.Name);
            Assert.Equal(_tree.GetValue(expectedState), result.Value);
            Assert.Equal(_tree.GetActionsToState(result.State), result.ActionsToState);
        }

        [Fact]
        public void RunMinimax_MaxDepthGreaterThanTheTreeDepth_ReturnsTheCorrectAnswer()
        {
            // Arrange
            var startingState = _tree.StartingState;
            var expectedState = "X4";
            var subject = _fixture.Create<AdversarialSearch<TreeState, TreeAction>>();

            // Act
            var result = subject.RunMinimax(startingState, 1000);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedState, result.State.Name);
            Assert.Equal(_tree.GetValue(expectedState), result.Value);
            Assert.Equal(_tree.GetActionsToState(result.State), result.ActionsToState);
        }
    }
}
