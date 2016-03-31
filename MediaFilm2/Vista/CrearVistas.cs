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
        public static Label getLabelFichero(FileInfo item)
        {
            Label tmpLabel = new Label();
            tmpLabel.Content = (item.Name);
            tmpLabel.Style = (Style)Application.Current.Resources["Label1"];
            return tmpLabel;
        }

        internal static UIElement getLabelErrorBorrando(FileInfo item)
        {
            Label tmpLabel = new Label();
            tmpLabel.Content = "Error borrando: " + (item.Name);
            tmpLabel.Style = (Style)Application.Current.Resources["Label1"];
            return tmpLabel;
        }

        internal static UIElement getLabelErrorMoviendo(FileInfo item)
        {
            Label tmpLabel = new Label();
            tmpLabel.Content = "Error borrando: " + (item.Name);
            tmpLabel.Style = (Style)Application.Current.Resources["Label1"];
            return tmpLabel;
        }

        internal static Label getLabelVideosRenombrados(string nombreOriginal, FileInfo fi)
        {
            Label tmpLabel = new Label();
            tmpLabel.Content = nombreOriginal + "    =>    " + fi.Name ;
            tmpLabel.Style = (Style)Application.Current.Resources["Label1"];
            return tmpLabel;
        }

        internal static Label getLabelErrorRenombrando(string nombreOriginal)
        {
            Label tmpLabel = new Label();
            tmpLabel.Content = "Error renombrando: " + nombreOriginal;
            tmpLabel.Style = (Style)Application.Current.Resources["Label1"];
            return tmpLabel;
        }
    }
}
