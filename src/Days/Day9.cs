using System.Collections.Generic;

namespace Advent.Days
{
    class Day9 : IDay
    {
        private string[] _buffer;
        private bool _negate;
        private bool _garbageEnabled;
        private int _score;
        private Stack<char>[] _stacks;

        public Day9(string[] inputs)
        {
            this._buffer = inputs;
            this._negate = this._garbageEnabled = false;
            this._stacks = new Stack<char>[]
            {
                new Stack<char>(),
                new Stack<char>()
            };
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
            var inputs = this._buffer[0].ToCharArray();
            var groupLevel = 0;
            foreach (var input in inputs)
            {
                if (this._negate)
                {
                    this._negate = false;
                }
                else
                {
                    switch(input)
                    {
                        case '!': this._negate = true;
                        break;
                        case '{':
                        if (!this._garbageEnabled)
                        {
                            this._stacks[0].Push(input);
                            groupLevel++;
                            this._score += groupLevel;
                        }
                        break;
                        case '}':
                        if (!this._garbageEnabled)
                        {
                            this._stacks[0].Pop();
                            groupLevel--;
                        }
                        break;
                        case '<': this._stacks[1].Push(input);
                        this._garbageEnabled = true;
                        break;
                        case '>': this._stacks[1].Pop();
                        this._garbageEnabled = false;
                        break;
                        default: break;
                    }
                }
            }

            return new string[]{this._score.ToString()};
        }

        private string[] DoPartB()
        {
            return new string[]{};
        }
    }
}