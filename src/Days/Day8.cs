using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Advent.Days
{
    class Day8 : IDay
    {
        private string[] _buffer;
        private Dictionary<string, int> _registers;

        public Day8(string[] inputs)
        {
            this._buffer = inputs;
            this._registers = new Dictionary<string, int>();
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
            foreach(var input in this._buffer)
            {
                this.PerformInstructions(input);
            }

            return new string[]{this._registers.Max(p => p.Value).ToString()};
        }

        private string[] DoPartB()
        {
            var maxValue = 0;
            this._registers.Clear();

            foreach(var input in this._buffer)
            {
                this.PerformInstructions(input);

                var largest = this._registers.Max(p => p.Value);
                if(largest > maxValue)
                {
                    maxValue = largest;
                }
            }

            return new string[]{maxValue.ToString()};
        }

        private void PerformInstructions(string input)
        {
            var instructions = input.Split(' ');

            this._registers.TryAdd(instructions[0], 0);
            this._registers.TryAdd(instructions[4], 0);

            if (this.PerformCondition(instructions[4], instructions[5], int.Parse(instructions[6])))
            {
                var value = int.Parse(instructions[2]);
                this._registers[instructions[0]] += (instructions[1] == "inc") ? value : value * -1;
            }
        }

        private bool PerformCondition(string register, string condition, int numeric)
        {
            switch (condition[0])
            {
                case '>': return (condition.Length > 1)
                    ? (this._registers[register] >= numeric)
                    : (this._registers[register] > numeric);
                case '<': return (condition.Length > 1)
                    ? (this._registers[register] <= numeric)
                    : (this._registers[register] < numeric);
                case '!': return (this._registers[register] != numeric);
                default: return (this._registers[register] == numeric);
            }
        }
    }
}