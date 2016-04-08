using MediaFilm2.Excepciones;
using MediaFilm2.Modelo;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MediaFilm2.Vista
{
    static class UpdateIU
    {
        internal static void Update(MainWindow mainWindow, int cod)
        {
            collapseAll(mainWindow);

            switch (cod)
            {
                case Codigos.ESTADO_INICIAL:
                    break;
                #region Paneles 1 (Tareas)
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
                #endregion

                #region Paneles 2 (Datos)
                case Codigos.PANEL_ADD_DATOS:
                    mainWindow.panelAddDatos.Visibility = Visibility.Visible;
                    break;
                case Codigos.ADD_SERIE:
                    mainWindow.panelAddDatos.Visibility = Visibility.Visible;
                    mainWindow.panelAddSerie.Visibility = Visibility.Visible;
                    break;
                case Codigos.ADD_SERIE_OK:
                    mainWindow.panelAddDatos.Visibility = Visibility.Visible;
                    mainWindow.panelAddSerie.Visibility = Visibility.Visible;
                    mainWindow.textBoxCapitulosTemporada.Text = "";
                    mainWindow.textBoxNumeroTemporadas.Text = "";
                    mainWindow.textBoxTitulo.Text = "";
                    mainWindow.comboBoxExtensionSerie.SelectedIndex = -1;
                    break;
                case Codigos.ADD_PATRON:
                    mainWindow.panelAddDatos.Visibility = Visibility.Visible;
                    mainWindow.panelAddPatron.Visibility = Visibility.Visible;
                    mainWindow.panelSeleccionarSeriePatron.Visibility = Visibility.Visible;
                    mainWindow.panelFicherosARenombrar.Visibility = Visibility.Visible;
                    rellenaPanelSeleccionarSeries(mainWindow);
                    rellenaPanelFicherosARenombrar(mainWindow);
                    break;
                case Codigos.ADD_PATRON_SERIE_SELEC:
                    mainWindow.panelAddDatos.Visibility = Visibility.Visible;
                    mainWindow.panelAddPatron.Visibility = Visibility.Visible;
                    mainWindow.panelSeleccionarSeriePatron.Visibility = Visibility.Visible;
                    mainWindow.panelNuevoPatron.Visibility = Visibility.Visible;
                    mainWindow.panelFicherosARenombrar.Visibility = Visibility.Visible;
                    rellenaPanelFicherosARenombrar(mainWindow);
                    rellenaPanelPatronesActuales(mainWindow);
                    break;
                case Codigos.ADD_PATRON_OK:
                    mainWindow.textBoxNuevoPatron.Text = "";
                    Update(mainWindow, Codigos.ADD_PATRON_SERIE_SELEC);
                    break;
                case Codigos.PANEL_IO_SERIES:
                    mainWindow.panelAddDatos.Visibility = Visibility.Visible;
                    mainWindow.panelIOSerie.Visibility = Visibility.Visible;
                    rellenaPanelSeriesActivas(mainWindow);
                    rellenaPanelSeriesInactivas(mainWindow);
                    break;
                case Codigos.PANEL_INCREMENTAR_TEMPORADAS:
                    mainWindow.panelAddDatos.Visibility = Visibility.Visible;
                    //   mainWindow.panelIncrementarTemporadas.Visibility = Visibility.Visible;
                    rellenaPanelIncrementarTemporadas(mainWindow);
                    break;
                #endregion

                #region Paneles 3 (Mantenimiento)
                case Codigos.PANEL_MANTENIMIENTO:
                    mainWindow.panelMantenimiento.Visibility = Visibility.Visible;
                    mainWindow.circuloContinuidad.Source = CrearVistas.getPunto(Codigos.PUNTO_AZUL);

                    break;
                case Codigos.RESULTADO_MANTENIMIENTO:
                    mainWindow.panelMantenimiento.Visibility = Visibility.Visible;
                    mainWindow.panelResultadoContinuidad.Visibility = Visibility.Visible;
                    mainWindow.panelResultadoHomogenia.Visibility = Visibility.Visible;
                    mainWindow.panelResultadoDuplicidad.Visibility = Visibility.Visible;


                    if (mainWindow.ErroresContinuidad.Count == 0)
                        mainWindow.circuloContinuidad.Source = CrearVistas.getPunto(Codigos.PUNTO_VERDE);
                    if (mainWindow.ErroresContinuidad.Count > 5)
                        mainWindow.circuloContinuidad.Source = CrearVistas.getPunto(Codigos.PUNTO_ROJO);
                    else
                        mainWindow.circuloContinuidad.Source = CrearVistas.getPunto(Codigos.PUNTO_AMARILLO);
                    mainWindow.labelResultadoContinuidad.Content = mainWindow.ErroresContinuidad.Count + " errores detectados";


                    if (mainWindow.ErroresHomogenia.Count == 0)
                        mainWindow.circuloHomogenia.Source = CrearVistas.getPunto(Codigos.PUNTO_VERDE);
                    if (mainWindow.ErroresHomogenia.Count > 10)
                        mainWindow.circuloHomogenia.Source = CrearVistas.getPunto(Codigos.PUNTO_ROJO);
                    else
                        mainWindow.circuloContinuidad.Source = CrearVistas.getPunto(Codigos.PUNTO_AMARILLO);
                    mainWindow.labelResultadoHomogenia.Content = mainWindow.ErroresHomogenia.Count + " errores detectados";

                    if (mainWindow.ErroresDuplicidad.Count == 0)
                        mainWindow.circuloDuplicidad.Source = CrearVistas.getPunto(Codigos.PUNTO_VERDE);
                    if (mainWindow.ErroresDuplicidad.Count > 2)
                        mainWindow.circuloDuplicidad.Source = CrearVistas.getPunto(Codigos.PUNTO_ROJO);
                    else
                        mainWindow.circuloDuplicidad.Source = CrearVistas.getPunto(Codigos.PUNTO_AMARILLO);
                    mainWindow.labelResultadoDuplicidad.Content = mainWindow.ErroresDuplicidad.Count + " errores detectados";





                    break;

                case Codigos.VER_CONTINUIDAD:
                    Update(mainWindow, Codigos.RESULTADO_MANTENIMIENTO);
                    mainWindow.borderResultadoMantenimiento.Visibility = Visibility.Visible;
                    mainWindow.labelTituloResultadosMantenimiento.Content = "Ficheros que faltan:";
                    rellenaPanelResultadoContinuidad(mainWindow);
                    break;
                case Codigos.VER_HOMOGENIA:
                    Update(mainWindow, Codigos.RESULTADO_MANTENIMIENTO);
                    mainWindow.borderResultadoMantenimiento.Visibility = Visibility.Visible;
                    mainWindow.labelTituloResultadosMantenimiento.Content = "Ficheros con ext incorrecta:";
                    rellenaPanelResultadoHomogenia(mainWindow);
                    break;
                case Codigos.VER_DUPLICIDAD:
                    Update(mainWindow, Codigos.RESULTADO_MANTENIMIENTO);
                    mainWindow.borderResultadoMantenimiento.Visibility = Visibility.Visible;
                    mainWindow.labelTituloResultadosMantenimiento.Content = "Ficheros duplicados:";
                    rellenaPanelResultadoDuplicidad(mainWindow);
                    break;
                #endregion

                #region Paneles 4 (Descarga)
                case Codigos.PANEL_DESCARGA:
                    mainWindow.panelDescarga.Visibility = Visibility.Visible;



                    break;
                #endregion
                default:
                    throw new UpdateIUException(cod);
            }
        }

        private static void collapseAll(MainWindow mainWindow)
        {
            //siempre ocultos
            mainWindow.panelOrdenarVideos.Visibility = Visibility.Collapsed;
            mainWindow.consolaPanelVideos.Visibility = Visibility.Collapsed;
            mainWindow.consolaPanelRecogerVideos.Visibility = Visibility.Collapsed;
            mainWindow.consolaPanelRenombrarVideos.Visibility = Visibility.Collapsed;
            mainWindow.PanelTiempoRecogido.Visibility = Visibility.Collapsed;
            mainWindow.panelTiempoRenombrado.Visibility = Visibility.Collapsed;
            mainWindow.panelAddDatos.Visibility = Visibility.Collapsed;
            mainWindow.panelAddSerie.Visibility = Visibility.Collapsed;
            mainWindow.panelAddPatron.Visibility = Visibility.Collapsed;
            mainWindow.panelAddPatron.Visibility = Visibility.Collapsed;
            mainWindow.panelSeleccionarSeriePatron.Visibility = Visibility.Collapsed;
            mainWindow.panelNuevoPatron.Visibility = Visibility.Collapsed;
            mainWindow.panelFicherosARenombrar.Visibility = Visibility.Collapsed;
            mainWindow.panelIOSerie.Visibility = Visibility.Collapsed;
            mainWindow.panelIncrementarTemporadas.Visibility = Visibility.Collapsed;
            mainWindow.panelMantenimiento.Visibility = Visibility.Collapsed;
            mainWindow.panelResultadoContinuidad.Visibility = Visibility.Collapsed;
            mainWindow.panelResultadoHomogenia.Visibility = Visibility.Collapsed;
            mainWindow.panelResultadoDuplicidad.Visibility = Visibility.Collapsed;
            mainWindow.borderResultadoMantenimiento.Visibility = Visibility.Collapsed;
            mainWindow.panelDescarga.Visibility = Visibility.Collapsed;



        }





        private static void rellenaPanelResultadoDuplicidad(MainWindow mainWindow)
        {
            mainWindow.panelMostrarResultadosMantenimiento.Children.Clear();
            //  mainWindow.ErroresDuplicidad.Sort();
            foreach (FileSystemInfo[] item in mainWindow.ErroresDuplicidad)
            {
                mainWindow.panelMostrarResultadosMantenimiento.Children.Add(CrearVistas.getVistaDuplicidad(mainWindow, item));
            }
        }

        private static void rellenaPanelResultadoHomogenia(MainWindow mainWindow)
        {
            mainWindow.panelMostrarResultadosMantenimiento.Children.Clear();
            mainWindow.ErroresHomogenia.Sort();
            foreach (string item in mainWindow.ErroresHomogenia)
            {
                mainWindow.panelMostrarResultadosMantenimiento.Children.Add(CrearVistas.getLabelResultado(item));
            }
        }

        private static void rellenaPanelResultadoContinuidad(MainWindow mainWindow)
        {
            mainWindow.panelMostrarResultadosMantenimiento.Children.Clear();
            mainWindow.ErroresContinuidad.Sort();
            foreach (string item in mainWindow.ErroresContinuidad)
            {
                mainWindow.panelMostrarResultadosMantenimiento.Children.Add(CrearVistas.getLabelResultado(item));

            }
        }

        private static void rellenaPanelSeriesActivas(MainWindow mainWindow)
        {
            mainWindow.panelListaSeriesActivas.Children.Clear();
            mainWindow.actualizarListaSeries();
            foreach (Serie item in mainWindow.series)
            {
                if (item.estado == "A")
                    mainWindow.panelListaSeriesActivas.Children.Add(CrearVistas.getVistaSerieActiva(mainWindow, item));
            }
        }

        private static void rellenaPanelSeriesInactivas(MainWindow mainWindow)
        {
            mainWindow.panelListaSeriesInactivas.Children.Clear();
            mainWindow.actualizarListaSeries();
            foreach (Serie item in mainWindow.series)
            {
                if (item.estado == "D")
                    mainWindow.panelListaSeriesInactivas.Children.Add(CrearVistas.getVistaSerieInactiva(mainWindow, item));
            }

        }

        private static void rellenaPanelFicherosARenombrar(MainWindow mainWindow)
        {
            mainWindow.panelFicherosARenombrar.Children.Clear();
            mainWindow.panelFicherosARenombrar.Children.Add(CrearVistas.getVistaTitulo("Ficheros a renombrar"));
            foreach (FileInfo item in GestorVideos.getFicherosARenombrar(mainWindow))
                if (item.Extension.Equals(".mkv") || item.Extension.Equals(".avi") || item.Extension.Equals(".mp4"))
                    mainWindow.panelFicherosARenombrar.Children.Add(CrearVistas.getVistaFicheroARenombrar(item.Name));
        }

        private static void rellenaPanelSeleccionarSeries(MainWindow mainWindow)
        {
            mainWindow.panelSeleccionarSeriePatron.Children.Clear();
            mainWindow.panelSeleccionarSeriePatron.Children.Add(CrearVistas.getVistaTitulo("Series"));
            mainWindow.actualizarListaSeries();
            foreach (Serie item in mainWindow.series)
            {
                if (item.estado == "A")
                    mainWindow.panelSeleccionarSeriePatron.Children.Add(CrearVistas.getVistaSeleccionarSerie(mainWindow, item));
            }
        }

        private static void rellenaPanelIncrementarTemporadas(MainWindow mainWindow)
        {
            mainWindow.panelListaIncrementarTemporadas.Children.Clear();
            mainWindow.actualizarListaSeries();
            foreach (Serie item in mainWindow.series)
            {
                if (item.estado == "A")
                    mainWindow.panelListaIncrementarTemporadas.Children.Add(CrearVistas.getVistaIncrementarTemporadas(mainWindow, item));
            }
        }

        private static void rellenaPanelPatronesActuales(MainWindow mainWindow)
        {
            mainWindow.panelPatronesActuales.Children.Clear();
            mainWindow.panelPatronesActuales.Children.Add(CrearVistas.getVistaTitulo("Patrones"));
            foreach (Patron item in mainWindow.serieSeleccionada.patrones)
            {
                mainWindow.panelPatronesActuales.Children.Add(CrearVistas.getVistaPatronesActuales(item));
            }
        }

    }
}
