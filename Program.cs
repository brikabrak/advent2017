using System;

namespace Advent
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = System.IO.File.ReadAllLines(@"./resources/Day1/input.txt");

            var day = new Days.Day1(input);
            day.PrintResults();
        }
    }
}
