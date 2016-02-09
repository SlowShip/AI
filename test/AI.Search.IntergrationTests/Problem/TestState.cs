using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.Search.IntergrationTests.Problem
{
    public class TestState : ICostTracked
    {
        public int Cost { get; }
        public int HuristicValue { get; }
        public Node Node { get; }
        public List<Node> Path { get; }

        public TestState(int cost, Node node, IEnumerable<Node> path)
        {
            Cost = cost;
            HuristicValue = node.HuristicValue;
            Node = node;
            Path = new List<Node>(path);
        }
    }
}
