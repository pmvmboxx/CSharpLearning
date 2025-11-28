namespace CSVProcessor
{
    public struct CSVReader
    {
        private FileInfo _source;
        private FileStream _stream;
        private StreamReader _sourceReader;
        public bool EndOfData 
        {
            get;
            private set;
        }

        public CSVReader(string filename)
        {
            _source = new FileInfo(filename);

            if (_source.Exists)
            {
                _stream = _source.OpenRead();
                _sourceReader = new StreamReader(_stream);
            }
        }


        public string[] GetLine()
        { 
            string data = _sourceReader.ReadLine();
            EndOfData = data == null;

            if (EndOfData)
            {
                return null;
            }

            string[] result = data.Split(',');

            return result;
        }

        public void Close()
        {
            _stream.Close();
        }
    }
}
