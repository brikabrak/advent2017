namespace Advent.Days
{
    class Day9 : IDay
    {
        private string[] _buffer;
        private bool _negate;
        private bool _garbageEnabled;
        private int _garbageCount;
        private int _score;

        public Day9(string[] inputs)
        {
            this._buffer = inputs;
            this.Reset();
        }

        public void PrintResults()
        {
            foreach(var result in DoPartA())
            {
                System.Console.WriteLine(result);
            }

            this.Reset();
            foreach(var result in DoPartB())
            {
                System.Console.WriteLine(result);
            }
        }

        private string[] DoPartA()
        {
            this.ParseInput(this._buffer[0]);

            return new string[]{this._score.ToString()};
        }

        private string[] DoPartB()
        {
            this.ParseInput(this._buffer[0], true);

            return new string[]{(this._garbageCount).ToString()};
        }

        private void ParseInput(string line, bool collectGarbage = false)
        {
            var inputs = line.ToCharArray();
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
                            groupLevel++;
                            this._score += groupLevel;
                        }
                        else if (collectGarbage)
                        {
                            this._garbageCount++;
                        }
                        break;
                        case '}':
                        if (!this._garbageEnabled)
                        {
                            groupLevel--;
                        } 
                        else if (collectGarbage)
                        {
                            this._garbageCount++;
                        }
                        break;
                        case '<': 
                        if (!this._garbageEnabled)
                        {
                            this._garbageEnabled = true;
                        }
                        else if (collectGarbage)
                        {
                            this._garbageCount++;
                        }
                        break;
                        case '>': this._garbageEnabled = false;
                        break;
                        default:
                        if (collectGarbage && this._garbageEnabled)
                        {
                            this._garbageCount++;
                        }
                        break;
                    }
                }
            }
        }

        private void Reset()
        {
            this._garbageCount = this._score = 0;
            this._garbageEnabled = this._negate = false;
        }
    }
}