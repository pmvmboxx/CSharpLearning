using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSVProcessor
{
    public struct CSVWriter
    {
        private FileInfo _source;
        private FileStream _stream;
        private StreamWriter _sourceWriter;
        public bool EndOfData
        {
            get;
            private set;
        }

        public CSVWriter(string filename)
        {
            _source = new FileInfo(filename);

            _stream = _source.OpenWrite();
            _sourceWriter = new StreamWriter(_stream);
        }

        public void Write(string[] source)
        {
            string text = string.Join(", ", source);
            _sourceWriter.WriteLine(text);
        }

        public void Close()
        {
            _sourceWriter.Flush();
            _stream.Close();
        }
    }
}
