using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.Search.IntergrationTests.Problem
{
    public abstract class TestProblemBase : IProblem<TestState>
    {
        protected Node StartNode { get; } = new Node("Start", int.MaxValue);
        protected Node GoalNode { get; } = new Node("Goal", 0);

        public List<TestState> Expanded { get; } = new List<TestState>();

        public TestProblemBase()
        {
            InitaliseProblem();
        }

        protected abstract void InitaliseProblem();

        protected void ConnectNodes(Node first, Node second, int cost)
        {
            var firstToSecond = new NodeConnection(second, cost);
            var secondToFirst = new NodeConnection(first, cost);

            first.Connections.Add(firstToSecond);
            second.Connections.Add(secondToFirst);
        }

        public TestState GetStartingState()
        {
            return new TestState(0, StartNode, new List<Node>() { StartNode });
        }

        public IEnumerable<TestState> GetSuccessors(TestState state)
        {
            Expanded.Add(state);

            return state
                .Node
                .Connections
                .Select(connection => new TestState(state.Cost + connection.Cost, connection.To, state.Path.Concat(new[] { connection.To })))
                .OrderBy(s => s.Node.Name);
        }

        public bool IsGoalState(TestState state)
        {
            return state.Node == GoalNode;
        }
    }
}
