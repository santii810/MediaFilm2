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
   public class PatronesXML
    {
        string nombreFichero;
        XmlDocument documento;
        XmlNode raiz;
        LoggerXML xmlDatos;
        LoggerXML xmlError;


        public PatronesXML(Config config)
        {
            this.nombreFichero = config.ficheroPatrones;
            xmlDatos = new LoggerXML(config.datosLog);
            xmlError = new LoggerXML(config.errorLog);
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
        public void añadirPatron(Patron patron)
        {
            documento = new XmlDocument();
            if (!File.Exists(this.nombreFichero))
            {
                XmlDeclaration declaracion = documento.CreateXmlDeclaration("1.0", "ISO-8859-1", null);
                documento.AppendChild(declaracion);
                raiz = documento.CreateElement("raiz");
                documento.AppendChild(raiz);
            }
            else
            {
                documento.Load(this.nombreFichero);
                raiz = documento.DocumentElement;
            }
            if (!existe(patron.textoPatron))
            {
                raiz.AppendChild(crearNodo(patron));
                documento.Save(this.nombreFichero);
                xmlDatos.añadirEntrada(new Log("AñadirPatron", "patron '" + patron.nombreSerie + "-" + patron.textoPatron + "' añadido correctamente a serie "));
            }
            else
            {
                xmlError.añadirEntrada(new Log("Error", "patron '" + patron.nombreSerie + "-" + patron.textoPatron + "' Ya existe "));
            }
        }
        public XmlNode crearNodo(Patron patron)
        {
            XmlElement serie = documento.CreateElement("serie");
            serie.SetAttribute("titulo", patron.nombreSerie);
            serie.InnerText = patron.textoPatron;

            return serie;
        }
        public List<Patron> leerPatrones(string serie)
        {
            List<Patron> patrones = new List<Patron>();
            if (cargarXML())
            {
                foreach (XmlNode item in documento.GetElementsByTagName("serie"))
                {
                    if (item.Attributes["titulo"].Value.Equals(serie))
                    {
                        patrones.Add(new Patron
                        {
                            nombreSerie = serie,
                            textoPatron = item.InnerText.ToString()
                        });
                    }
                }
            }
            return patrones;
        }
        public bool existe(string textoPatron)
        {
            foreach (XmlNode item in documento.GetElementsByTagName("serie"))
            {
                if (item.InnerText.Equals(textoPatron))
                {
                    return true;
                }
            }
            return false;
        }
    }

}

