using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MediaFilm2.Modelo
{
    class GestorDescargas
    {
        public static void BuscarDescargasDisponibles(MainWindow mainWindow)
        {
            mainWindow.updateListaSeries();
            foreach (Serie item in mainWindow.series)
            {
                GestorVideos.getUltimoFichero(mainWindow,item);



                string ejemplo = "http://www.mejortorrent.com/uploads/torrents/series/Los_100_3_09.torrent";
            }
        }

        private bool RemoteFileExists(string url)
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
