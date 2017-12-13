using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Advent.Days
{
    class Day6 : IDay
    {
        private string[] _buffer;

        public Day6(string[] inputs)
        {
            this._buffer = inputs[0].Split(new char[]{' ', '\t'}, StringSplitOptions.RemoveEmptyEntries);
        }

        public void PrintResults()
        {
            foreach(var result in DoPartAB())
            {
                System.Console.WriteLine(result);
            }
        }

        private string[] DoPartAB()
        {
            var results = new List<string>();
            var memory = new int[this._buffer.Length];
            for (var i = 0; i < memory.Length; i++)
            {
                memory[i] = int.Parse(this._buffer[i]);
            }

            bool isSimilar = false, secondaryState = false;
            int maxValue = 0, traverse = 0, targetIndex = 0, count = 0;
            var memoryMap = new List<string>();
            while (!isSimilar)
            {
                count++;

                maxValue = memory.Max();
                targetIndex = Array.IndexOf(memory, maxValue);
                traverse = targetIndex;
                memory[targetIndex] = 0;
                while (maxValue != 0)
                {
                    traverse = ++traverse % memory.Length;
                    memory[traverse]++;
                    maxValue--;
                }

                var mappedMemory = string.Join("", memory);
                isSimilar = memoryMap.Contains(mappedMemory);
                
                if (isSimilar && !secondaryState)
                {
                    results.Add(count.ToString());
                    memoryMap.Clear();
                    count = 0;
                    isSimilar = false;
                    secondaryState = true;
                }

                memoryMap.Add(mappedMemory);
            }

            results.Add(count.ToString());
            return results.ToArray();
        }
    }
}