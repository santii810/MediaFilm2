﻿using MediaFilm2.Excepciones;
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
                #region Paneles 1
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

                #region Paneles 2
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
                    mainWindow.panelIncrementarTemporadas.Visibility = Visibility.Visible;
                    mainWindow.panelAddDatos.Visibility = Visibility.Visible;
                    rellenaPanelIncrementarTemporadas(mainWindow);
                    break;
                #endregion

                #region Paneles 3
                case Codigos.PANEL_MANTENIMIENTO:
                    mainWindow.panelMantenimiento.Visibility = Visibility.Visible;

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
