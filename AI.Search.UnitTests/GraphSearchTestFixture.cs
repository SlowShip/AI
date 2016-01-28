using AI.Search;
using Moq;
using Moq.Sequences;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AI.Search.UnitTests
{
    public class GraphSearchTestFixture
    {
        private IFixture fixture;

        private Mock<IProblem<TestState>> problem;
        private Mock<IFringeStrategy<TestState>> strategy;

        private TestState startState;
        private TestState goalState;

        public GraphSearchTestFixture()
        {
            fixture = new Fixture().Customize(new AutoMoqCustomization());

            problem = fixture.Create<Mock<IProblem<TestState>>>();
            strategy = fixture.Create<Mock<IFringeStrategy<TestState>>>();

            startState = fixture.Create<TestState>();
            goalState = fixture.Create<TestState>();

            strategy.Setup(strat => strat.IsEmpty()).Returns(false);

            problem.Setup(prob => prob.GetStartingState()).Returns(startState);
            problem.Setup(prob => prob.IsGoalState(It.IsAny<TestState>())).Returns(false);
            problem.Setup(prob => prob.IsGoalState(goalState)).Returns(true);
        }

        public class TestState
        {
        }
        
        [Fact]
        public void Execute_WhenProblemIsNull_ThrowsAnArgumentNullException()
        {
            // Arrange
            var subject = new GraphSearch<TestState>();

            // Act
            Action act = () => subject.Execute(null, strategy.Object);

            // Assert
            var ex = Assert.Throws<ArgumentNullException>(act);
            Assert.Equal(nameof(problem), ex.ParamName);
        }

        [Fact]
        public void Execute_WhenStrategyIsNull_ThrowsAnArgumentNullException()
        {
            // Arrange
            var subject = new GraphSearch<TestState>();

            // Act
            Action act = () => subject.Execute(problem.Object, null);

            // Assert
            var ex = Assert.Throws<ArgumentNullException>(act);
            Assert.Equal(nameof(strategy), ex.ParamName);
        }

        [Fact]
        public void Execute_AddsTheStartStateToTheFringe()
        {
            // Arrange
            problem.Setup(prob => prob.IsGoalState(It.IsAny<TestState>())).Returns(true); // avoid infinite loop

            var subject = new GraphSearch<TestState>();

            // Act
            subject.Execute(problem.Object, strategy.Object);

            // Assert
            strategy.Verify(strat => strat.Add(startState), Times.Once());
        }

        [Fact]
        public void Execute_WhenTheStartStateIsAGoalState_DoesNotExpandAnyStates()
        {
            // Arrange
            var subject = new GraphSearch<TestState>();

            strategy.Setup(strat => strat.GetNext()).Returns(startState);
            problem.Setup(prob => prob.IsGoalState(startState)).Returns(true);

            // Act
            subject.Execute(problem.Object, strategy.Object);

            // Assert
            problem.Verify(prob => prob.GetSuccessors(It.IsAny<TestState>()), Times.Never());
        }

        [Fact]
        public void Execute_WhenTheStartStateIsNotAGoalState_ExpandsTheStartState()
        {
            // Arrange
            var subject = new GraphSearch<TestState>();
            
            strategy.SetupSequence(strat => strat.GetNext())
                .Returns(startState)
                .Returns(goalState);

            // Act
            subject.Execute(problem.Object, strategy.Object);

            // Assert
            problem.Verify(prob => prob.GetSuccessors(startState), Times.Once());
        }

        [Fact]
        public void Execute_WhenTheStartStateIsNotAGoalState_AddsItsSucessorsToTheFringe()
        {
            // Arrange
            var succesors = fixture.CreateMany<TestState>(3).ToArray();
            strategy.SetupSequence(strat => strat.GetNext())
                .Returns(startState)
                .Returns(succesors[0])
                .Returns(succesors[1])
                .Returns(succesors[2])
                .Returns(goalState);

            problem.Setup(prob => prob.GetSuccessors(startState)).Returns(succesors);

            var subject = new GraphSearch<TestState>();

            // Act
            subject.Execute(problem.Object, strategy.Object);

            // Assert
            strategy.Verify(strat => strat.Add(succesors[0]), Times.Once());
            strategy.Verify(strat => strat.Add(succesors[1]), Times.Once());
            strategy.Verify(strat => strat.Add(succesors[2]), Times.Once());
        }

        [Fact]
        public void Execute_ExpandsStatesInOrderOfTheirAppearanceOffTheFringe()
        {
            // Arrange
            var stateBuilder = fixture.Build<TestState>();
            var s1 = stateBuilder.Create();
            var s2 = stateBuilder.Create();
            var s3 = stateBuilder.Create();

            strategy.SetupSequence(strat => strat.GetNext())
                .Returns(startState)
                .Returns(s1)
                .Returns(s2)
                .Returns(s3)
                .Returns(goalState);

            using (Sequence.Create())
            {
                problem.Setup(prob => prob.GetSuccessors(startState)).InSequence().Returns(fixture.CreateMany<TestState>());
                problem.Setup(prob => prob.GetSuccessors(s1)).InSequence().Returns(fixture.CreateMany<TestState>());
                problem.Setup(prob => prob.GetSuccessors(s2)).InSequence().Returns(fixture.CreateMany<TestState>());
                problem.Setup(prob => prob.GetSuccessors(s3)).InSequence().Returns(fixture.CreateMany<TestState>());

                var subject = new GraphSearch<TestState>();

                // Act
                subject.Execute(problem.Object, strategy.Object);

                // Assert
                problem.Verify();
            }
        }

        [Fact]
        public void Execute_AddsExpandedStatesToTheFringe()
        {
            // Arrange
            var stateBuilder = fixture.Build<TestState>();
            var s1 = stateBuilder.Create();
            var s2 = stateBuilder.Create();
            var s3 = stateBuilder.Create();
            
            strategy.SetupSequence(strat => strat.GetNext())
                .Returns(startState)
                .Returns(s1)
                .Returns(s2)
                .Returns(s3)
                .Returns(goalState);

            problem.Setup(prob => prob.GetSuccessors(startState)).Returns(new[] { s1 });
            problem.Setup(prob => prob.GetSuccessors(s1)).Returns(new[] { s2, s3 });
            problem.Setup(prob => prob.GetSuccessors(s2)).Returns(fixture.CreateMany<TestState>());
            problem.Setup(prob => prob.GetSuccessors(s3)).Returns(new[] { goalState });

            using (Sequence.Create())
            {
                strategy.Setup(strat => strat.Add(s1)).InSequence();
                strategy.Setup(strat => strat.Add(s2)).InSequence();
                strategy.Setup(strat => strat.Add(s3)).InSequence();
                strategy.Setup(strat => strat.Add(goalState)).InSequence();

                var subject = new GraphSearch<TestState>();

                // Act
                subject.Execute(problem.Object, strategy.Object);

                // Assert
                strategy.VerifyAll();
            }
        }

        [Fact]
        public void Execute_ChecksForGoalStatesInOrderOfTheirAppearanceOffTheFringe()
        {
            // Arrange
            var stateBuilder = fixture.Build<TestState>();
            var s1 = stateBuilder.Create();
            var s2 = stateBuilder.Create();
            var s3 = stateBuilder.Create();

            strategy.SetupSequence(strat => strat.GetNext())
                .Returns(s1)
                .Returns(s2)
                .Returns(s3);

            using (Sequence.Create())
            {
                problem.Setup(prob => prob.IsGoalState(s1)).InSequence().Returns(false);
                problem.Setup(prob => prob.IsGoalState(s2)).InSequence().Returns(false);
                problem.Setup(prob => prob.IsGoalState(s3)).InSequence().Returns(true);

                var subject = new GraphSearch<TestState>();

                // Act
                subject.Execute(problem.Object, strategy.Object);

                // Assert
                problem.Verify();
            }
        }

        [Fact]
        public void Execute_WhenTheFringeBecomesEmptyWithNoGoalStateFound_ThrowsANoSolutionPossibleException()
        {
            // Arrange
            strategy.SetupSequence(strat => strat.IsEmpty())
                .Returns(false)
                .Returns(false)
                .Returns(true);

            var subject = new GraphSearch<TestState>();

            // Act
            Action act = () => subject.Execute(problem.Object, strategy.Object);

            // Assert
            var ex = Assert.Throws<NoSolutionPossibleException>(act);
            Assert.NotNull(ex.Message);
        }

        [Fact]
        public void Execute_ReturnsTheFirstGoalStateOffTheFringe()
        {
            // Arrange
            var nodeBuilder = fixture.Build<TestState>();
            var secondGoal = nodeBuilder.Create();

            strategy.SetupSequence(strat => strat.GetNext())
                .Returns(nodeBuilder.Create())
                .Returns(nodeBuilder.Create())
                .Returns(goalState)
                .Returns(nodeBuilder.Create())
                .Returns(secondGoal);

            problem.Setup(prob => prob.IsGoalState(secondGoal)).Returns(true);

            var subject = new GraphSearch<TestState>();

            // Act
            var result = subject.Execute(problem.Object, strategy.Object);

            // Assert
            Assert.Equal(goalState, result);
        }

        [Fact]
        public void Execute_DoesNotExpandNodesAfterTheGoalStateIsFound()
        {
            // Arrange
            var afterGoal = fixture.Create<TestState>();

            strategy.SetupSequence(strat => strat.GetNext())
                .Returns(startState)
                .Returns(goalState)
                .Returns(afterGoal);

            var subject = new GraphSearch<TestState>();

            // Act
            subject.Execute(problem.Object, strategy.Object);

            // Assert
            problem.Verify(prob => prob.IsGoalState(afterGoal), Times.Never);
            problem.Verify(prob => prob.GetSuccessors(afterGoal), Times.Never);
        }

        [Fact]
        public void Execute_WhenTheSameStateOccursMultipleTimes_OnlyExpandsItOnce()
        {
            // Arrange
            var someState = fixture.Create<TestState>();

            strategy.SetupSequence(strat => strat.GetNext())
                .Returns(startState)
                .Returns(someState)
                .Returns(someState)
                .Returns(goalState);

            var subject = new GraphSearch<TestState>();

            // Act
            subject.Execute(problem.Object, strategy.Object);

            // Assert
            problem.Verify(prob => prob.IsGoalState(someState), Times.Once);
            problem.Verify(prob => prob.GetSuccessors(someState), Times.Once);
        }

        [Fact]
        public void Execute_WhenTheSameStateOccursMultipleTimesAccordingToTheStateEqualityComparer_OnlyExpandsItOnce()
        {
            // Arrange
            var someState = fixture.Create<TestState>();
            var someOtherState = fixture.Create<TestState>();
            var equalToSomeState = fixture.Create<TestState>();

            var equalityComparer = fixture.Create<Mock<IEqualityComparer<TestState>>>();
            equalityComparer.Setup(comparer => comparer.Equals(It.IsAny<TestState>(), It.IsAny<TestState>())).Returns(false);
            equalityComparer.Setup(comparer => comparer.Equals(someState, equalToSomeState)).Returns(true);

            strategy.SetupSequence(strat => strat.GetNext())
                .Returns(startState)
                .Returns(someState)
                .Returns(someOtherState)
                .Returns(equalToSomeState)
                .Returns(goalState);

            var subject = new GraphSearch<TestState>();

            // Act
            subject.Execute(problem.Object, strategy.Object, equalityComparer.Object);

            // Assert
            problem.Verify(prob => prob.IsGoalState(equalToSomeState), Times.Never);
            problem.Verify(prob => prob.GetSuccessors(equalToSomeState), Times.Never);
        }
    }
}
