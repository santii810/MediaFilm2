using MediaFilm2.Datos;

using MediaFilm2.Modelo;
using MediaFilm2.Vista;
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
        public PatronesXML PatronesXML;

        //Estructuras
        public List<Serie> series = new List<Serie>();

        public Serie serieSeleccionada;

        public MainWindow()
        {
            InitializeComponent();
            UpdateIU.Update(this, Codigos.ESTADO_INICIAL);

            config = configXML.leerConfig();
            LogErrorXML = new LoggerXML(config.errorLog);
            LogMediaXML = new LoggerXML(config.mediaLog);
            LogMediaXML = new LoggerXML(config.datosLog);
            SeriesXML = new SeriesXML(config);
            PatronesXML = new PatronesXML(config);
        }


        internal void actualizarListaSeries()
        {
            series = SeriesXML.leerSeries();
            series.Sort();
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

        private void ImageAddSerie_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            UpdateIU.Update(this, Codigos.ADD_SERIE);

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

        private void ButtonAñadirSerie_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string[] extensiones = { ".mkv", ".avi", ".mp4" };
                if (Validar.validarAddSerie(this))
                {
                    SeriesXML.añadirSerie(new Serie
                    {
                        titulo = textBoxTitulo.Text.Trim(),
                        capitulosPorTemporada = Convert.ToInt32(textBoxCapitulosTemporada.Text.Trim()),
                        estado = "A",
                        extension = extensiones[comboBoxExtensionSerie.SelectedIndex],
                        numeroTemporadas = Convert.ToInt32(textBoxNumeroTemporadas.Text.Trim()),
                        temporadaActual = 1
                    });

                    MessageBox.Show("Serie añadida correctamente");
                    UpdateIU.Update(this, Codigos.ADD_SERIE_OK);

                    actualizarListaSeries();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Datos insertados incorrectos");
            }
        }


        private void ImageAddPatron_MouseLeftButtonUp_1(object sender, MouseButtonEventArgs e)
        {
            actualizarListaSeries();
            UpdateIU.Update(this, Codigos.ADD_PATRON);
        }


        private void ButtonAddPatron_Click(object sender, RoutedEventArgs e)
        {
           if(Validar.validarAddPatron(this))
            {
                PatronesXML.añadirPatron(new Patron { nombreSerie = serieSeleccionada.titulo, textoPatron = textBoxNuevoPatron.Text.Trim() });
                serieSeleccionada.getPatrones(config);
                UpdateIU.Update(this, Codigos.ADD_PATRON_OK);
            }
        }
    }
}
