using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaFilm2.Modelo
{
   public class Log
    {
        public DateTime fecha { set; get; }
        public string tipo { set; get; }
        public string mensaje { set; get; }
        public FileInfo fichero { get; set; }

        public Log(string tipo, string mensaje)
        {
            fecha = DateTime.Now;
            this.tipo = tipo;
            this.mensaje = mensaje;
        }

        public Log(string tipo, string mensaje, FileInfo fichero) : this(tipo, mensaje)
        {
            this.fichero = fichero;
        }
    }
}
