using Advent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Advent.Days
{
    class Day3 : IDay
    {
        private enum Directions
        {
            Right,
            Up,
            Down,
            Left
        }

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
            var target = int.Parse(this._buffer[0]);
            var mappedValues = new Dictionary<string, int>()
            {
                {"00", 1}
            };

            var passes = 2;
            var moveLimit = 1;
            var moveCount = 0;
            var direction = Directions.Right;
            var position = new int[]{0,0};
            while (mappedValues[(position[0]).ToString() + (position[1]).ToString()] < target)
            {
                switch (direction)
                {
                    case Directions.Right:
                    position[0]++;
                    break;
                    case Directions.Up:
                    position[1]++;
                    break;
                    case Directions.Left:
                    position[0]--;
                    break;
                    case Directions.Down:
                    position[1]--;
                    break;
                }

                mappedValues.Add(
                    (position[0]).ToString() + (position[1]).ToString(),
                    GetValue(mappedValues, position)
                );

                if (++moveCount == moveLimit)
                {
                    passes--;
                    direction = GetNextDirection(direction);
                    moveCount = 0;
                }

                if (passes == 0)
                {
                    passes = 2;
                    moveLimit++;
                }
            }


            return new string[]{mappedValues.Last().Value.ToString()};
        }

        private int GetValue(Dictionary<string, int> mappedValues, int[] position)
        {
            var gridIndices = new string[]
            {
                (position[0]-1).ToString() + (position[1]-1).ToString(),
                position[0].ToString() + (position[1]-1).ToString(),
                (position[0]+1).ToString() + (position[1]-1).ToString(),
                (position[0]-1).ToString() + position[1].ToString(),
                (position[0]+1).ToString() + position[1].ToString(),
                (position[0]-1).ToString() + (position[1]+1).ToString(),
                position[0].ToString() + (position[1]+1).ToString(),
                (position[0]+1).ToString() + (position[1]+1).ToString(),
            };

            var sum = 0;
            foreach (var gridIndex in gridIndices)
            {
                if (mappedValues.ContainsKey(gridIndex))
                {
                    sum += mappedValues[gridIndex];
                }
            }

            return sum;
        }

        private string GetKey(int[] position)
        {
            return position[0].ToString() + position[1].ToString();
        }

        private Directions GetNextDirection(Directions direction)
        {
            switch(direction)
            {
                case Directions.Right: return Directions.Up;
                case Directions.Up: return Directions.Left;
                case Directions.Left: return Directions.Down;
                default: return Directions.Right;
            }            
        }
    }
}