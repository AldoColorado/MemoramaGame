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
    /// Lógica de interacción para Juego.xaml
    /// </summary>
    public partial class Juego : ProxyJuego.IJuegoServiceCallback , ProxyEstadisticas.IEstadisticasServiceCallback
    {
        JuegoMemorama juego = new JuegoMemorama();
        Jugador jugador;
        int puntaje = 0;
        Canvas canvas2;
        InstanceContext contexto;
        ProxyJuego.JuegoServiceClient servidor;
        InstanceContext contextoEstadisticas;
        ProxyEstadisticas.EstadisticasServiceClient servidorEstadisticas;
        Partida partida;
        EstadisticaPartida estadisticaJugador;



        ObservableCollection<int> numeros = new ObservableCollection<int>();
        ObservableCollection<int> puntajesJugadores;

        private ObservableCollection<Jugador> jugadoresJuego;

        public Juego(JuegoMemorama juego, Jugador jugador, Partida partida)
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            InitializeComponent();

            contexto = new InstanceContext(this);
            contextoEstadisticas = new InstanceContext(this);
            servidor = new ProxyJuego.JuegoServiceClient(contexto);
            servidorEstadisticas = new ProxyEstadisticas.EstadisticasServiceClient(contextoEstadisticas);

            this.partida = partida;

            jugadoresJuego = new ObservableCollection<Jugador>();
            puntajesJugadores = new ObservableCollection<int>();
            estadisticaJugador = new EstadisticaPartida();
            this.jugador = jugador;
            this.juego = juego;

            TxtNombreJugador.Text = jugador.nickName;
            servidor.ConectarseJuego(jugador);
            servidor.InicializarPuntajes(jugador, 0);

            jugadoresEnJuego.Items.Clear();
            jugadoresEnJuego.ItemsSource = jugadoresJuego;

            puntajes.Items.Clear();
            puntajes.ItemsSource = puntajesJugadores;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = juego;
            NuevoJuego();
        }

        private void NuevoJuego()
        {
            try
            {
                juego.NuevoJuego();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                Close();
            }
        }

        private void Canvas2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            bool resultadoCartasVolteadas = false;

            try
            {
                resultadoCartasVolteadas = juego.Click(e.GetPosition(canvas2).X, e.GetPosition(canvas2).Y);
                servidor.MovimientoDeJugador(e.GetPosition(canvas2).X, e.GetPosition(canvas2).Y);
                if(resultadoCartasVolteadas)
                {
                    puntaje++;
                    servidor.ModificarPuntajes(jugador, puntaje);
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
                MessageBox.Show(ex.ToString());
                throw;
            }
        }

        public void MostrarMovimiento(double x, double y)
        {
            juego.Click(x, y);
        }

        public void OrdenCartas(int[] numeros)
        {
            foreach(int n in numeros)
            {
                this.numeros.Add(n);
            }
        }

        public void JugadoresEnJuego(Jugador[] jugadores)
        {
            jugadoresJuego.Clear();

            foreach(Jugador c in jugadores)
            {
                jugadoresJuego.Add(c);
            }
        }

        public void ActualizarPuntajes(int[] puntajes)
        {
            puntajesJugadores.Clear();

            foreach(var p in puntajes)
            {
                puntajesJugadores.Add(p);
            }
        }

        private void botonSalir(object sender, RoutedEventArgs e)
        {
            AsignarPuntajes();

            try
            {
                servidorEstadisticas.GuardarEstadisticasPartida(estadisticaJugador, jugador, partida);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void AsignarPuntajes()
        {
            int paresJugador = 0;
            int contadorPosicionJugadores = 0;
            int contadorPosicionPuntos = 0;
            int posicionJugadorEnArreglo = 0;

            foreach(var j in jugadoresJuego)
            {
                if(j.nickName == jugador.nickName)
                {
                    posicionJugadorEnArreglo = contadorPosicionJugadores;
                }
                contadorPosicionJugadores++;
            }

            foreach(var p in puntajesJugadores)
            {
                if(contadorPosicionPuntos == posicionJugadorEnArreglo)
                {
                    paresJugador = p;
                }
                contadorPosicionPuntos++;
            }

            estadisticaJugador.paresObtenidos = paresJugador;
            estadisticaJugador.puntaje = paresJugador;
        }


        public void MostrarEstadistica()
        {
            throw new NotImplementedException();
        }
    }
}