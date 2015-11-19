using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.Search
{
    public interface IHuristic<TState>
    {
        int GetHuristicValue(TState state);
    }
}
