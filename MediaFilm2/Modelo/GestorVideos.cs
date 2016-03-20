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


        public static void recogerTorrent(MainWindow mainWindow)
        {
            int videosMovidos = 0;
            int ficherosBorrados = 0;
            int errorBorrando = 0;
            int errorMoviendo = 0;
            int unsuported = 0;
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
                        if (borrarFichero(item, mainWindow)) ficherosBorrados++;
                        else errorBorrando++;
                        break;
                    case ".!ut":
                        if (borrarFichero(item, mainWindow)) ficherosBorrados++;
                        else errorBorrando++;
                        break;
                    case ".url":
                        if (borrarFichero(item, mainWindow)) ficherosBorrados++;
                        else errorBorrando++;
                        break;
                    //mover
                    case ".avi":
                        if (moverFichero(item, mainWindow))
                        {
                            Label tmpLabel = new Label();
                            tmpLabel.Content =   (item.Name);
                            tmpLabel.Style = (Style)Application.Current.Resources["Label1"];
                            mainWindow.panelResultadoVideosMovidos.Children.Add(tmpLabel);
                        }
                        else
                            errorMoviendo++;
                        break;
                    case ".mkv":
                        if (moverFichero(item, mainWindow))
                        {
                            Label tmpLabel = new Label();
                            tmpLabel.Content = (item.Name);
                            tmpLabel.Style = (Style)Application.Current.Resources["Label1"];
                            mainWindow.panelResultadoVideosMovidos.Children.Add(tmpLabel);
                        }
                        else errorMoviendo++;
                        break;
                    case ".mp4":
                        if (moverFichero(item, mainWindow))
                        {
                            Label tmpLabel = new Label();
                            tmpLabel.Content = (item.Name);
                            tmpLabel.Style = (Style)Application.Current.Resources["Label1"];
                            mainWindow.panelResultadoVideosMovidos.Children.Add(tmpLabel);
                        }
                        else errorMoviendo++;
                        break;
                    default:
                        unsuported++;
                        throw new TipoArchivoNoSoportadoException(item);
                }
            }
            directoriosBorrados = borrarDirectoriosVacios(mainWindow.config.dirTorrent);
            Directory.CreateDirectory(mainWindow.config.dirTorrent);



            mainWindow.labelTiempoRecoger.Content = tiempo.ElapsedMilliseconds.ToString() + " ms";
            //return new int[] { videosMovidos, ficherosBorrados, errorMoviendo, errorBorrando, unsuported, Convert.ToInt32(tiempo.ElapsedMilliseconds), directoriosBorrados };
        }





        /// <summary>
        /// funcion recursiva que devuelve todos los ficheros dentro de la carpeta.
        /// </summary>
        /// <param name="filesInfo">The files information.</param>
        /// <returns></returns>
        static public List<FileInfo> listarFicheros(FileSystemInfo[] filesInfo)
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
    }
}
