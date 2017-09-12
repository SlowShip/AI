using System;
using System.Collections.Generic;
using System.Linq;

namespace AI.Search.SearchFringes
{
    // Todo, maybe could be more efficient by caching huristic values
    // Todo, beam search will lead to many more items addded then retrieved, Probably want to optimse for adding
    public class BeamSearchFringe<TState> : IFringeStrategy<TState>
    {
        private readonly int _beamWidth;
        private readonly IHuristic<TState> _huristic;
        private Queue<TState> _queue;

        public BeamSearchFringe(int beamWidth, IHuristic<TState> huristic)
        {
            if (beamWidth <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(beamWidth));
            }

            if (huristic == null)
            {
                throw new ArgumentNullException(nameof(huristic));
            }

            _beamWidth = beamWidth;
            _huristic = huristic;
            _queue = new Queue<TState>();
        }

        public void Add(TState state)
        {
            if (state == null)
            {
                throw new ArgumentNullException(nameof(state));
            }
            _queue.Enqueue(state);
            EnsureQueueCountLessThanBeamWidth();
        }

        public TState GetNext()
        {
            return _queue.Dequeue();
        }

        public bool IsEmpty()
        {
            return !_queue.Any();
        }

        private void EnsureQueueCountLessThanBeamWidth()
        {
            if (_queue.Count <= _beamWidth)
            {
                return;
            }

            var minValue = int.MaxValue;
            var minStateIndex = 0;

            var newItems = _queue.ToList();
            for (var i = 0; i < _queue.Count; i++)
            {
                var s = newItems[i];
                var value = _huristic.GetHuristicValue(s);
                if (minValue > value)
                {
                    minValue = value;
                    minStateIndex = i;
                }
            }
            newItems.RemoveAt(minStateIndex);
            _queue = new Queue<TState>(newItems);
        }
    }
}
