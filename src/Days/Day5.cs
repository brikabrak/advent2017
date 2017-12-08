using System;
using System.Collections.Generic;
using System.Text;

namespace Advent.Days
{
    class Day5 : IDay
    {
        private string[] _buffer;
        private LinkedList<int> _cache;

        public Day5(string[] inputs)
        {
            this._buffer = inputs;
            this._cache = new LinkedList<int>();
        }

        private void Setup()
        {
            this._cache.Clear();
            foreach (var input in this._buffer)
            {
                this._cache.AddLast(int.Parse(input));
            }
        }

        public void PrintResults()
        {
            foreach(var result in DoPartA())
            {
                System.Console.WriteLine(result);
            }

            foreach(var result in DoPartB())
            {
                System.Console.WriteLine(result);
            }
        }

        private string[] DoPartA()
        {
            Setup();

            var pointer = this._cache.First;
            var stepCount = 0;
            var step = 0;
            while (pointer != null)
            {
                step = pointer.Value;
                stepCount++;
                
                var traverse = pointer;
                while (traverse != null && step != 0)
                {
                    if (step < 0)
                    {
                        traverse = traverse.Previous;
                        step++;
                    }
                    else if (step > 0)
                    {
                        traverse = traverse.Next;
                        step--;
                    }
                }

                pointer.Value++;
                pointer = traverse;
            }

            return new string[]{stepCount.ToString()};
        }

        private string[] DoPartB()
        {
            Setup();

            var pointer = this._cache.First;
            var stepCount = 0;
            var step = 0;
            while (pointer != null)
            {
                step = pointer.Value;
                stepCount++;
                
                var traverse = pointer;
                while (traverse != null && step != 0)
                {
                    if (step < 0)
                    {
                        traverse = traverse.Previous;
                        step++;
                    }
                    else if (step > 0)
                    {
                        traverse = traverse.Next;
                        step--;
                    }
                }

                pointer.Value += (pointer.Value >= 3) ? -1 : 1;
                pointer = traverse;
            }

            return new string[]{stepCount.ToString()};
        }
    }
}