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

        public void IngresarCodigo()
        {
            codigoPartida = TextoCodigo.Text;
            partida.codigo = TextoCodigo.Text;
        }

        public void JugadoresEnPartida(Jugador[] jugadores)
        {
            throw new NotImplementedException();
        }

        public void OrdenCartas(int[] numeros)
        {
            throw new NotImplementedException();
        }

        private void BotonRegresar(object sender, RoutedEventArgs e)
        {
            Lobby lobby = new Lobby(jugadoresConectados, jugador);
            Window.GetWindow(this).Close();
            lobby.Show();
        }
    }
}
