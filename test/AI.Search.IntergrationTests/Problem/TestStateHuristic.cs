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
