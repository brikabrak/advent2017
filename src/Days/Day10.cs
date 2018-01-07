using System.Collections.Generic;

namespace Advent.Days
{
    class Day10 : IDay
    {
        private const int LENGTH = 256;

        sealed class Node
        {
            public Node Previous {get; set;}
            public Node Next {get; set;}
            public int Value {get; set;}

            public Node(int value)
            {
                this.Value = value;
            }
        }

        private string[] _buffer;
        private Node _head;

        public Day10(string[] inputs)
        {
            this._buffer = inputs;

            this._head = new Node(0);
            var a = this._head;
            var b = this._head;
            for(var i = 1; i < LENGTH; i++)
            {
                a = new Node(i);
                a.Previous = b;
                b.Next = a;
                b = a;
            }
            this._head.Previous = b;
            b.Next = this._head;
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
            var input = this._buffer[0].Split(',');
            var index = this._head;
            var skipCount = 0;
            foreach(var move in input)
            {
                var moves = int.Parse(move);
                var count = moves;
                var start = index;
                while (--count > 0)
                {
                    index = index.Next;
                }

                Reverse(moves, start, index);

                count = 1 + skipCount++;
                while (count-- > 0)
                {
                    index = index.Next;
                }
            }

            return new string[]{(this._head.Value * this._head.Next.Value).ToString()};
        }

        private string[] DoPartB()
        {
            return new string[]{};
        }

        private void Reverse(int moves, Node a, Node b)
        {
            if (moves <= 1)
            {
                return;
            }

            var t = b.Value;
            b.Value = a.Value;
            a.Value = t;

            Reverse(moves - 2, a.Next, b.Previous);
        }
    }
}