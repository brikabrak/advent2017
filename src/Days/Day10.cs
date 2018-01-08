using System;
using System.Collections.Generic;
using System.Text;

namespace Advent.Days
{
    class Day10 : IDay
    {
        private const int LENGTH = 256;
        private const string SUFFIX = "17,31,73,47,23";

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
        }

        private void BuildNodes(int size)
        {
            this._head = new Node(0);
            Node a = this._head, b = this._head;

            for (var i = 1; i < size; i++)
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
            BuildNodes(LENGTH);
            foreach(var result in DoPartA())
            {
                System.Console.WriteLine(result);
            }

            BuildNodes(LENGTH);
            foreach(var result in DoPartB())
            {
                System.Console.WriteLine(result);
            }
        }

        private string[] DoPartA()
        {
            var input = Array.ConvertAll(this._buffer[0].Split(','), int.Parse);
            var index = this._head;
            var skipCount = 0;

            PerformKnot(input, ref index, ref skipCount);

            return new string[]{(this._head.Value * this._head.Next.Value).ToString()};
        }

        private string[] DoPartB()
        {
            var input = Array.ConvertAll(GetASCIIValues(this._buffer[0]), b => (int)b);
            var index = this._head;
            var skipCount = 0;

            for (var i = 0; i < 64; i++)
            {
                skipCount = PerformKnot(input, ref index, ref skipCount);
            }

            var hex = new StringBuilder();
            var bitwise = 0;
            var count = 0;
            index = this._head;
            while (count++ != LENGTH)
            {
                bitwise = bitwise ^ index.Value;
                if (count % 16 == 0)
                {
                    var value = bitwise.ToString("X");
                    hex.Append((value.Length != 2) ? "0" + value : value);
                    bitwise = 0;
                }
                index = index.Next;
            }

            return new string[]{hex.ToString()};
        }

        private int PerformKnot(int[] lengths, ref Node position, ref int skipCount)
        {
            Node start = position, index = position;
            var count = 0;
            foreach (var move in lengths)
            {
                count = move;
                start = index;
                index = MoveForward(index, count - 1);

                ReverseRange(move, start, index);

                count = 1 + skipCount++;
                index = MoveForward(index, count);
            }

            position = index;
            return skipCount;
        }

        private Node MoveForward(Node node, int moves)
        {
            while (moves-- > 0)
            {
                node = node.Next;
            }

            return node;
        }

        private void ReverseRange(int moves, Node a, Node b)
        {
            if (moves <= 1)
            {
                return;
            }

            var t = b.Value;
            b.Value = a.Value;
            a.Value = t;

            ReverseRange(moves - 2, a.Next, b.Previous);
        }

        private byte[] GetASCIIValues(string input)
        {
            var ascii = new List<byte>();
            foreach (var b in System.Text.Encoding.UTF8.GetBytes(input.ToCharArray()))
            {
                ascii.Add(b);
            }

            foreach (var suffix in SUFFIX.Split(','))
            {
                ascii.Add(byte.Parse(suffix));
            }

            return ascii.ToArray();
        }
    }
}