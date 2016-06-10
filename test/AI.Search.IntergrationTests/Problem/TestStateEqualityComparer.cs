using System.Collections.Generic;

namespace AI.Search.IntergrationTests.Problem
{
    public class TestStateEqualityComparer : IEqualityComparer<TestState>
    {
        public bool Equals(TestState x, TestState y)
        {
            return x.Node == y.Node;
        }

        public int GetHashCode(TestState obj)
        {
            return obj.Node.GetHashCode();
        }
    }
}
