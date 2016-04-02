using System;
using System.IO;
using System.Runtime.Serialization;
using System.Windows;

namespace MediaFilm2.Excepciones
{
    [Serializable]
    internal class TipoArchivoNoSoportadoException : Exception
    {
        private FileInfo item;

        public TipoArchivoNoSoportadoException()
        {
        }

        public TipoArchivoNoSoportadoException(FileInfo item)
        {
            this.item = item;
            MessageBox.Show("TipoArchivoNoSoportadoException en el fichero " + item.Name + "." + item.Extension);
        }

        public TipoArchivoNoSoportadoException(string message) : base(message)
        {
        }

        public TipoArchivoNoSoportadoException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected TipoArchivoNoSoportadoException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}