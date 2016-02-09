using System;
using System.Collections.Generic;
using Xunit;
using System.Linq;
using AI.Search.SearchFringes;
using AI.Search.IntergrationTests.Problem;

namespace AI.Search.IntergrationTests
{
    public class GraphSearchIntergrationTests
    {
        // Todo - need to make terminology clear we don't repeate nodes rather than states 
        [Fact]
        public void DepthFirstGraphSearch()
        {
            // Arrange
            var problem = new TriForkTestProblem();
            var search = new GraphSearch<TestState>(new DepthFirstSearchFringe<TestState>(), new TestStateEqualityComparer());

            // Act
            var result = search.Execute(problem);

            // Assert
            var expanded = problem.Expanded.Select(s => s.Node.Name);
            var resultPath = result.Path.Select(n => n.Name);

            Assert.Equal(new[] { "Start", "C", "B" }, expanded);
            Assert.Equal(new[] { "Start", "C", "B", "Goal" }, resultPath);
        }

        [Fact]
        public void BreadthFirstGraphSearch()
        {
            // Arrange
            var problem = new TriForkTestProblem();
            var search = new GraphSearch<TestState>(new BreadthFirstSearchFringe<TestState>(), new TestStateEqualityComparer());

            // Act
            var result = search.Execute(problem);

            // Assert
            var expanded = problem.Expanded.Select(s => s.Node.Name);
            var resultPath = result.Path.Select(n => n.Name);

            Assert.Equal(new[] { "Start", "A", "B", "C", "D" }, expanded);
            Assert.Equal(new[] { "Start", "B", "Goal" }, resultPath);
        }

        [Fact]
        public void UniformCostGraphSearch()
        {
            // Arrange
            var problem = new TriForkTestProblem();
            var search = new GraphSearch<TestState>(new UniformCostSearchFringe<TestState>(), new TestStateEqualityComparer());

            // Act
            var result = search.Execute(problem);

            // Assert
            var expanded = problem.Expanded.Select(s => s.Node.Name);
            var resultPath = result.Path.Select(n => n.Name);

            Assert.Equal(new[] { "Start", "A", "C", "B", "D" }, expanded);
            Assert.Equal(new[] { "Start", "A", "D", "Goal" }, resultPath);
        }

        [Fact]
        public void GreedyGraphSearch()
        {
            // Arrange
            var problem = new TriForkTestProblem();
            var search = new GraphSearch<TestState>(new GreedySearchFringe<TestState>(new TestStateHuristic()), new TestStateEqualityComparer());

            // Act
            var result = search.Execute(problem);

            // Assert
            var expanded = problem.Expanded.Select(s => s.Node.Name);
            var resultPath = result.Path.Select(n => n.Name);

            Assert.Equal(new[] { "Start", "B" }, expanded);
            Assert.Equal(new[] { "Start", "B", "Goal" }, resultPath);
        }

        [Fact]
        public void AStarGraphSearch()
        {
            // Arrange
            var problem = new TriForkTestProblem();
            var search = new GraphSearch<TestState>(new AStarSearchFringe<TestState>(new TestStateHuristic()), new TestStateEqualityComparer());

            // Act
            var result = search.Execute(problem);

            // Assert
            var expanded = problem.Expanded.Select(s => s.Node.Name);
            var resultPath = result.Path.Select(n => n.Name);

            Assert.Equal(new[] { "Start", "A", "B", "C", "D" }, expanded);
            Assert.Equal(new[] { "Start", "A", "D", "Goal" }, resultPath);
        }
    }
}
