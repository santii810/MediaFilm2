using MediaFilm2.Modelo;
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
   static class CrearVistas
    {
        public static Label getLabelResultado(string content)
        {
            Label tmpLabel = new Label();
            tmpLabel.Content = content;
            tmpLabel.Style = (Style)Application.Current.Resources["LabelResultados"];
            return tmpLabel;
        }




    
        internal static StackPanel getVistaSeleccionarSerie(Serie item)
        {
            StackPanel tmpPanel = new StackPanel();
            tmpPanel.Orientation = Orientation.Horizontal;

            Label tmpLabelTitulo = new Label();
            tmpLabelTitulo.Content = item.titulo;
            tmpLabelTitulo.Style = (Style)Application.Current.Resources["LabelListaSeries"];
            tmpPanel.Children.Add(tmpLabelTitulo);


            Button tmpButton = new Button();
            tmpButton.Content = "Seleccionar";

            tmpPanel.Children.Add(tmpButton);

            return tmpPanel;
        }
    }
}
