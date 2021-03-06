﻿using System.Collections.Generic;

namespace AI.Search.IntergrationTests.Problem
{
    public class Node
    {
        public string Name { get; }
        public int HuristicValue { get; }
        public List<NodeConnection> Connections { get; } = new List<NodeConnection>();

        public Node(string name, int huristicValue)
        {
            Name = name;
            HuristicValue = huristicValue;
        }
    }
}
