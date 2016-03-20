using System;
using System.Runtime.Serialization;
using System.Windows;

namespace MediaFilm2.Iconos
{
    [Serializable]
    internal class UpdateIUException : Exception
    {
        private int cod;

        public UpdateIUException()
        {

        }

        public UpdateIUException(int cod)
        {
            this.cod = cod;
            MessageBox.Show("UpdateIUException, error code " + cod);
        }

        public UpdateIUException(string message) : base(message)
        {
        }

        public UpdateIUException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected UpdateIUException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}