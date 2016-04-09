using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MediaFilm2.Modelo
{
    class GestorDescargas
    {
        public static void BuscarDescargasDisponibles(MainWindow mainWindow)
        {
            mainWindow.updateListaSeries();
            foreach (Serie serie in mainWindow.series)
            {
                int[] tmp = GestorVideos.getUltimoFichero(mainWindow, serie);
                if (tmp != null)
                {
                    int temp = tmp[0];
                    int cap = tmp[1];

                    List<string> pruebas = new List<string>();
                    //prueba con capitulo++
                    if (cap < 9)
                    {
                        pruebas.Add("http://www.mejortorrent.com/uploads/torrents/series/" + serie.titDescarga + "_" + temp + "_0" + (cap + 1) + ".torrent");
                        pruebas.Add("http://www.mejortorrent.com/uploads/torrents/series/" + serie.titDescarga + "_" + temp + "_720p_0" + (cap + 1) + ".torrent");
                        pruebas.Add("http://www.mejortorrent.com/uploads/torrents/series/" + serie.titDescarga + "_" + temp + "_1080p_0" + (cap + 1) + ".torrent");

                    }
                    else
                    {
                        pruebas.Add("http://www.mejortorrent.com/uploads/torrents/series/" + serie.titDescarga + "_" + temp + "_" + (cap + 1) + ".torrent");
                        pruebas.Add("http://www.mejortorrent.com/uploads/torrents/series/" + serie.titDescarga + "_" + temp + "_720p_" + (cap + 1) + ".torrent");
                        pruebas.Add("http://www.mejortorrent.com/uploads/torrents/series/" + serie.titDescarga + "_" + temp + "_1080p_" + (cap + 1) + ".torrent");

                    }

                    foreach (string url in pruebas)
                    {
                        if (RemoteFileExists(url))
                        {
                            mainWindow.listaFicherosDescargar.Children.Add(CrearVistas.getFicheroDescargar((serie.titulo + " " + temp + "x" + cap), url));
                        }
                    }

                    //        ejemplos
                    // http://www.mejortorrent.com/uploads/torrents/series/Los_100_3_09.torrent
                    //  http://www.mejortorrent.com/uploads/torrents/series/Angie_Tribeca_1_720p_01.torrent


                }


            }
        }

        private static bool RemoteFileExists(string url)
        {
            try
            {
                //Creating the HttpWebRequest
                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                //Setting the Request method HEAD, you can also use GET too.
                request.Method = "HEAD";
                //Getting the Web Response.
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                //Returns TRUE if the Status code == 200
                response.Close();
                return (response.StatusCode == HttpStatusCode.OK);
            }
            catch
            {
                //Any exception will returns false.
                return false;
            }
        }

    }
}
