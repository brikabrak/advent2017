using Advent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Advent.Days
{
    class Day3 : Day
    {
        private string[] _buffer;

        public Day3(string[] inputs)
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
            var target = int.Parse(this._buffer[0]);

            var nearestSquare = 1;
            var center = 0;
            while (nearestSquare * nearestSquare < target)
            {
                center++;
                nearestSquare += 2;
            }

            var squareIndex = nearestSquare - 1;
            var targetPosition = new int[]{0, 0};
            var distance = (nearestSquare * nearestSquare) - target;
            var movements = (int)(distance / squareIndex);

            if (movements > 2)
            {
                targetPosition[1] = distance % squareIndex;
            }
            else if (movements >= 1)
            {
                targetPosition[0] = distance % squareIndex;
            }
            else
            {
                targetPosition[0] = targetPosition[1] = squareIndex;
                targetPosition[0] -= (distance % squareIndex);
            }

            targetPosition[0] -= center;
            targetPosition[1] -= center;

            return new string[]{(Math.Abs(targetPosition[0]) + Math.Abs(targetPosition[1])).ToString()};
        }

        private string[] DoPartB()
        {
            return new string[]{""};
        }
    }
}