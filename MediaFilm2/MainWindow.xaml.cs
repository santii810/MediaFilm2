using MediaFilm2.Datos;
using MediaFilm2.Iconos;
using MediaFilm2.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MediaFilm2
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Config config;

        //Gestores XML
        private ConfigXML configXML = new ConfigXML();
        public LoggerXML LogErrorXML;
        public LoggerXML LogMediaXML;
        public LoggerXML LogDatosXML;

        public MainWindow()
        {
            InitializeComponent();
            config = configXML.leerConfig();
            LogErrorXML = new LoggerXML(config.errorLog);
            LogMediaXML = new LoggerXML(config.mediaLog);
            LogMediaXML = new LoggerXML(config.datosLog);
        }



        /// <summary>
        /// Handles the MouseLeftButtonUp event of the OrdenarVideos control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MouseButtonEventArgs"/> instance containing the event data.</param>
        private void OrdenarVideos_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            UpdateIU.Update(this, Codigos.PANEL_ORDENAR_VIDEOS);
        }


        private void RecogerVideos_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            GestorVideos.recogerTorrent(this);

        }
    }
}
