
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
                            mainWindow.panelResultadoFicherosBorrados.Children.Add(CrearVistar.getLabelFichero(item));
                        else
                            mainWindow.panelResultadoErroresMoviendo.Children.Add(CrearVistar.getLabelErrorBorrando(item));
                        break;
                    case ".!ut":
                        if (borrarFichero(item, mainWindow))
                            mainWindow.panelResultadoFicherosBorrados.Children.Add(CrearVistar.getLabelFichero(item));
                        else
                            mainWindow.panelResultadoErroresMoviendo.Children.Add(CrearVistar.getLabelErrorBorrando(item));
                        break;
                    case ".url":
                        if (borrarFichero(item, mainWindow))
                            mainWindow.panelResultadoFicherosBorrados.Children.Add(CrearVistar.getLabelFichero(item));
                        else
                            mainWindow.panelResultadoErroresMoviendo.Children.Add(CrearVistar.getLabelErrorBorrando(item));
                        break;
                    case ".jpg":
                        if (borrarFichero(item, mainWindow))
                            mainWindow.panelResultadoFicherosBorrados.Children.Add(CrearVistar.getLabelFichero(item));
                        else
                            mainWindow.panelResultadoErroresMoviendo.Children.Add(CrearVistar.getLabelErrorBorrando(item));
                        break;
                    //mover
                    case ".avi":
                        if (moverFichero(item, mainWindow))
                            mainWindow.panelResultadoVideosMovidos.Children.Add(CrearVistar.getLabelFichero(item));
                        else
                            mainWindow.panelResultadoErroresMoviendo.Children.Add(CrearVistar.getLabelErrorMoviendo(item));
                        break;
                    case ".mkv":
                        if (moverFichero(item, mainWindow))
                            mainWindow.panelResultadoVideosMovidos.Children.Add(CrearVistar.getLabelFichero(item));
                        else
                            mainWindow.panelResultadoErroresMoviendo.Children.Add(CrearVistar.getLabelErrorMoviendo(item));
                        break;
                    case ".mp4":
                        if (moverFichero(item, mainWindow))
                            mainWindow.panelResultadoVideosMovidos.Children.Add(CrearVistar.getLabelFichero(item));
                        else
                            mainWindow.panelResultadoErroresMoviendo.Children.Add(CrearVistar.getLabelErrorMoviendo(item));
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
            int videosRenombrados = 0;
            int erroresRenombrado = 0;
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
                                    if (cap >= 10) fi = obtenerCoincidenciaBusqueda(strPatrones[i + 6]);
                                    else fi = obtenerCoincidenciaBusqueda(strPatrones[i]);
                                    if (fi != null)
                                    {
                                        if (ejecutarMovimiento(fi, dirSerie, itSerie.titulo, temp, cap, fi.Extension)) videosRenombrados++;
                                        else erroresRenombrado++;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            //return new int[] { videosRenombrados, erroresRenombrado, numeroPatrones, seriesActivas, Convert.ToInt32(tiempo.ElapsedMilliseconds) };
        }

        #endregion
    }
}
