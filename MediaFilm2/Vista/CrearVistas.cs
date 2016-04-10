using MediaFilm2.Modelo;
using MediaFilm2.Vista;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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
            BitmapImage srcMin = new BitmapImage();
            srcMin.BeginInit();
            srcMin.UriSource = new Uri("Iconos/sub.png", UriKind.Relative);
            srcMin.EndInit();


            //bitmap
            BitmapImage srcMax = new BitmapImage();
            srcMax.BeginInit();
            srcMax.UriSource = new Uri("Iconos/suma.png", UriKind.Relative);
            srcMax.EndInit();

            ////añado imagen min
            Image tmpImagenSubMin = new Image();
            tmpImagenSubMin.Source = srcMin;
            tmpImagenSubMin.Style = (Style)Application.Current.Resources["Image"];
            tmpImagenSubMin.MouseLeftButtonUp += delegate
            {
                if (serie.temporadaActual > 1)
                    serie.temporadaActual--;
                mainWindow.SeriesXML.updateSerie(serie);
                UpdateIU.Update(mainWindow, Codigos.PANEL_INCREMENTAR_TEMPORADAS);
            };
            tmpPanel.Children.Add(tmpImagenSubMin);

            //añado imagen min
            Image tmpImagenAddMin = new Image();
            tmpImagenAddMin.Source = srcMax;
            tmpImagenAddMin.Style = (Style)Application.Current.Resources["Image"];
            tmpImagenAddMin.MouseLeftButtonUp += delegate
            {
                if (serie.temporadaActual < serie.numeroTemporadas)
                    serie.temporadaActual++;
                mainWindow.SeriesXML.updateSerie(serie);
                UpdateIU.Update(mainWindow, Codigos.PANEL_INCREMENTAR_TEMPORADAS);
            };
            tmpPanel.Children.Add(tmpImagenAddMin);

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
            Image tmpImagenSubMax = new Image();
            tmpImagenSubMax.Source = srcMin;
            tmpImagenSubMax.Style = (Style)Application.Current.Resources["Image"];
            tmpImagenSubMax.MouseLeftButtonUp += delegate
            {
                if (serie.numeroTemporadas > serie.temporadaActual)
                    serie.numeroTemporadas--;
                mainWindow.SeriesXML.updateSerie(serie);
                UpdateIU.Update(mainWindow, Codigos.PANEL_INCREMENTAR_TEMPORADAS);
            };
            tmpPanel.Children.Add(tmpImagenSubMax);

            //imagen max
            Image tmpImagenAddMax = new Image();
            tmpImagenAddMax.Source = srcMax;
            tmpImagenAddMax.Style = (Style)Application.Current.Resources["Image"];
            tmpImagenAddMax.MouseLeftButtonUp += delegate
            {
                serie.numeroTemporadas++;
                mainWindow.SeriesXML.updateSerie(serie);
                UpdateIU.Update(mainWindow, Codigos.PANEL_INCREMENTAR_TEMPORADAS);
            };
            tmpPanel.Children.Add(tmpImagenAddMax);

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
                mainWindow.SeriesXML.updateSerie(serie);
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
                mainWindow.SeriesXML.updateSerie(serie);
                UpdateIU.Update(mainWindow, Codigos.PANEL_IO_SERIES);

            };
            tmpPanel.Children.Add(tmpImagenMax);

            return tmpPanel;
        }

        internal static UIElement getVistaDuplicidad(MainWindow mainWindow, FileSystemInfo[] item)
        {

            Border border = new Border();
            border.Style = (Style)Application.Current.Resources["Border"];

            StackPanel tmpPanel = new StackPanel();

            //titulo
            Label tmpLabel1 = new Label();
            tmpLabel1.Content = item[0].Name;
            tmpLabel1.Style = (Style)Application.Current.Resources["LabelListaSeries"];
            tmpLabel1.HorizontalAlignment = HorizontalAlignment.Center;
            tmpPanel.Children.Add(tmpLabel1);

            //titulo
            Label tmpLabel2 = new Label();
            tmpLabel2.Content = item[1].Name;
            tmpLabel2.Style = (Style)Application.Current.Resources["LabelListaSeries"];
            tmpLabel2.HorizontalAlignment = HorizontalAlignment.Center;
            tmpPanel.Children.Add(tmpLabel2);


            Button tmpButton = new Button();
            tmpButton.Width = 60;
            tmpButton.Click += delegate
            {
                try
                {

                    if (MessageBox.Show("¿Seguro que quieres borrar el fichero?", "Borrar", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        File.SetAttributes(item[1].FullName, FileAttributes.Normal);
                        item[1].Delete();
                        mainWindow.ErroresDuplicidad.Remove(item);
                    }
                    UpdateIU.Update(mainWindow, Codigos.VER_DUPLICIDAD);
                }
                catch (Exception e)
                {
                    MessageBox.Show("Error borrando el fichero" + e);
                }
            };
            tmpButton.Style = (Style)Application.Current.Resources["Button"];
            tmpButton.Content = "Borrar";
            tmpPanel.Children.Add(tmpButton);

            border.Child = tmpPanel;
            return border;
        }

        internal static UIElement getFicheroDescargar(string titulo, string url)
        {
            Border border = new Border();
            border.Style = (Style)Application.Current.Resources["Border"];

            StackPanel tmpPanel = new StackPanel();

            //titulo
            Label tmpLabel1 = new Label();
            tmpLabel1.Content = titulo;
            tmpLabel1.Style = (Style)Application.Current.Resources["LabelListaSeries"];
            tmpLabel1.HorizontalAlignment = HorizontalAlignment.Center;
            tmpPanel.Children.Add(tmpLabel1);



            Button tmpButton = new Button();
            tmpButton.Width = 60;
            tmpButton.Click += delegate
            {

                border.Visibility = Visibility.Collapsed;
                WebClient myWebClient = new WebClient();
                myWebClient.DownloadFile(new System.Uri(url), titulo + ".torrent");

            };
            tmpButton.Style = (Style)Application.Current.Resources["Button"];
            tmpButton.Content = "Descargar";
            tmpPanel.Children.Add(tmpButton);

            border.Child = tmpPanel;
            return border;
        }
    }
}
