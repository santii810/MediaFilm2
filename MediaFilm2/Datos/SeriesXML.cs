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
   public class SeriesXML
    {
        string nombreFichero;
        XmlDocument documento;
        XmlNode raiz;
        LoggerXML xmlDatos;
        LoggerXML xmlError;
        PatronesXML xmlPatrones;


        public SeriesXML(Config config)
        {
            this.nombreFichero = config.ficheroSeries;
            xmlDatos = new LoggerXML(config.datosLog);
            xmlError = new LoggerXML(config.errorLog);
            xmlPatrones = new PatronesXML(config);
        }
        public bool cargarXML()
        {
            if (File.Exists(nombreFichero))
            {
                documento = new XmlDocument();
                documento.Load(nombreFichero);
                raiz = documento.DocumentElement;
                return true;
            }
            else return false;
        }
        public List<Serie> leerSeries()
        {
            List<Serie> series = new List<Serie>();
            if (cargarXML())
            {
                foreach (XmlNode item in documento.GetElementsByTagName("serie"))
                {
                    series.Add(new Serie
                    {
                        titulo = item["titulo"].InnerText.ToString(),
                        temporadaActual = Convert.ToInt32(item["temporadaActual"].InnerText.ToString()),
                        numeroTemporadas = Convert.ToInt32(item["numeroTemporadas"].InnerText.ToString()),
                        capitulosPorTemporada = Convert.ToInt32(item["capitulosPorTemporada"].InnerText.ToString()),
                        estado = item["estado"].InnerText,
                        extension = item["extension"].InnerText
                    });
                }
            }
            return series;
        }
        public Serie buscarSerie(string nombreSerie)
        {
            Serie serie = new Serie();
            if (cargarXML())
            {
                foreach (XmlNode item in documento.GetElementsByTagName("serie"))
                {
                    if (item["titulo"].InnerText.ToString().Equals(nombreSerie))
                    {
                        serie = new Serie
                        {
                            titulo = item["titulo"].InnerText.ToString(),
                            temporadaActual = Convert.ToInt32(item["temporadaActual"].InnerText.ToString()),
                            numeroTemporadas = Convert.ToInt32(item["numeroTemporadas"].InnerText.ToString()),
                            capitulosPorTemporada = Convert.ToInt32(item["capitulosPorTemporada"].InnerText.ToString()),
                            estado = item["estado"].InnerText,
                            extension = item["extension"].InnerText
                        };
                    }
                }
            }
            return serie;
        }

        public void añadirSerie(Serie serie)
        {
            documento = new XmlDocument();

            if (!File.Exists(nombreFichero))
            {
                XmlDeclaration declaracion = documento.CreateXmlDeclaration("1.0", "ISO-8859-1", null);
                documento.AppendChild(declaracion);
                raiz = documento.CreateElement("Series");
                documento.AppendChild(raiz);
            }
            else
            {
                documento.Load(nombreFichero);
                raiz = documento.DocumentElement;
            }
            if (!existe(serie.titulo))
            {
                raiz.AppendChild(crearNodo(serie));
                documento.Save(nombreFichero);

                xmlDatos.añadirEntrada(new Log("AñadirSerie", "Serie '" + serie.titulo + "' añadida correctamente"));
                //Añado 3 patrones por defecto a todas las series nada mas ser añadidas
                xmlPatrones.añadirPatron(new Patron { nombreSerie = serie.titulo, textoPatron = serie.titulo });
                xmlPatrones.añadirPatron(new Patron { nombreSerie = serie.titulo, textoPatron = serie.titulo.Replace(' ', '.') });
                xmlPatrones.añadirPatron(new Patron { nombreSerie = serie.titulo, textoPatron = serie.titulo.Replace(' ', '_') });
            }
            else
            {
                xmlError.añadirEntrada(new Log("Error añadiendo datos", "Serie '" + serie.titulo + "' ya existe"));
            }
        }
        public XmlNode crearNodo(Serie serie)
        {
            XmlElement nodoSerie = documento.CreateElement("serie");
            nodoSerie.SetAttribute("titulo", serie.titulo);

            XmlElement titulo = documento.CreateElement("titulo");
            titulo.InnerText = serie.titulo;
            nodoSerie.AppendChild(titulo);

            XmlElement temporadaActual = documento.CreateElement("temporadaActual");
            temporadaActual.InnerText = serie.temporadaActual.ToString();
            nodoSerie.AppendChild(temporadaActual);

            XmlElement numeroTemporadas = documento.CreateElement("numeroTemporadas");
            numeroTemporadas.InnerText = serie.numeroTemporadas.ToString();
            nodoSerie.AppendChild(numeroTemporadas);

            XmlElement capitulosPorTemporada = documento.CreateElement("capitulosPorTemporada");
            capitulosPorTemporada.InnerText = serie.capitulosPorTemporada.ToString();
            nodoSerie.AppendChild(capitulosPorTemporada);


            XmlElement estado = documento.CreateElement("estado");
            estado.InnerText = serie.estado;
            nodoSerie.AppendChild(estado);

            XmlElement extension = documento.CreateElement("extension");
            extension.InnerText = serie.extension;
            nodoSerie.AppendChild(extension);

            return nodoSerie;
        }
        public bool existe(string nombreSerie)
        {
            foreach (XmlNode item in documento.GetElementsByTagName("serie"))
                if (item.Attributes["titulo"].Value.Equals(nombreSerie))
                    return true;
            return false;
        }
    }

}
