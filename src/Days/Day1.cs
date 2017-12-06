using System.Collections.Generic;
using System.Text;

namespace Advent.Days
{
    class Day1 : IDay
    {
        private string[] buffer;
        private int sum;

        public Day1(string[] inputs)
        {
            this.buffer = inputs;
            this.sum = 0;
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
            var results = new List<string>();

            var target = 0;
            foreach (var input in buffer) {
                for (var i = 0; i < input.Length; i++)
                {
                    target = (i == input.Length - 1) ? 0 : i + 1;
                    sum += (input[i] == input[target]) ? int.Parse(input[i].ToString()) : 0;
                }
                results.Add(sum.ToString());
                sum = 0;
            }

            return results.ToArray();
        }

        private string[] DoPartB()
        {
            var results = new List<string>();

            var target = 0;
            var moves = 0;
            foreach (var input in buffer) {
                moves = input.Length / 2;
                for (var i = 0; i < input.Length; i++)
                {
                    target = (moves + i) % input.Length;
                    sum += (input[i] == input[target]) ? int.Parse(input[i].ToString()) : 0;
                }
                results.Add(sum.ToString());
                sum = 0;
            }

            return results.ToArray();
        }
    }
}