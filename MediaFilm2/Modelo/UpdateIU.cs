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
                case Codigos.ESTADO_INICIAL:

                    break;
                case Codigos.PANEL_ORDENAR_VIDEOS:
                    mainWindow.panelOrdenarVideos.Visibility = Visibility.Visible;
                    mainWindow.labelTiempoOrden.Content = "";
                    mainWindow.labelTiempoRecoger.Content = "";
                    mainWindow.consolaPanelOrdenarVideos.Visibility = Visibility.Collapsed;
                    mainWindow.consolaPanelRecogerVideos.Visibility = Visibility.Collapsed;
                    break;
                case Codigos.LIMPIAR_ANTIGUOS_RESULTADOS_RECOGER:
                    mainWindow.panelResultadoVideosMovidos.Children.Clear();
                    mainWindow.panelResultadoFicherosBorrados.Children.Clear();
                    mainWindow.panelResultadoErroresMoviendo.Children.Clear();
                    break;
                case Codigos.MOSTRAR_RESULTADOS_RECOGER:
                    mainWindow.panelOrdenarVideos.Visibility = Visibility.Visible;
                    mainWindow.consolaPanelVideos.Visibility = Visibility.Visible;
                    mainWindow.consolaPanelRecogerVideos.Visibility = Visibility.Visible;
                    break;
                case Codigos.MOSTRAR_RESULTADOS_ORDENAR:
                    mainWindow.panelOrdenarVideos.Visibility = Visibility.Visible;
                    mainWindow.consolaPanelVideos.Visibility = Visibility.Visible;
                    mainWindow.consolaPanelOrdenarVideos.Visibility = Visibility.Visible;

                    break;
                default:
                    throw new UpdateIUException(cod);
            }
        }

        private static void collapseAll(MainWindow mainWindow)
        {
            //siempre visibles
            mainWindow.panelMenu.Visibility = Visibility.Visible;

            //siempre ocultos
            mainWindow.panelOrdenarVideos.Visibility = Visibility.Collapsed;
            mainWindow.consolaPanelVideos.Visibility = Visibility.Collapsed;
            mainWindow.consolaPanelRecogerVideos.Visibility = Visibility.Collapsed;
            mainWindow.consolaPanelOrdenarVideos.Visibility = Visibility.Collapsed;



        }

        internal static void Update(MainWindow mainWindow, object mOSTRAR_RESULTADOS_RECOGER)
        {
            throw new NotImplementedException();
        }
    }
}
