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

        internal static Label getVistaTituloSeleccionarSerie()
        {
            Label tmpLabelTitulo = new Label();
            tmpLabelTitulo.Content = "Series";
            tmpLabelTitulo.HorizontalAlignment = HorizontalAlignment.Center;
            tmpLabelTitulo.Style = (Style)Application.Current.Resources["LabelTitulos"];
            return tmpLabelTitulo;
        }
        internal static Label getVistaTituloPatronesActuales()
        {
            Label tmpLabelTitulo = new Label();
            tmpLabelTitulo.Content = "Patrones actuales";
            tmpLabelTitulo.HorizontalAlignment = HorizontalAlignment.Center;
            tmpLabelTitulo.Style = (Style)Application.Current.Resources["LabelTitulos"];
            return tmpLabelTitulo;
        }
    }
}
