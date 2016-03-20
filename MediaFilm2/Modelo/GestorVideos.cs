using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaFilm2.Modelo
{
    static public class GestorVideos
    {
        static recogerTorrent()
        {
            int videosMovidos = 0;
            int ficherosBorrados = 0;
            int errorBorrando = 0;
            int errorMoviendo = 0;
            int unsuported = 0;
            int directoriosBorrados = 0;
            Stopwatch tiempo = Stopwatch.StartNew();

            DirectoryInfo dir = new DirectoryInfo(config.dirTorrent);
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
                        if (borrarFichero(item)) ficherosBorrados++;
                        else errorBorrando++;
                        break;
                    case ".!ut":
                        if (borrarFichero(item)) ficherosBorrados++;
                        else errorBorrando++;
                        break;
                    case ".url":
                        if (borrarFichero(item)) ficherosBorrados++;
                        else errorBorrando++;
                        break;
                    //mover
                    case ".avi":
                        if (moverFichero(item)) videosMovidos++;
                        else errorMoviendo++;
                        break;
                    case ".mkv":
                        if (moverFichero(item)) videosMovidos++;
                        else errorMoviendo++;
                        break;
                    case ".mp4":
                        if (moverFichero(item)) videosMovidos++;
                        else errorMoviendo++;
                        break;
                    default:
                        unsuported++;
                        throw new TipoArchivoNoSoportadoException();
                }
            }
            directoriosBorrados = borrarDirectoriosVacios(config.dirTorrent);
            Directory.CreateDirectory(config.dirTorrent);
            return new int[] { videosMovidos, ficherosBorrados, errorMoviendo, errorBorrando, unsuported, Convert.ToInt32(tiempo.ElapsedMilliseconds), directoriosBorrados };
        }
    }
}
