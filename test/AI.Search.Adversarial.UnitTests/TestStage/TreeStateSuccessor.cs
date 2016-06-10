using System.Collections.Generic;

namespace AI.Search.Adversarial.UnitTests.TestStage
{
    public sealed class TreeStateSuccessor
    {
        public TreeStateSuccessor(TreeState parent, TreeAction action, TreeState result, decimal valueOfResultingState)
        {
            ParentState = parent;
            ActionTaken = action;
            ResultingState = result;
            ValueOfResultingState = valueOfResultingState;
        }

        public TreeAction ActionTaken { get; }
        public TreeState ParentState { get; }
        public TreeState ResultingState { get; }
        public decimal ValueOfResultingState { get; }
    }
}
