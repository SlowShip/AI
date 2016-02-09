using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.Search.IntergrationTests.Problem
{
    public class NodeConnection
    {
        public Node To { get; }
        public int Cost { get; }

        public NodeConnection(Node to, int cost)
        {
            To = to;
            Cost = cost;
        }
    }
}
