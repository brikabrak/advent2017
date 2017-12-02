using Advent;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Advent.Days
{
    class Day2 : Day
    {
        private string[] buffer;

        public Day2(string[] inputs)
        {
            this.buffer = inputs;
        }

        public void PrintResults()
        {
            foreach(var result in DoPartA())
            {
                System.Console.WriteLine(result);
            }

            // foreach(var result in DoPartB())
            // {
            //     System.Console.WriteLine(result);
            // }
        }

        private string[] DoPartA()
        {
            var differences = new List<int>();

            var minMaxValues = new int[2]{-1, -1};
            foreach (var input in buffer) {
                var parsedValues = input.Split(new char[]{' ', '\t'}, StringSplitOptions.RemoveEmptyEntries);
                foreach (var number in parsedValues)
                {
                    var parsedNumber = int.Parse(number);
                    if(minMaxValues[1] == -1 || minMaxValues[1] < parsedNumber)
                    {
                        minMaxValues[1] = parsedNumber;
                    }
                    if(minMaxValues[0] == -1 ||minMaxValues[0] > parsedNumber)
                    {
                        minMaxValues[0] = parsedNumber;
                    }
                }
                differences.Add(minMaxValues[1] - minMaxValues[0]);
                minMaxValues[0] = minMaxValues[1] = -1;
            }

            return new string[]{differences.Sum().ToString()};
        }

        // private string[] DoPartB()
        // {
        //     var results = new List<string>();

        //     var target = 0;
        //     var moves = 0;
        //     foreach (var input in buffer) {
        //         moves = input.Length / 2;
        //         for (var i = 0; i < input.Length; i++)
        //         {
        //             target = (moves + i) % input.Length;
        //             sum += (input[i] == input[target]) ? int.Parse(input[i].ToString()) : 0;
        //         }
        //         results.Add(sum.ToString());
        //         sum = 0;
        //     }

        //     return results.ToArray();
        // }
    }
}