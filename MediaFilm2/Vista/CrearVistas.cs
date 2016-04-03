using MediaFilm2.Modelo;
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
        public static Label getLabelResultado(string content)
        {
            Label tmpLabel = new Label();
            tmpLabel.Content = content;
            tmpLabel.Style = (Style)Application.Current.Resources["LabelResultados"];
            return tmpLabel;
        }




    
        internal static StackPanel getVistaSeleccionarSerie(MainWindow  mainWindow,Serie item)
        {
            StackPanel tmpPanel = new StackPanel();
            tmpPanel.Orientation = Orientation.Horizontal;
            tmpPanel.Style = (Style)Application.Current.Resources["StackPanelSeleccionarSerie"];


            Label tmpLabelTitulo = new Label();
            tmpLabelTitulo.Content = item.titulo;
            tmpLabelTitulo.Style = (Style)Application.Current.Resources["LabelListaSeries"];
            tmpPanel.Children.Add(tmpLabelTitulo);


            Button tmpButton = new Button();
            tmpButton.Click += delegate {
                buttonSeleccionarSerie_Click(mainWindow,item); };
            tmpButton.Style = (Style)Application.Current.Resources["Button"];

            tmpButton.Content = "Seleccionar";

            tmpPanel.Children.Add(tmpButton);

            return tmpPanel;
        }

        private static void buttonSeleccionarSerie_Click(MainWindow mainWindow, Serie item)
        {
            mainWindow.serieSeleccionada = item;
           mainWindow.serieSeleccionada.getPatrones(mainWindow.config);
            UpdateIU.Update(mainWindow, Codigos.ADD_PATRON_SERIE_SELEC);
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

        internal static UIElement getVistaFicheroARenombrar(string name)
        {
            Label tmpLabelFichero = new Label();
            tmpLabelFichero.Content = name;
            tmpLabelFichero.Style = (Style)Application.Current.Resources["FicherosARenombrar"];
         
            
            return tmpLabelFichero;
        }

        internal static UIElement getVistaIncrementarTemporadas(MainWindow mainWindow, Serie item)
        {
            StackPanel tmpPanel = new StackPanel();
            tmpPanel.Orientation = Orientation.Horizontal;
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
            tmpPanel.Children.Add(tmpImagenMin);


            //temporada min
            Label tmpLabelTemporadaMin = new Label();
            tmpLabelTemporadaMin.Content = item.temporadaActual;
            tmpLabelTemporadaMin.Style = (Style)Application.Current.Resources["LabelListaSeries"];
            tmpLabelTemporadaMin.Width = 20;
            tmpPanel.Children.Add(tmpLabelTemporadaMin);


            //titulo
            Label tmpLabelTitulo = new Label();
            tmpLabelTitulo.Content = item.titulo;
            tmpLabelTitulo.Style = (Style)Application.Current.Resources["LabelListaSeries"];
            tmpPanel.Children.Add(tmpLabelTitulo);


            //temporada max
            Label tmpLabelTemporadaMax = new Label();
            tmpLabelTemporadaMax.Content = item.numeroTemporadas;
            tmpLabelTemporadaMax.Style = (Style)Application.Current.Resources["LabelListaSeries"];
            tmpLabelTemporadaMax.Width = 20;
            tmpPanel.Children.Add(tmpLabelTemporadaMax);


            //imagen max
            Image tmpImagenMax = new Image();
            tmpImagenMax.Source = src;
            tmpImagenMax.Style = (Style)Application.Current.Resources["Image"];
            tmpPanel.Children.Add(tmpImagenMax);

            return tmpPanel;
        }
    }
}
