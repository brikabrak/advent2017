using System;
using System.Text;

namespace Advent.Days
{
    class Day4 : IDay
    {
        private string[] _buffer;

        public Day4(string[] inputs)
        {
            this._buffer = inputs;
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
            var count = 0;
            var isValid = true;
            foreach (var pass in this._buffer)
            {
                var words = pass.Split(' ');

                for (var i = 0; i < words.Length - 1 && isValid; i++)
                {
                    for (var j = i + 1; j < words.Length && isValid; j++)
                    {
                        isValid = isValid && (words[i] != words[j]);
                    }
                }

                if(isValid)
                {
                    count++;
                }
                isValid = true;
            }

            return new string[]{count.ToString()};
        }

        private string[] DoPartB()
        {
            return new string[]{};
        }
    }
}