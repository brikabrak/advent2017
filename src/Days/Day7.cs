using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Advent.Days
{
    class Day7 : IDay
    {
        private const string PATTERN = @"^(\w+)\s\((\d+)\)(?:[^\w]+([\w,\s]+))?";

        private string[] _buffer;
        private Dictionary<string, Program> _storage;
        private Program _baseProgram;
        private Program _unbalancedProgram;

        sealed class Program
        {
            public List<Program> Dependents { get; private set; }
            public string Name { get; private set; }
            public Program Parent { get; set; }
            public int Weight { get; set; }

            public int Height
            {
                get
                {
                    var height = 0;
                    foreach(var dependency in Dependents)
                    {
                        var dependentHeight = dependency.Height + 1;
                        if (dependentHeight > height)
                        {
                            height = dependentHeight;
                        }
                    }

                    return height;
                }
            }

            public Program TopParent
            {
                get
                {
                    if (Parent == null)
                    {
                        return this;
                    }

                    return Parent.TopParent;
                }
            }

            public Program(string name)
            {
                Name = name;
                Weight = 0;
                Dependents = new List<Program>();
            }
        }

        public Day7(string[] inputs)
        {
            this._buffer = inputs;
            this._storage = new Dictionary<string, Program>();
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
            var height = 0;
            Program foundation = null;
            foreach (var input in this._buffer)
            {
                var program = this.ParseStringForProgram(input);

                var top = program.TopParent;
                var dependentHeight = top.Height;
                if (dependentHeight > height)
                {
                    height = dependentHeight;
                    foundation = top;
                }
            }

            this._baseProgram = foundation;
            return new string[]{foundation.Name};
        }

        private string[] DoPartB()
        {
            var fullWeight = this.GetFullWeightFromBase(this._baseProgram);
            return new string[]{this._unbalancedProgram.Weight.ToString()};
        }

        private Program ParseStringForProgram(string input)
        {
            var match = Regex.Match(input, PATTERN);
            var tag = match.Groups[1].Value;
            var weight = int.Parse(match.Groups[2].Value);
            var relationships = (!string.IsNullOrEmpty(match.Groups[3].Value))
                ? match.Groups[3].Value.Split(new char[]{',', '\t', ' '}, StringSplitOptions.RemoveEmptyEntries)
                : null;

            var program = (this._storage.ContainsKey(tag)) ? this._storage[tag] : new Program(tag);
            program.Weight = weight;

            foreach (var relationship in relationships ?? new string[]{})
            {
                if (this._storage.ContainsKey(relationship))
                {
                    this._storage[relationship].Parent = program;
                    program.Dependents.Add(this._storage[relationship]);
                }
                else
                {
                    var dependentProgram = new Program(relationship);
                    dependentProgram.Parent = program;
                    program.Dependents.Add(dependentProgram);
                    this._storage[relationship] = dependentProgram;
                }
            }

            this._storage[tag] = program;

            return program;
        }

        private int GetFullWeightFromBase(Program baseProgram)
        {
            if (baseProgram.Dependents.Count == 0)
            {
                return baseProgram.Weight;
            }

            var weights = new List<int>();
            baseProgram.Dependents.ForEach(prog => weights.Add(this.GetFullWeightFromBase(prog)));
            this.BalanceDependentWeights(baseProgram, weights);

            return baseProgram.Weight + weights.Sum();
        }

        private void BalanceDependentWeights(Program baseProgram, List<int> weights)
        {
            foreach (var weight in weights)
            {
                if (weights.Count(w => w == weight) == 1)
                {
                    var index = weights.IndexOf(weight);
                    var newWeight = weights.First(w => w != weight);
                    this._unbalancedProgram = baseProgram.Dependents[index];
                    this._unbalancedProgram.Weight -= (weight - newWeight);
                    weights[index] = newWeight;

                    return;
                }
            }
        }
    }
}