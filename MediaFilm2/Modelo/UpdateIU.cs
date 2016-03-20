using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MediaFilm2.Iconos
{
    static class UpdateIU
    {
       

        internal static void Update(MainWindow mainWindow)
        {

        }

        internal static void Update(MainWindow mainWindow, int cod)
        {
            collapseAll(mainWindow);

            switch (cod)
            {
                case Codigos.PANEL_ORDENAR_VIDEOS:
                    mainWindow.panelOrdenarVideos.Visibility = Visibility.Visible;
                    break;
                default:
                    throw new UpdateIUException(cod);
            }
        }

        private static void collapseAll(MainWindow mainWindow)
        {
            mainWindow.panelOrdenarVideos.Visibility = Visibility.Collapsed;
        }
    }
}
