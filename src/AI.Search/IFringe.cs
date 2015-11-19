using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.Search
{
    public interface IFringeStrategy<TNode>
    {
        bool IsEmpty();
        TNode GetNext();
        void Add(TNode node);
    }
}
