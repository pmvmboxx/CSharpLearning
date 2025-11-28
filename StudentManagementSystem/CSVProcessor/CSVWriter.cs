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

            //if (_source.Exists)
            //{
               
            //}
            _stream = _source.OpenWrite();
            _sourceWriter = new StreamWriter(_stream);
        }

        public void Write(string[] source)
        {
            StringBuilder destination = new StringBuilder();

            for (int i = 0; i < source.Length - 1; i++)
            {
                destination.AppendFormat("{0}, ", source[i]);
            }

            destination.AppendFormat("{0}", source[source.Length - 1]);
            _sourceWriter.WriteLine(destination.ToString());
        }

        public void Close()
        {
            _sourceWriter.Flush();
            _stream.Close();
        }
    }
}
