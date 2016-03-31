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
                    mainWindow.consolaPanelRenombrarVideos.Visibility = Visibility.Collapsed;
                    mainWindow.consolaPanelRecogerVideos.Visibility = Visibility.Collapsed;
                    break;
                case Codigos.LIMPIAR_ANTIGUOS_RESULTADOS_RECOGER:
                    mainWindow.panelResultadoVideosMovidos.Children.Clear();
                    mainWindow.panelResultadoFicherosBorrados.Children.Clear();
                    mainWindow.panelResultadoErroresMoviendo.Children.Clear();
                    break;
                case Codigos.LIMPIAR_ANTIGUOS_RESULTADOS_RENOMBRAR:
                    mainWindow.panelResultadoErroresRenombrado.Children.Clear();
                    mainWindow.panelResultadoPatronesEjecutados.Children.Clear();
                    mainWindow.panelResultadoVideosRenombrados.Children.Clear();
                    break;
                case Codigos.MOSTRAR_RESULTADOS_RECOGER:
                    mainWindow.panelOrdenarVideos.Visibility = Visibility.Visible;
                    mainWindow.consolaPanelVideos.Visibility = Visibility.Visible;
                    mainWindow.consolaPanelRecogerVideos.Visibility = Visibility.Visible;
                    mainWindow.PanelTiempoRecogido.Visibility = Visibility.Visible;
                    break;
                case Codigos.MOSTRAR_RESULTADOS_RENOMBRAR:
                    mainWindow.panelOrdenarVideos.Visibility = Visibility.Visible;
                    mainWindow.consolaPanelVideos.Visibility = Visibility.Visible;
                    mainWindow.consolaPanelRenombrarVideos.Visibility = Visibility.Visible;
                    mainWindow.panelTiempoRenombrado.Visibility = Visibility.Visible;
                    break;
                case Codigos.PANEL_ADD_DATOS:
                    mainWindow.panelAddDatos.Visibility = Visibility.Visible;
                    break;
                case Codigos.COD_ADD_SERIE:
                    mainWindow.panelAddDatos.Visibility = Visibility.Visible;
                    mainWindow.panelAddSerie.Visibility = Visibility.Visible;

                    break;
                case Codigos.COD_ADD_SERIE_OK:
                    mainWindow.textBoxCapitulosTemporada.Text = "";
                    mainWindow.textBoxNumeroTemporadas.Text = "";
                    mainWindow.textBoxTitulo.Text = "";
                    mainWindow.comboBoxExtensionSerie.SelectedIndex = -1;
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
            mainWindow.consolaPanelRenombrarVideos.Visibility = Visibility.Collapsed;
            mainWindow.PanelTiempoRecogido.Visibility = Visibility.Collapsed;
            mainWindow.panelTiempoRenombrado.Visibility = Visibility.Collapsed;
            mainWindow.panelAddDatos.Visibility = Visibility.Collapsed;
            mainWindow.panelAddSerie.Visibility = Visibility.Collapsed;


        }
    }
}
