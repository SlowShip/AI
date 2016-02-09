using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.Search
{
    public class NoSolutionPossibleException : Exception
    {
        public NoSolutionPossibleException() : base() { }
        public NoSolutionPossibleException(string message) : base(message) { }
    }

}
