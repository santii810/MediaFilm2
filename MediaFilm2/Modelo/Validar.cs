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
            if (mainWindow.textBoxTitulo.Text.Trim() == "")
            {
                MessageBox.Show("El titulo de la serie no puede estar vacio");
                return false;
            }
            if (mainWindow.textBoxTitulo.Text.Trim().Length < 3)
            {
                MessageBox.Show("El titulo de la serie debe ser de al menos 3 caracteres");
                return false;
            }
            if (mainWindow.textBoxCapitulosTemporada.Text.Trim() == "")
            {
                mainWindow.textBoxCapitulosTemporada.Text = "25";
            }
            if (mainWindow.textBoxNumeroTemporadas.Text.Trim() == "")
            {
                mainWindow.textBoxNumeroTemporadas.Text = "1";
            }
            if (mainWindow.comboBoxExtensionSerie.SelectedIndex == -1)
            {
                mainWindow.comboBoxExtensionSerie.SelectedIndex = 0;
            }
            return true;
        }

        internal static bool validarAddPatron(MainWindow mainWindow)
        {
            if (mainWindow.textBoxNuevoPatron.Text.Trim() == "")
                return false;
            if (mainWindow.textBoxNuevoPatron.Text.Trim().Length < 2)
                return false;
            return true;
        }
    }
}
