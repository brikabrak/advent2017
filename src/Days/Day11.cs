using System;
using System.Collections.Generic;
using System.Linq;

namespace Advent.Days
{
    class Day11 : IDay
    {
        private List<int> _distances;
        private string[] _buffer;

        public Day11(string[] inputs)
        {
            this._buffer = inputs;
            this._distances = new List<int>();
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
            var moves = this._buffer[0].Split(',');
            var coordinates = new int[]{0,0,0};
            var shortest = ComputeDistances(this._distances, coordinates, moves);

            return new string[]{shortest.ToString()};
        }

        private string[] DoPartB()
        {
            return new string[]{this._distances.Max().ToString()};
        }

        private int ComputeDistances(List<int> distances, int[] coordinates, string[] moves)
        {
            foreach (var move in moves)
            {
                switch(move)
                {
                    case "n": coordinates[1]++;
                    coordinates[2]--;
                    break;
                    case "ne": coordinates[0]++;
                    coordinates[2]--;
                    break;
                    case "se": coordinates[0]++;
                    coordinates[1]--;
                    break;
                    case "s": coordinates[1]--;
                    coordinates[2]++;
                    break;
                    case "sw": coordinates[0]--;
                    coordinates[2]++;
                    break;
                    default: coordinates[0]--;
                    coordinates[1]++;
                    break;
                }

                distances.Add((Math.Abs(coordinates[0]) + Math.Abs(coordinates[1]) + Math.Abs(coordinates[2])) / 2);
            }

            return distances.Last();
        }
    }
}