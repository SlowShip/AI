using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AI.Search.IntergrationTests.Problem
{
    // Generates the following tree.
    //              Start
    //            /    | \
    //           2     |  3
    //          /      |   \
    //        [a]      5   [c]
    //        h=2      |   h=5
    //         |       |    /
    //         4       |   4
    //         |       |  /
    //        [d]--1--[b]
    //        h=2     h=1
    //         \       /
    //          2     5
    //           \   /     
    //            Goal

    public class TriForkTestProblem : TestProblemBase
    {
        protected override void InitaliseProblem()
        {
            var a = new Node("A", 2);
            var b = new Node("B", 1);
            var c = new Node("C", 5);
            var d = new Node("D", 2);

            ConnectNodes(StartNode, a, 2);
            ConnectNodes(StartNode, b, 5);
            ConnectNodes(StartNode, c, 3);
            ConnectNodes(a, d, 4);
            ConnectNodes(c, b, 4);
            ConnectNodes(d, b, 1);
            ConnectNodes(d, GoalNode, 2);
            ConnectNodes(b, GoalNode, 5);
        }
    }
}
