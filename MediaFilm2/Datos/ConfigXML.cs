using MediaFilm2.Modelo;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace MediaFilm2.Datos
{
    class ConfigXML
    {
        string nombreFichero = @"xml\config.xml";
        XmlNode raiz;

        public XmlDocument Documento { get; set; }

        private bool cargarXML()
        {
            if (File.Exists(nombreFichero))
            {
                Documento = new XmlDocument();
                Documento.Load(nombreFichero);
                raiz = Documento.DocumentElement;
                return true;
            }
            else return false;
        }

        public Config leerConfig()
        {
            Config config = new Config();
            if (cargarXML())
            {
                config.dirTorrent = Documento.GetElementsByTagName("dirTorrent")[0].InnerText;
                config.errorLog = Documento.GetElementsByTagName("errorLog")[0].InnerText;
                config.mediaLog = Documento.GetElementsByTagName("mediaLog")[0].InnerText;
                config.datosLog = Documento.GetElementsByTagName("datosLog")[0].InnerText;
                config.dirTrabajo = Documento.GetElementsByTagName("dirTrabajo")[0].InnerText;
                config.ficheroSeries = Documento.GetElementsByTagName("fichSeries")[0].InnerText;
                config.ficheroPatrones = Documento.GetElementsByTagName("fichPatrones")[0].InnerText;
                config.dirSeries = Documento.GetElementsByTagName("dirSeries")[0].InnerText;
            }
            return config;
        }
    }
}
