using Modelo.Modelo;
using System;
using System.Collections.Generic;
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

        public CrearPartida(Jugador jugador)
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            InitializeComponent();
            this.jugador = jugador;

            partida = new Partida();

            contexto = new InstanceContext(this);
            servidor = new ProxyPartida.PartidaServiceClient(contexto);
        }

        public void JugadoresEnPartida(Jugador[] jugadores)
        {
            throw new NotImplementedException();
        }

        public void OrdenCartas(int[] numeros)
        {
            throw new NotImplementedException();
        }

        private void BotonCrearPartida(object sender, RoutedEventArgs e)
        {
            string codigoConTexto = "CODIGO: ";
            
            codigoPartida= servidor.GenerarCodigo();
            TxtCodigo.Text = codigoConTexto + codigoPartida;

            partida.codigo = codigoPartida;   
        }

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
                    PrePartida ventanaPrePartida = new PrePartida(jugador, codigoPartida);
                    ventanaPrePartida.Show();
                    Window.GetWindow(this).Close();
                }
            }
            else
            {
                MessageBox.Show("Por favor genera el codigo de la partida");
            }   
        }
    }
}
