using Modelo.Modelo;
using System;
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
    /// Lógica de interacción para UnirseAPartida.xaml
    /// </summary>
    public partial class UnirseAPartida : ProxyPartida.IPartidaServiceCallback
    {
        Jugador jugador;
        Partida partida;
        string codigoPartida = "";
        InstanceContext contexto;
        ProxyPartida.PartidaServiceClient servidor;
        ObservableCollection<Jugador> jugadoresConectados;

        /// <summary>
        /// Constructor de la clase
        /// </summary>
        /// <param name="jugadores">Jugadores conectados al juego</param>
        /// <param name="jugador">Jugador en en juego actual</param>
        public UnirseAPartida(ObservableCollection<Jugador> jugadores, Jugador jugador)
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            InitializeComponent();
            jugadoresConectados = new ObservableCollection<Jugador>();
            this.jugadoresConectados = jugadores;
            this.jugador = jugador;
            partida = new Partida();
            contexto = new InstanceContext(this);
            servidor = new ProxyPartida.PartidaServiceClient(contexto);

        }

        /// <summary>
        /// Evento del boton unirse
        /// </summary>
        /// <param name="sender">Propiedad del evento</param>
        /// <param name="e">Propiedad del evento</param>
        private void BotonUnirse(object sender, RoutedEventArgs e)
        {
            IngresarCodigo();
            bool estadisticaCreada = false;

            if(servidor.ComprobarCodigoPartida(codigoPartida))
            {
                try
                {
                    servidor.CrearEstadisticaPartida(partida, jugador);
                    estadisticaCreada = true;
                }
                catch(Exception ex)
                {
                    MessageBox.Show("No existe partida en curso");
                }

                if(estadisticaCreada)
                {
                    PrePartida ventanaPrePartida = new PrePartida(jugadoresConectados, jugador, codigoPartida);
                    ventanaPrePartida.Show();
                    Window.GetWindow(this).Close();
                }  
            }
            else
            {
                MessageBox.Show("El codigo no corresponde a ninguna partida en curso");
            }   
        }

        /// <summary>
        /// Metodo para inicializar el codigo ingresado de la partida
        /// </summary>
        public void IngresarCodigo()
        {
            codigoPartida = TextoCodigo.Text;
            partida.codigo = TextoCodigo.Text;
        }

        /// <summary>
        /// Metodo para verificar los jugadores en la partida
        /// </summary>
        /// <param name="jugadores"></param>
        public void JugadoresEnPartida(Jugador[] jugadores)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Meodo no implementado
        /// </summary>
        /// <param name="numeros">Orden de las cartas</param>
        public void OrdenCartas(int[] numeros)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Evento del boton regresar
        /// </summary>
        /// <param name="sender">Propiedad del evento</param>
        /// <param name="e">Propiedad del evento</param>
        private void BotonRegresar(object sender, RoutedEventArgs e)
        {
            Lobby lobby = new Lobby(jugadoresConectados, jugador);
            Window.GetWindow(this).Close();
            lobby.Show();
        }
    }
}
