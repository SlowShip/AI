using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.Search.IntergrationTests.Problem
{
    public class TestStateHuristic : IHuristic<TestState>
    {
        public int GetHuristicValue(TestState state)
        {
            return state.Node.HuristicValue;
        }
    }
}
