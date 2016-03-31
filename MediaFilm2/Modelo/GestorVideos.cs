
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MediaFilm2.Modelo
{
    static public class GestorVideos
    {

        #region Recorrer torrent
        public static void recogerTorrent(MainWindow mainWindow)
        {
            int directoriosBorrados = 0;

            Stopwatch tiempo = Stopwatch.StartNew();

            DirectoryInfo dir = new DirectoryInfo(mainWindow.config.dirTorrent);
            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException("Directorio de descargas no encontrado");
            }

            FileSystemInfo[] filesInfo = dir.GetFileSystemInfos();
            List<FileInfo> ficherosTorrent = new List<FileInfo>();
            ficherosTorrent.AddRange(listarFicheros(filesInfo));

            foreach (FileInfo item in ficherosTorrent)
            {
                switch (item.Extension)
                {
                    //borrar
                    case ".txt":
                        if (borrarFichero(item, mainWindow))
                            mainWindow.panelResultadoFicherosBorrados.Children.Add(CrearVistas.getLabelResultado(item.Name));
                        else
                            mainWindow.panelResultadoErroresMoviendo.Children.Add(CrearVistas.getLabelResultado("Error borrando: " +item.Name));
                        break;
                    case ".!ut":
                        if (borrarFichero(item, mainWindow))
                            mainWindow.panelResultadoFicherosBorrados.Children.Add(CrearVistas.getLabelResultado(item.Name));
                        else
                            mainWindow.panelResultadoErroresMoviendo.Children.Add(CrearVistas.getLabelResultado("Error borrando: " + item.Name));
                        break;
                    case ".url":
                        if (borrarFichero(item, mainWindow))
                            mainWindow.panelResultadoFicherosBorrados.Children.Add(CrearVistas.getLabelResultado(item.Name));
                        else
                            mainWindow.panelResultadoErroresMoviendo.Children.Add(CrearVistas.getLabelResultado("Error borrando: " + item.Name));
                        break;
                    case ".jpg":
                        if (borrarFichero(item, mainWindow))
                            mainWindow.panelResultadoFicherosBorrados.Children.Add(CrearVistas.getLabelResultado(item.Name));
                        else
                            mainWindow.panelResultadoErroresMoviendo.Children.Add(CrearVistas.getLabelResultado("Error borrando: " + item.Name));
                        break;
                    //mover
                    case ".avi":
                        if (moverFichero(item, mainWindow))
                            mainWindow.panelResultadoVideosMovidos.Children.Add(CrearVistas.getLabelResultado(item.Name));
                        else
                            mainWindow.panelResultadoErroresMoviendo.Children.Add(CrearVistas.getLabelResultado("Error renombrando: " + item.Name));
                        break;
                    case ".mkv":
                        if (moverFichero(item, mainWindow))
                            mainWindow.panelResultadoVideosMovidos.Children.Add(CrearVistas.getLabelResultado(item.Name));
                        else
                            mainWindow.panelResultadoErroresMoviendo.Children.Add(CrearVistas.getLabelResultado("Error renombrando: " + item.Name));
                        break;
                    case ".mp4":
                        if (moverFichero(item, mainWindow))
                            mainWindow.panelResultadoVideosMovidos.Children.Add(CrearVistas.getLabelResultado(item.Name));
                        else
                            mainWindow.panelResultadoErroresMoviendo.Children.Add(CrearVistas.getLabelResultado("Error renombrando: " + item.Name));
                        break;
                    default:
                        throw new TipoArchivoNoSoportadoException(item);
                }
            }
            directoriosBorrados = borrarDirectoriosVacios(mainWindow.config.dirTorrent);
            Directory.CreateDirectory(mainWindow.config.dirTorrent);


            //mostrar tiempo
            mainWindow.labelTiempoRecoger.Content = tiempo.ElapsedMilliseconds.ToString() + " ms";
        }

        /// <summary>
        /// funcion recursiva que devuelve todos los ficheros dentro de la carpeta.
        /// </summary>
        /// <param name="filesInfo">The files information.</param>
        /// <returns></returns>
        static private List<FileInfo> listarFicheros(FileSystemInfo[] filesInfo)
        {
            List<FileInfo> retorno = new List<FileInfo>();

            foreach (FileSystemInfo item in filesInfo)
            {
                if (item is DirectoryInfo)
                {
                    DirectoryInfo dInfo = (DirectoryInfo)item;
                    retorno.AddRange(listarFicheros(dInfo.GetFileSystemInfos()));
                }
                else if (item is FileInfo)
                {
                    retorno.Add((FileInfo)item);
                }
            }
            return retorno;
        }

        /// <summary>
        /// Borra el fichero enviado como parametro.
        /// </summary>
        /// <param name="fichero">Fichero a borrar.</param>
        /// <returns></returns>
        static private bool borrarFichero(FileInfo fichero, MainWindow mainWindow)
        {
            string nombreFichero = fichero.Name;
            try
            {
                fichero.Delete();
                mainWindow.LogMediaXML.añadirEntrada(new Log("Borrado", "Fichero '" + nombreFichero + "' borrado correctamente", fichero));
                return true;
            }
            catch (Exception e)
            {
                mainWindow.LogMediaXML.añadirEntrada(new Log("Error borrando", "Error borrando '" + nombreFichero + "' \t" + e.ToString()));
                return false;
            }
        }

        /// <summary>
        /// Mueve el fichero enviado como parametro al directorio de trabajo seleccionado en la configuracion.
        /// </summary>
        /// <param name="fichero">The fichero.</param>
        /// <returns></returns>
        static private bool moverFichero(FileInfo fichero, MainWindow mainWindow)
        {
            string nombreFichero = fichero.Name;
            string pathDestino = mainWindow.config.dirTrabajo + @"\" + fichero.Name;
            try
            {
                fichero.MoveTo(pathDestino);
                mainWindow.LogMediaXML.añadirEntrada(new Log("Movido", "Fichero '" + nombreFichero + "' movido a '" + fichero.FullName + "'"));
                return true;
            }
            catch (Exception e)
            {
                mainWindow.LogErrorXML.añadirEntrada(new Log("Error moviendo", "Error moviendo '" + nombreFichero + "' \t" + e.ToString()));
                return false;
            }
        }

        /// <summary>
        /// Borra todos los directorios vacios de la ruta proporcionada, asi como el propio directorio.
        /// </summary>
        /// <param name="dir">El directorio.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">Starting directory is a null reference or an empty</exception>
        static private int borrarDirectoriosVacios(string dir)
        {
            int retorno = 0;
            if (String.IsNullOrEmpty(dir))
                throw new ArgumentException(
                    "Starting directory is a null reference or an empty string", "dir");
            try
            {
                foreach (var d in Directory.EnumerateDirectories(dir))
                {
                    retorno += borrarDirectoriosVacios(d);
                }

                var entries = Directory.EnumerateFileSystemEntries(dir);

                if (!entries.Any())
                {
                    try
                    {
                        Directory.Delete(dir);
                        retorno++;
                    }
                    catch (UnauthorizedAccessException) { }
                    catch (DirectoryNotFoundException) { }
                }
            }
            catch (UnauthorizedAccessException) { }
            return retorno;
        }

        #endregion


        #region Renombrar videos
        public static void renombrarVideos(MainWindow mainWindow)
        {
            Stopwatch tiempo = Stopwatch.StartNew();
            int numeroPatrones = 0;
            int seriesActivas = 0;
            mainWindow.series = mainWindow.SeriesXML.leerSeries();
            mainWindow.series.Sort();
            foreach (Serie itSerie in mainWindow.series)
            {
                if (itSerie.estado.Equals("A"))
                {
                    itSerie.getPatrones(mainWindow.config);
                    //calculo de patrones: Numero de patrones de la serie en el xml * temporadas activas de la serie * numero de capitulos de cada temporada * 12 (strings que se comprueban en cada patron)
                    numeroPatrones += (itSerie.patrones.Count * ((itSerie.numeroTemporadas - itSerie.temporadaActual) + 1) * itSerie.capitulosPorTemporada) * 12;
                    seriesActivas++;
                    foreach (Patron itPatron in itSerie.patrones)
                    {
                        for (int temp = itSerie.temporadaActual; temp <= itSerie.numeroTemporadas; temp++)
                        {
                            for (int cap = 1; cap <= itSerie.capitulosPorTemporada; cap++)
                            {
                                FileInfo fi;
                                string dirSerie = @mainWindow.config.dirSeries + @"\" + itSerie.titulo + @"\Temporada" + temp + @"\";
                                string[] strPatrones = new string[]
                                {
                                    //patrones para capitulos<10  y extension == mkv
                                    itPatron.textoPatron + "*" + temp.ToString() + "0" + cap.ToString() + "*.mkv" ,
                                    itPatron.textoPatron + "*" + temp.ToString() + "x0" + cap.ToString() + "*.mkv" ,
                                    temp.ToString()+"x0"+cap.ToString()+"*"+itPatron.textoPatron+"*.mkv",

                                    //patrones para capitulos<10  y extension == avi
                                    itPatron.textoPatron + "*" + temp.ToString() + "0" + cap.ToString() + "*.avi" ,
                                    itPatron.textoPatron + "*" + temp.ToString() + "x0" + cap.ToString() + "*.avi" ,
                                    temp.ToString()+"x0"+cap.ToString()+"*"+itPatron.textoPatron+"*.avi",

                                    //patrones para capitulos>10  y extension == mkv
                                    itPatron.textoPatron + "*" + temp.ToString() + cap.ToString() + "*.mkv",
                                    itPatron.textoPatron + "*" + temp.ToString() + "x" + cap.ToString() + "*.mkv",
                                    temp.ToString()+"x"+cap.ToString()+"*"+itPatron.textoPatron+"*.mkv",

                                    //patrones para capitulos>10  y extension == avi
                                    itPatron.textoPatron + "*" + temp.ToString() + cap.ToString() + "*.avi",
                                    itPatron.textoPatron + "*" + temp.ToString() + "x" + cap.ToString() + "*.avi",
                                    temp.ToString()+"x"+cap.ToString()+"*"+itPatron.textoPatron+"*.avi",
                              };

                                for (int i = 0; i < 6; i++)
                                {
                                    if (cap >= 10) fi = obtenerCoincidenciaBusqueda(mainWindow, strPatrones[i + 6]);
                                    else fi = obtenerCoincidenciaBusqueda(mainWindow, strPatrones[i]);
                                    if (fi != null)
                                        ejecutarMovimiento(mainWindow, fi, dirSerie, itSerie.titulo, temp, cap, fi.Extension);
                                }
                            }
                        }
                    }
                }
            }

            //mostrar tiempo
            mainWindow.labelTiempoOrden.Content = tiempo.ElapsedMilliseconds.ToString() + " ms";

            //patrones 
            mainWindow.panelResultadoPatronesEjecutados.Children.Add(CrearVistas.getLabelResultado(numeroPatrones + " patrones ejecutados"));

            //series
            mainWindow.panelResultadoPatronesEjecutados.Children.Add(CrearVistas.getLabelResultado(seriesActivas + " series activas"));

        }
        

        /// <summary>
        /// Mueve los ficheros.
        /// </summary>
        /// <param name="fi">Fichero a mover</param>
        /// <param name="dirSerie">Directorio de destino</param>
        /// <param name="titulo">Titulo del fichero.</param>
        /// <param name="temp">The temporada.</param>
        /// <param name="cap">The capitulo.</param>
        /// <param name="ext">The extension.</param>
        /// <returns> Retorna true si el movimiento se realiza correctamente</returns>
        private static void ejecutarMovimiento(MainWindow mainWindow, FileInfo fi, string dirSerie, string titulo, int temp, int cap, string ext)
        {
            string nombreOriginal = fi.Name;
            //Crea todos los directorios y subdirectorios en la ruta de acceso especificada, a menos que ya existan.
            Directory.CreateDirectory(dirSerie);
            try
            {
                if (cap < 10)
                {
                    fi.MoveTo(dirSerie + @"\" + titulo + " " + temp + "0" + cap + ext);
                }
                else
                {
                    fi.MoveTo(dirSerie + @"\" + titulo + " " + temp + cap + ext);
                }
                mainWindow.LogMediaXML.añadirEntrada(new Log("Renombrado", "Fichero '" + nombreOriginal + "' renombrado a '" + fi.FullName + "'", fi));
                mainWindow.panelResultadoVideosRenombrados.Children.Add(CrearVistas.getLabelResultado(nombreOriginal + "    =>    " + fi.Name));
            }
            catch (Exception e)
            {
                mainWindow.panelResultadoErroresRenombrado.Children.Add(CrearVistas.getLabelResultado("Error renombrando: " + nombreOriginal));
                mainWindow.LogErrorXML.añadirEntrada(new Log("Error renombrando", "Fichero '" + nombreOriginal + "' no se ha podido renombrar a  '" + fi.FullName + "' /n" + e.ToString(), fi));
            }
        }

        /// <summary>
        /// Busca en el directorio de trabajo si existe algun fichero que coincida con el patron enviado.
        /// </summary>
        /// <param name="pat">Patron a buscar</param>
        /// <returns>FileInfo del fichero si hay coincidencias</returns>
        /// <exception cref="TooManySerieCoincidencesException"></exception>
        static private FileInfo obtenerCoincidenciaBusqueda(MainWindow mainWindow, string pat)
        {
            DirectoryInfo iomegaInfo = new DirectoryInfo(mainWindow.config.dirTrabajo);
            FileSystemInfo[] fsi;
            fsi = iomegaInfo.GetFileSystemInfos(pat);
            if (fsi.Length == 1 && fsi[0] is FileInfo)
            {
                return (FileInfo)fsi[0];
            }
            if (fsi.Length > 1)
            {
                throw new TooManySerieCoincidencesException(pat);
            }
            return null;
        }

        #endregion
    }
}
