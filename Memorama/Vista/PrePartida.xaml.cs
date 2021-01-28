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

        public void JugadoresEnPartida(Jugador[] jugadores)
        {

            jugadoresConectados.Clear();

            foreach(Jugador c in jugadores)
            {
                jugadoresConectados.Add(c);
            }
        }

        private void BotonJugar(object sender, RoutedEventArgs e)
        {
            Juego ventana = new Juego(juego, jugador, partida, jugadoresEnLinea);
            Window.GetWindow(this).Close();
            ventana.Show();
        }

        private void BotonRegresarAlLobby(object sender, RoutedEventArgs e)
        {
            servidor.DesconectarseDePartida(jugador);
            AbrirVentanaLobby();
        }

        public void AbrirVentanaLobby()
        {
            Lobby ventanLobby = new Lobby(jugadoresEnLinea, jugador);
            Window.GetWindow(this).Close();
            ventanLobby.Show();
        }

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
