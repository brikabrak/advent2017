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
            var memory = new int[this._buffer.Length];
            for (var i = 0; i < memory.Length; i++)
            {
                memory[i] = int.Parse(this._buffer[i]);
            }

            var isSimilar = false;
            var count = 0;
            var memoryMap = new List<string>();
            var targetIndex = 0;
            while (!isSimilar)
            {
                count++;

                var maxValue = memory.Max();
                targetIndex = Array.IndexOf(memory, maxValue);
                var traverse = targetIndex;
                memory[targetIndex] = 0;
                while (maxValue != 0)
                {
                    traverse = ++traverse % memory.Length;
                    memory[traverse]++;
                    maxValue--;
                }

                var mappedMemory = string.Join("", memory);
                isSimilar = (memoryMap.Contains(mappedMemory));
                memoryMap.Add(mappedMemory);
            }

            return new string[]{count.ToString()};
        }

        private string[] DoPartB()
        {
            return new string[]{};
        }
    }
}