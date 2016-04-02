using System;
using System.Runtime.Serialization;
using System.Windows;

namespace MediaFilm2.Excepciones
{
    [Serializable]
    internal class TooManySerieCoincidencesException : Exception
    {
        public TooManySerieCoincidencesException()
        {
        }

        public TooManySerieCoincidencesException(string message) : base(message)

        {
            MessageBox.Show("ERROR: Varias coincidencias para al patron " + message);
        }

        public TooManySerieCoincidencesException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected TooManySerieCoincidencesException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}