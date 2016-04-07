﻿using MediaFilm2.Modelo;
using MediaFilm2.Vista;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MediaFilm2
{
    class CrearVistas
    {
        internal static Label getLabelResultado(string content)
        {
            Label tmpLabel = new Label();
            tmpLabel.Content = content;
            tmpLabel.Style = (Style)Application.Current.Resources["LabelResultados"];
            return tmpLabel;
        }

        internal static StackPanel getVistaSeleccionarSerie(MainWindow mainWindow, Serie item)
        {
            StackPanel tmpPanel = new StackPanel();
            tmpPanel.Style = (Style)Application.Current.Resources["StackPanelSeleccionarSerie"];


            Label tmpLabelTitulo = new Label();
            tmpLabelTitulo.Content = item.titulo;
            tmpLabelTitulo.Style = (Style)Application.Current.Resources["LabelListaSeries"];
            tmpPanel.Children.Add(tmpLabelTitulo);


            Button tmpButton = new Button();
            tmpButton.Click += delegate
            {
                mainWindow.serieSeleccionada = item;
                mainWindow.serieSeleccionada.getPatrones(mainWindow.config);
                UpdateIU.Update(mainWindow, Codigos.ADD_PATRON_SERIE_SELEC);
            };
            tmpButton.Style = (Style)Application.Current.Resources["Button"];
            tmpButton.Content = "Seleccionar";
            tmpPanel.Children.Add(tmpButton);

            return tmpPanel;
        }

        internal static Label getVistaPatronesActuales(Patron item)
        {
            Label tmpLabelTitulo = new Label();
            tmpLabelTitulo.Content = item.textoPatron;
            tmpLabelTitulo.Style = (Style)Application.Current.Resources["LabelListaSeries"];
            return tmpLabelTitulo;

        }

        internal static Label getVistaTitulo(string titulo)
        {
            Label tmpLabelTitulo = new Label();
            tmpLabelTitulo.Content = titulo;
            tmpLabelTitulo.Style = (Style)Application.Current.Resources["LabelTitulos"];
            return tmpLabelTitulo;
        }

        internal static Label getVistaFicheroARenombrar(string name)
        {
            Label tmpLabelFichero = new Label();
            tmpLabelFichero.Content = name;
            tmpLabelFichero.Style = (Style)Application.Current.Resources["FicherosARenombrar"];
            return tmpLabelFichero;
        }

        internal static StackPanel getVistaIncrementarTemporadas(MainWindow mainWindow, Serie serie)
        {
            StackPanel tmpPanel = new StackPanel();
            tmpPanel.Style = (Style)Application.Current.Resources["StackPanelSeleccionarSerie"];

            //bitmap
            BitmapImage src = new BitmapImage();
            src.BeginInit();
            src.UriSource = new Uri("Iconos/suma.png", UriKind.Relative);
            src.EndInit();


            //añado imagen min
            Image tmpImagenMin = new Image();
            tmpImagenMin.Source = src;
            tmpImagenMin.Style = (Style)Application.Current.Resources["Image"];
            tmpImagenMin.MouseLeftButtonUp += delegate
            {
                serie.temporadaActual++;
                if (serie.temporadaActual > serie.numeroTemporadas)
                    serie.numeroTemporadas++;
                mainWindow.SeriesXML.actualizarSerie(serie);
                UpdateIU.Update(mainWindow, Codigos.PANEL_INCREMENTAR_TEMPORADAS);
            };
            tmpPanel.Children.Add(tmpImagenMin);


            //temporada min
            Label tmpLabelTemporadaMin = new Label();
            tmpLabelTemporadaMin.Content = serie.temporadaActual;
            tmpLabelTemporadaMin.Style = (Style)Application.Current.Resources["LabelListaSeries"];
            tmpLabelTemporadaMin.Width = 30;
            tmpPanel.Children.Add(tmpLabelTemporadaMin);


            //titulo
            Label tmpLabelTitulo = new Label();
            tmpLabelTitulo.Content = serie.titulo;
            tmpLabelTitulo.Style = (Style)Application.Current.Resources["LabelListaSeries"];
            tmpPanel.Children.Add(tmpLabelTitulo);


            //temporada max
            Label tmpLabelTemporadaMax = new Label();
            tmpLabelTemporadaMax.Content = serie.numeroTemporadas;
            tmpLabelTemporadaMax.Style = (Style)Application.Current.Resources["LabelListaSeries"];
            tmpLabelTemporadaMax.Width = 30;
            tmpPanel.Children.Add(tmpLabelTemporadaMax);


            //imagen max
            Image tmpImagenMax = new Image();
            tmpImagenMax.Source = src;
            tmpImagenMax.Style = (Style)Application.Current.Resources["Image"];
            tmpImagenMax.MouseLeftButtonUp += delegate
            {
                serie.numeroTemporadas++;
                mainWindow.SeriesXML.actualizarSerie(serie);
                UpdateIU.Update(mainWindow, Codigos.PANEL_INCREMENTAR_TEMPORADAS);
            };
            tmpPanel.Children.Add(tmpImagenMax);

            return tmpPanel;
        }

        internal static StackPanel getVistaSerieActiva(MainWindow mainWindow, Serie serie)
        {
            StackPanel tmpPanel = new StackPanel();
            tmpPanel.Style = (Style)Application.Current.Resources["StackPanelSeleccionarSerie"];

            //bitmap
            BitmapImage src = new BitmapImage();
            src.BeginInit();
            src.UriSource = new Uri("Iconos/powerOff.png", UriKind.Relative);
            src.EndInit();

            //titulo
            Label tmpLabelTitulo = new Label();
            tmpLabelTitulo.Content = serie.titulo;
            tmpLabelTitulo.Style = (Style)Application.Current.Resources["LabelListaSeries"];
            tmpLabelTitulo.HorizontalAlignment = HorizontalAlignment.Center;
            tmpPanel.Children.Add(tmpLabelTitulo);


            //imagen max
            Image tmpImagenMax = new Image();
            tmpImagenMax.Source = src;
            tmpImagenMax.Style = (Style)Application.Current.Resources["Image"];
            tmpImagenMax.MouseLeftButtonUp += delegate
            {
                serie.estado = "D";
                mainWindow.SeriesXML.actualizarSerie(serie);
                UpdateIU.Update(mainWindow, Codigos.PANEL_IO_SERIES);

            };
            tmpPanel.Children.Add(tmpImagenMax);

            return tmpPanel;
        }

        internal static ImageSource getPunto(int cod)
        {
            BitmapImage src = new BitmapImage();
            src.BeginInit();
                        switch (cod)
            {
                case Codigos.PUNTO_VERDE:
                    src.UriSource = new Uri("Iconos/greenPoint.png", UriKind.Relative);
                    break;
                case Codigos.PUNTO_ROJO:
                    src.UriSource = new Uri("Iconos/redPoint.png", UriKind.Relative);
                    break;
                case Codigos.PUNTO_AMARILLO:
                    src.UriSource = new Uri("Iconos/yellowPoint.png", UriKind.Relative);
                    break;
                case Codigos.PUNTO_AZUL:
                    src.UriSource = new Uri("Iconos/bluePoint.png", UriKind.Relative);
                    break;

            }
            src.EndInit();
            
            return src;
        }

        internal static StackPanel getVistaSerieInactiva(MainWindow mainWindow, Serie serie)
        {
            StackPanel tmpPanel = new StackPanel();
            tmpPanel.Style = (Style)Application.Current.Resources["StackPanelSeleccionarSerie"];

            //bitmap
            BitmapImage src = new BitmapImage();
            src.BeginInit();
            src.UriSource = new Uri("Iconos/powerOn.png", UriKind.Relative);
            src.EndInit();

            //titulo
            Label tmpLabelTitulo = new Label();
            tmpLabelTitulo.Content = serie.titulo;
            tmpLabelTitulo.Style = (Style)Application.Current.Resources["LabelListaSeries"];
            tmpLabelTitulo.HorizontalAlignment = HorizontalAlignment.Center;
            tmpPanel.Children.Add(tmpLabelTitulo);


            //imagen max
            Image tmpImagenMax = new Image();
            tmpImagenMax.Source = src;
            tmpImagenMax.Style = (Style)Application.Current.Resources["Image"];
            tmpImagenMax.MouseLeftButtonUp += delegate
            {
                serie.estado = "A";
                mainWindow.SeriesXML.actualizarSerie(serie);
                UpdateIU.Update(mainWindow, Codigos.PANEL_IO_SERIES);

            };
            tmpPanel.Children.Add(tmpImagenMax);

            return tmpPanel;
        }


    }
}
