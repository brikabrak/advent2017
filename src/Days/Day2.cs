using Advent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Advent.Days
{
    class Day2 : Day
    {
        private string[] _buffer;
        private List<string[]> _cachedInput;

        public Day2(string[] inputs)
        {
            this._buffer = inputs;
            this._cachedInput = new List<string[]>();

            foreach(var input in this._buffer)
            {
                this._cachedInput.Add(input.Split(new char[]{' ', '\t'}, StringSplitOptions.RemoveEmptyEntries));
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
            var differences = new List<int>();

            var minMaxValues = new int[2]{-1, -1};
            foreach (var parsedValues in this._cachedInput)
            {
                foreach (var number in parsedValues)
                {
                    var parsedNumber = int.Parse(number);

                    minMaxValues[1] = (minMaxValues[1] == -1 || minMaxValues[1] < parsedNumber)
                        ? parsedNumber
                        : minMaxValues[1];

                    minMaxValues[0] = (minMaxValues[0] == -1 || minMaxValues[0] > parsedNumber)
                        ? parsedNumber
                        : minMaxValues[0];
                }
                differences.Add(minMaxValues[1] - minMaxValues[0]);
                minMaxValues[0] = minMaxValues[1] = -1;
            }

            return new string[]{differences.Sum().ToString()};
        }

        private string[] DoPartB()
        {
            var quotients = new List<int>();

            var quotient = 0;
            foreach (var parsedValues in this._cachedInput)
            {
                for (var i = 0; i < parsedValues.Length && quotient == 0; i++)
                {
                    var x = int.Parse(parsedValues[i]);
                    for (var j = i + 1; j < parsedValues.Length && quotient == 0; j++)
                    {
                        var y = int.Parse(parsedValues[j]);
                        quotient = (x > y && x % y == 0) 
                            ? x / y
                            : (y % x == 0)
                                ? y / x
                                : 0;
                    }
                }
                quotients.Add(quotient);
                quotient = 0;
            }

            return new string[]{quotients.Sum().ToString()};
        }
    }
}