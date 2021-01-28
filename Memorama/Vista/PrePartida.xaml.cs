using Modelo.Modelo;
using System;
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
    /// Lógica de interacción para PrePartida.xaml
    /// </summary>
    public partial class PrePartida : ProxyPartida.IPartidaServiceCallback
    {
        InstanceContext contexto;
        ProxyPartida.PartidaServiceClient servidor;
        Jugador jugador;
        JuegoMemorama juego = new JuegoMemorama();
        private ObservableCollection<Jugador> jugadoresConectados;
        ObservableCollection<int> numerosOrdenCartas;
        ObservableCollection<Jugador> jugadoresEnLinea;
        Partida partida;

        /// <summary>
        /// Constructor de la clase
        /// </summary>
        /// <param name="jugadores">Coleccion de jugadores conectados a la partida</param>
        /// <param name="jugador">Jugador en el juego</param>
        /// <param name="codigoPartida">codigo de la partida que se ha creado</param>
        public PrePartida(ObservableCollection<Jugador> jugadores, Jugador jugador, string codigoPartida)
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            InitializeComponent();

            jugadoresEnLinea = new ObservableCollection<Jugador>();
            this.jugadoresEnLinea = jugadores;
            partida = new Partida();
            this.jugador = jugador;
            contexto = new InstanceContext(this);
            servidor = new ProxyPartida.PartidaServiceClient(contexto);
            partida.codigo = codigoPartida;
            jugadoresConectados = new ObservableCollection<Jugador>();
            numerosOrdenCartas = new ObservableCollection<int>();

            try
            {
                servidor.AgregarJugador(jugador);
                servidor.GenerarOrdenCartas();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
           
            jugadoresUnidos.Items.Clear();
            jugadoresUnidos.ItemsSource = jugadoresConectados;
        }

        /// <summary>
        /// Metodo para actualizar los jugadores que se conectan a la partida
        /// </summary>
        /// <param name="jugadores">Arreglo de jugadores conectados</param>
        public void JugadoresEnPartida(Jugador[] jugadores)
        {
            jugadoresConectados.Clear();

            foreach(Jugador c in jugadores)
            {
                jugadoresConectados.Add(c);
            }
        }

        /// <summary>
        /// Metodo para jugar la partida
        /// </summary>
        /// <param name="sender">Propiedad del evento</param>
        /// <param name="e">Propiedad del evento</param>
        private void BotonJugar(object sender, RoutedEventArgs e)
        {
            Juego ventana = new Juego(juego, jugador, partida, jugadoresEnLinea);
            Window.GetWindow(this).Close();
            ventana.Show();
        }

        /// <summary>
        /// Metodo para regresar al lobby
        /// </summary>
        /// <param name="sender">Propiedad del evento</param>
        /// <param name="e">Propiedad del evento</param>
        private void BotonRegresarAlLobby(object sender, RoutedEventArgs e)
        {
            servidor.DesconectarseDePartida(jugador);
            AbrirVentanaLobby();
        }

        /// <summary>
        /// Metodo para abrir la ventana del lobby
        /// </summary>
        public void AbrirVentanaLobby()
        {
            Lobby ventanLobby = new Lobby(jugadoresEnLinea, jugador);
            Window.GetWindow(this).Close();
            ventanLobby.Show();
        }

        /// <summary>
        /// Metodo para actualizar el orden de las cartas
        /// </summary>
        /// <param name="numeros">Arreglo de numeros para establer el orden</param>
        public void OrdenCartas(int[] numeros)
        {
            numerosOrdenCartas.Clear();

            foreach(int n in numeros)
            {
                numerosOrdenCartas.Add(n);
            }

            juego.InicializarOrdenCartas(numerosOrdenCartas);
        }

    }
}
