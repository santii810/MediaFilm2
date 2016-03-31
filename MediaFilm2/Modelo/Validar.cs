using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MediaFilm2.Modelo
{
    public static class Validar
    {

        public static bool validarAddSerie(MainWindow mainWindow)
        {
            if (mainWindow.textBoxTitulo.Text == "")
            {
                MessageBox.Show("El titulo de la serie no puede estar vacio");
                return false;
            }
            if (mainWindow.textBoxCapitulosTemporada.Text == "")
            {
                mainWindow.textBoxCapitulosTemporada.Text = "25";
            }
            if (mainWindow.textBoxNumeroTemporadas.Text == "")
            {
                mainWindow.textBoxNumeroTemporadas.Text = "1";
            }
            if(mainWindow.comboBoxExtensionSerie.SelectedIndex == -1)
            {
                mainWindow.comboBoxExtensionSerie.SelectedIndex = 0;
            }


            return true;
        }
    }
}
