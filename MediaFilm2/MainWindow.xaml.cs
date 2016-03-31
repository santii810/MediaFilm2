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
        public SeriesXML SeriesXML;

        //Estructuras
        public List<Serie> series = new List<Serie>();


        public MainWindow()
        {
            InitializeComponent();
            UpdateIU.Update(this, Codigos.ESTADO_INICIAL);

            config = configXML.leerConfig();
            LogErrorXML = new LoggerXML(config.errorLog);
            LogMediaXML = new LoggerXML(config.mediaLog);
            LogMediaXML = new LoggerXML(config.datosLog);
            SeriesXML = new SeriesXML(config);
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
            UpdateIU.Update(this, Codigos.LIMPIAR_ANTIGUOS_RESULTADOS_RECOGER);
            GestorVideos.recogerTorrent(this);
            UpdateIU.Update(this, Codigos.MOSTRAR_RESULTADOS_RECOGER);
        }

        private void RenombrarVideos_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            UpdateIU.Update(this, Codigos.LIMPIAR_ANTIGUOS_RESULTADOS_RENOMBRAR);
            GestorVideos.renombrarVideos(this);
            UpdateIU.Update(this, Codigos.MOSTRAR_RESULTADOS_RENOMBRAR);
        }

        private void AddDatos_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            UpdateIU.Update(this, Codigos.PANEL_ADD_DATOS);
        }

        private void ButtonAñadirSerie_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string[] extensiones = { ".mkv", ".avi", ".mp4" };
                if (Validar.validarAddSerie(this))
                {
                    SeriesXML.añadirSerie(new Serie
                    {
                        titulo = textBoxTitulo.Text,
                        capitulosPorTemporada = Convert.ToInt32(textBoxCapitulosTemporada.Text),
                        estado = "A",
                        extension = extensiones[comboBoxExtensionSerie.SelectedIndex],
                        numeroTemporadas = Convert.ToInt32(textBoxNumeroTemporadas.Text),
                        temporadaActual = 1
                    });

                    MessageBox.Show("Serie añadida correctamente");
                    UpdateIU.Update(this, Codigos.COD_ADD_SERIE_OK);

                }
            }
            catch (Exception)
            {
                MessageBox.Show("Datos insertados incorrectos");
            }
        }

        private void textBoxCapitulosTemporada_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void textBoxNumeroTemporadas_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void Image_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void ImageAddSerie_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            UpdateIU.Update(this, Codigos.COD_ADD_SERIE);
        }
    }
}
