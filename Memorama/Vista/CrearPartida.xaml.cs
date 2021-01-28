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
    /// Lógica de interacción para CrearPartida.xaml
    /// </summary>
    public partial class CrearPartida : ProxyPartida.IPartidaServiceCallback
    {
        Partida partida;
        InstanceContext contexto;
        ProxyPartida.PartidaServiceClient servidor;
        Jugador jugador;
        string codigoPartida;
        ObservableCollection<Jugador> jugadores;

        /// <summary>
        /// Constructor de la clase
        /// </summary>
        /// <param name="jugadores">Lista de jugadores conectados</param>
        /// <param name="jugador">Jugador actual</param>
        public CrearPartida(ObservableCollection<Jugador> jugadores, Jugador jugador)
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            InitializeComponent();
            this.jugador = jugador;
            this.jugadores = jugadores;
            partida = new Partida();

            contexto = new InstanceContext(this);
            servidor = new ProxyPartida.PartidaServiceClient(contexto);
        }

        /// <summary>
        /// Metodo no implementado
        /// </summary>
        /// <param name="jugadores">Jugadores conectados</param>
        public void JugadoresEnPartida(Jugador[] jugadores)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Metodo no implementado
        /// </summary>
        /// <param name="numeros">Arreglo de numeros</param>
        public void OrdenCartas(int[] numeros)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Evento del boton crear partida
        /// </summary>
        /// <param name="sender">Propiedad del evento</param>
        /// <param name="e">Propiedad del evento</param>
        private void BotonCrearPartida(object sender, RoutedEventArgs e)
        {
            string codigoConTexto = "CODIGO: ";
            codigoPartida= servidor.GenerarCodigo();
            TxtCodigo.Text = codigoConTexto + codigoPartida;

            partida.codigo = codigoPartida;   
        }

        /// <summary>
        /// Evento del boron iniciar partida
        /// </summary>
        /// <param name="sender">Propiedad del evento</param>
        /// <param name="e">Propiedad del evento</param>
        private void BotonIniciarPartida(object sender, RoutedEventArgs e)
        {
            bool creada = false;

            if(codigoPartida != null)
            {
                try
                {
                    creada = servidor.CrearPartida(partida, jugador);
                    servidor.CrearEstadisticaPartida(partida, jugador);
                }
                catch(Exception ex)
                {
                    MessageBox.Show("ERROR: El servidor no esta disponible, intenta más tarde");
                    Window.GetWindow(this).Close();
                }

                if(creada)
                {
                    PrePartida ventanaPrePartida = new PrePartida(jugadores, jugador, codigoPartida);
                    Window.GetWindow(this).Close();
                    ventanaPrePartida.Show();
                }
            }
            else
            {
                MessageBox.Show("Por favor genera el codigo de la partida");
            }   
        }

        /// <summary>
        /// Evento del boton regresar
        /// </summary>
        /// <param name="sender">Propiedad del evento</param>
        /// <param name="e">Propiedad del evento</param>
        private void BotonRegresar(object sender, RoutedEventArgs e)
        {
            Lobby lobby = new Lobby(jugadores, jugador);
            Window.GetWindow(this).Close();
            lobby.Show();
        }
    }
}
