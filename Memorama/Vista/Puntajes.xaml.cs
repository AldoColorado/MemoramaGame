using System;
using Modelo.Modelo;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Memorama.Vista
{
    /// <summary>
    /// Lógica de interacción para Puntajes.xaml
    /// </summary>
    public partial class Puntajes
    {
        ObservableCollection<Jugador> listaJugadores;
        ObservableCollection<Tabla> coleccionTabla;
        ObservableCollection<Jugador> jugadoresConectados;
        ObservableCollection<int> listaPuntajes;
        Jugador jugador = new Jugador();
        Jugador[] jugadores;
        int[] puntajes;
        string[] players;

        ProxyEstadisticas.EstadisticasServiceClient servidor;
        Tabla[] tablaPuntajes;

        /// <summary>
        /// Constructor de la clase
        /// </summary>
        /// <param name="jugadores">Collecion de jugadores conectados</param>
        /// <param name="jugador">Jugador actualmente logeado</param>
        public Puntajes(ObservableCollection<Jugador> jugadores, Jugador jugador)
        {
            InitializeComponent();

            servidor = new ProxyEstadisticas.EstadisticasServiceClient();
            puntajes = new int[10];
            players = new string[10];
            coleccionTabla = new ObservableCollection<Tabla>();
            this.jugadoresConectados = jugadores;
            this.jugador = jugador;

            try
            {
                servidor.GenerarTablaDePuntajes();

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            puntajes = servidor.ObtenerPuntaje();
            players = servidor.ObtenerJugadores();

            int contadorLista = 0;
            foreach(var p in players)
            {
                Tabla tabla = new Tabla();
                tabla.jugador = p;
                tabla.puntaje = puntajes[contadorLista];
                coleccionTabla.Add(tabla);
                contadorLista++;
            }

            listaViewPuntajes.Items.Clear();
            listaViewPuntajes.ItemsSource = coleccionTabla;
        }

        /// <summary>
        /// Evento click del boton regresar
        /// </summary>
        /// <param name="sender">Propiedad del evento</param>
        /// <param name="e">Propiedad del evento</param>
        private void BotonRegresar(object sender, RoutedEventArgs e)
        {
            Lobby ventanLobby = new Lobby(jugadoresConectados, jugador);
            Window.GetWindow(this).Close();
            ventanLobby.Show();
        }

        /// <summary>
        /// Metodo para inicializar a los jugadores 
        /// </summary>
        public void InicializarCollecionJugadores()
        {
            try
            {
                foreach(var j in jugadores)
                {
                    listaJugadores.Add(j);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        /// <summary>
        /// Metodo no implementado
        /// </summary>
        public void MostrarEstadistica()
        {
            throw new NotImplementedException();
        }

    }
}
