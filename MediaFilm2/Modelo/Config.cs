using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaFilm2.Modelo
{
    public class Config
    {
        //Directorios
        public string dirTorrent { get; set; }
        public string dirTrabajo { get; set; }
        public string dirSeries { get; set; }

        //Ficheros log
        public string mediaLog { get; set; }
        public string errorLog { get; set; }
        public string datosLog { get; set; }

        //Ficheros de datos
        public string ficheroSeries { get; set; }
        public string ficheroPatrones { get; set; }
    }
}
}
