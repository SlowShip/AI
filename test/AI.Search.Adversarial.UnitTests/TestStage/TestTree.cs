using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.Search.Adversarial.UnitTests.TestStage
{
    public class TestTree
    {
        //                           _____________________________________A1___________________________________
        //                          /                                     |                                    \  
        //               __________A2__________                ___________B2__________                ___________C2_________
        //              /          |           \              /           |           \              /           |          \   
        //            A3           B3           C3           D3           E3           F3           G3           H3          I3
        //         /  |  \      /  |  \      /  |  \      /  |  \      /  |  \      /  |  \      /  |  \      /  |  \      /    \
        //        A4  B4  C4   D4  E4  F4   G4  H4  I4   J4  K4  L4   M4  N4  O4   P4  Q4  R4   S4  T4  U4   V4  W4  X4   Y4     Z4 
        private static readonly Dictionary<string, decimal> _values = new Dictionary<string, decimal>()
        {
            // Tier 1
            { "A1", 5.0m },

            // Tier 2
            { "A2", 5.0m }, { "B2", 2.0m }, { "C2", 10.0m },
            
            // Tier 3
            { "A3", 5.0m }, { "B3", 4.0m }, { "C3", 6.0m },
            { "D3", 2.0m }, { "E3", 1.0m }, { "F3", 3.0m },
            { "G3", 10.0m }, { "H3", 9.0m }, { "I3", 11.0m },

            // Tier 4
            { "A4", 5.0m }, { "B4", 4.0m }, { "C4", 6.0m },
            { "D4", 4.0m }, { "E4", 3.0m }, { "F4", 5.0m },
            { "G4", 6.0m }, { "H4", 5.0m }, { "I4", 7.0m },

            { "J4", 2.0m }, { "K4", 1.0m }, { "L4", 3.0m },
            { "M4", 1.0m }, { "N4", 0.0m }, { "O4", 2.0m },
            { "P4", 3.0m }, { "Q4", 2.0m }, { "R4", 4.0m },

            { "S4", 10.0m }, { "T4", 9.0m }, { "U4", 11.0m },
            { "V4", 9.0m }, { "W4", 8.0m }, { "X4", 10.0m },
            { "Y4", 11.0m }, { "Z4", 10.0m }
        };
        private readonly Dictionary<TreeState, List<TreeStateSuccessor>> _stateSuccessors = new Dictionary<TreeState, List<TreeStateSuccessor>>();
        private readonly Dictionary<TreeState, IEnumerable<TreeAction>> _stateActions = new Dictionary<TreeState, IEnumerable<TreeAction>>();

        public TreeState StartingState { get; private set; }

        public TestTree()
        {
            InitTree();
        }

        public void SetupProblem(Mock<IAdversarialSearchProblem<TreeState, TreeAction>> problem,
            Mock<IStateEvaluator<TreeState>> stateEvaluator)
        {
            stateEvaluator.Setup(evaluator => evaluator.GetValue(StartingState)).Returns(GetValue(StartingState.Name)); 
            foreach(var kvp in _stateSuccessors)
            {
                var parentState = kvp.Key;
                var successors = kvp.Value;
                problem.Setup(x => x.GetAvaliableActions(parentState)).Returns(successors.Select(s => s.ActionTaken));
                foreach(var successor in successors)
                {
                    problem.Setup(prob => prob.GetResultOfAction(parentState, successor.ActionTaken)).Returns(successor.ResultingState);
                    stateEvaluator.Setup(evaluator => evaluator.GetValue(successor.ResultingState)).Returns(successor.ValueOfResultingState);
                }
            }
        }

        public decimal GetValue(string stateName)
        {
            return _values[stateName];
        }

        public IEnumerable<TreeAction> GetActionsToState(TreeState state)
        {
            return _stateActions[state];
        }

        private void InitTree()
        {
            // Tier 1
            var root = new TreeState("A1");
            StartingState = root;
            _stateActions[root] = new List<TreeAction>();

            // Tier 2
            var A2 = new TreeState("A2");
            var B2 = new TreeState("B2");
            var C2 = new TreeState("C2");
            InitSuccessors(root, A2, B2, C2);

            // Tier 3
            var A3 = new TreeState("A3");
            var B3 = new TreeState("B3");
            var C3 = new TreeState("C3");
            InitSuccessors(A2, A3, B3, C3);

            var D3 = new TreeState("D3");
            var E3 = new TreeState("E3");
            var F3 = new TreeState("F3");
            InitSuccessors(B2, D3, E3, F3);

            var G3 = new TreeState("G3");
            var H3 = new TreeState("H3");
            var I3 = new TreeState("I3");
            InitSuccessors(C2, G3, H3, I3);

            // Tier 4
            var A4 = new TreeState("A4");
            var B4 = new TreeState("B4");
            var C4 = new TreeState("C4");
            InitSuccessors(A3, A4, B4, C4);

            var D4 = new TreeState("D4");
            var E4 = new TreeState("E4");
            var F4 = new TreeState("F4");
            InitSuccessors(B3, D4, E4, F4);

            var G4 = new TreeState("G4");
            var H4 = new TreeState("H4");
            var I4 = new TreeState("I4");
            InitSuccessors(C3, G4, H4, I4);

            var J4 = new TreeState("J4");
            var K4 = new TreeState("K4");
            var L4 = new TreeState("L4");
            InitSuccessors(D3, J4, K4, L4);

            var M4 = new TreeState("M4");
            var N4 = new TreeState("N4");
            var O4 = new TreeState("O4");
            InitSuccessors(E3, M4, N4, O4);

            var P4 = new TreeState("P4");
            var Q4 = new TreeState("Q4");
            var R4 = new TreeState("R4");
            InitSuccessors(F3, P4, Q4, R4);

            var S4 = new TreeState("S4");
            var T4 = new TreeState("T4");
            var U4 = new TreeState("U4");
            InitSuccessors(G3, S4, T4, U4);

            var V4 = new TreeState("V4");
            var W4 = new TreeState("W4");
            var X4 = new TreeState("X4");
            InitSuccessors(H3, V4, W4, X4);

            var Y4 = new TreeState("Y4");
            var Z4 = new TreeState("Z4");
            InitSuccessors(I3, null, Y4, Z4);
        }

        private void InitSuccessors(TreeState parent, TreeState left, TreeState middle, TreeState right)
        {
            var successors = new List<TreeStateSuccessor>();
            if(left != null)
            {
                successors.Add(new TreeStateSuccessor(parent, TreeAction.Left, left, _values[left.Name]));
                _stateActions.Add(left, Enumerable.Concat(_stateActions[parent], new[] { TreeAction.Left }));
            }

            if (middle != null)
            {
                successors.Add(new TreeStateSuccessor(parent, TreeAction.Middle, middle, _values[middle.Name]));
                _stateActions.Add(middle, Enumerable.Concat(_stateActions[parent], new[] { TreeAction.Middle }));
            }

            if (right != null)
            {
                successors.Add(new TreeStateSuccessor(parent, TreeAction.Right, right, _values[right.Name]));
                _stateActions.Add(right, Enumerable.Concat(_stateActions[parent], new[] { TreeAction.Right }));
            }

            _stateSuccessors.Add(parent, successors);
        }
    }
}
