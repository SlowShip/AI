using System;

namespace AI.Search
{
    public class NoSolutionPossibleException : Exception
    {
        public NoSolutionPossibleException() : base() { }
        public NoSolutionPossibleException(string message) : base(message) { }
    }

}
