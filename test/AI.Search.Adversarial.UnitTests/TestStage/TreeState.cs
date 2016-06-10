using System.Collections.Generic;

namespace AI.Search.Adversarial.UnitTests.TestStage
{
    public sealed class TreeState
    {
        public TreeState(string name)
        {
            Name = name;
        }
        public string Name { get; }
    }
}
