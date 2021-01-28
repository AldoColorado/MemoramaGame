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
    public partial class Juego : ProxyJuego.IJuegoServiceCallback
    {
        JuegoMemorama juego = new JuegoMemorama();
        Jugador jugador;
        int puntaje = 0;
        Canvas canvas2;
        InstanceContext contexto;
        ProxyJuego.JuegoServiceClient servidor;
        InstanceContext contextoEstadisticas;
        ProxyEstadisticas.EstadisticasServiceClient servidorEstadisticas;
        ObservableCollection<Jugador> jugadores = new ObservableCollection<Jugador>();
        Partida partida;
        EstadisticaPartida estadisticaJugador;
        int statusReporteJugador = 0;
        string jugadorReportado = "";
        ObservableCollection<int> numeros = new ObservableCollection<int>();
        ObservableCollection<int> puntajesJugadores;
        ObservableCollection<Jugador> jugadoresJuego;
        ObservableCollection<string> jugadoresEnLinea;
        ObservableCollection<string> jugadoresQueReportan;

        /// <summary>
        /// Constructor de la clase
        /// </summary>
        /// <param name="juego">Clase de la funcionalidad de la ventana</param>
        /// <param name="jugador">Jugador actual</param>
        /// <param name="partida">Partida en curso</param>
        /// <param name="jugadoresConectados">Coleccion de jugadores concetados</param>
        public Juego(JuegoMemorama juego, Jugador jugador, Partida partida, ObservableCollection<Jugador> jugadoresConectados)
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            InitializeComponent();

            this.jugadores = jugadoresConectados;
            contexto = new InstanceContext(this);
            contextoEstadisticas = new InstanceContext(this);
            servidor = new ProxyJuego.JuegoServiceClient(contexto);
            servidorEstadisticas = new ProxyEstadisticas.EstadisticasServiceClient();
            this.partida = partida;
            jugadoresJuego = new ObservableCollection<Jugador>();
            puntajesJugadores = new ObservableCollection<int>();
            jugadoresEnLinea = new ObservableCollection<string>();
            estadisticaJugador = new EstadisticaPartida();
            jugadoresQueReportan = new ObservableCollection<string>();

            this.jugador = jugador;
            this.juego = juego;

            TxtNombreJugador.Text = jugador.nickName;
            servidor.ConectarseJuego(jugador);
            servidor.InicializarPuntajes(jugador, 0);

            foreach(Jugador j in jugadoresJuego)
            {
                jugadoresEnLinea.Add(j.nickName);
            }

            jugadoresEnJuego.Items.Clear();
            jugadoresEnJuego.ItemsSource = jugadoresJuego;

            puntajes.Items.Clear();
            puntajes.ItemsSource = puntajesJugadores;
        }

        /// <summary>
        /// Evento de carga de la ventana
        /// </summary>
        /// <param name="sender">Propiedad del evento</param>
        /// <param name="e">Propiedad del evento</param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = juego;
            NuevoJuego();
        }

        /// <summary>
        /// Clase para crear un nuevo juego
        /// </summary>
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

        /// <summary>
        /// Evento de clic del Raton
        /// </summary>
        /// <param name="sender">Propieadad del evento</param>
        /// <param name="e">Propiedad del evento</param>
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
                throw;
            }
        }

        /// <summary>
        /// Metodo para mostrar el movimiento de las cartas en todos los jugadores conectados
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void MostrarMovimiento(double x, double y)
        {
            juego.Click(x, y);
        }

        /// <summary>
        /// Metodo para crear el orden de las cartas
        /// </summary>
        /// <param name="numeros">Arreglo de numeros </param>
        public void OrdenCartas(int[] numeros)
        {
            foreach(int n in numeros)
            {
                this.numeros.Add(n);
            }
        }

        /// <summary>
        /// Metodo para actualizar los jugadores conectados
        /// </summary>
        /// <param name="jugadores">Lista de jugadores conectados</param>
        public void JugadoresEnJuego(Jugador[] jugadores)
        {
            jugadoresJuego.Clear();

            foreach(Jugador c in jugadores)
            {
                jugadoresJuego.Add(c);
            }
        }

        /// <summary>
        /// Metodo par actualizar los puntajes de la tabla
        /// </summary>
        /// <param name="puntajes">Arreglo de puntajes</param>
        public void ActualizarPuntajes(int[] puntajes)
        {
            puntajesJugadores.Clear();

            foreach(var p in puntajes)
            {
                puntajesJugadores.Add(p);
            }
        }

        /// <summary>
        /// Evento para el boton salir
        /// </summary>
        /// <param name="sender">Propiedad del evento</param>
        /// <param name="e">Propiedad del evento</param>
        private void botonSalir(object sender, RoutedEventArgs e)
        {
            AsignarPuntajes();

            try
            {
                servidorEstadisticas.GuardarEstadisticasPartida(estadisticaJugador, jugador, partida);
                Lobby ventanaLobby = new Lobby(jugadores , jugador);
                Window.GetWindow(this).Close();
                ventanaLobby.Show();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// Metodo para asignar puntajes a jugadores cuando se termina la partida
        /// </summary>
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

        /// <summary>
        /// Evento del boton reportar
        /// </summary>
        /// <param name="sender">Propiedad del evento</param>
        /// <param name="e">Propiedad del evento</param>
        private void botonReportar(object sender, RoutedEventArgs e)
        {
            if(jugadorReportado != null)
            {
                try
                {
                    servidor.ReportarJugador(jugadorReportado, jugador);
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else
            {
                MessageBox.Show("Tienes que elegir un jugador para reportarlo");
            }
        }

        /// <summary>
        /// Evento de seleccion de la tabla
        /// </summary>
        /// <param name="sender">Propiedad del evento</param>
        /// <param name="e">Propiedad del evento</param>
        private void Seleccion(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                jugadorReportado = ((Jugador)jugadoresEnJuego.SelectedItem).nickName;   
            }
            catch(Exception ex)
            {
                
            }
        }

        /// <summary>
        /// Metodo para actualizar el reporte del jugador
        /// </summary>
        public void ActualizarReporteJugador(Jugador jugadorQueReporta)
        {
            bool jugadorYaReporto = false;

            foreach(var jugador in jugadoresQueReportan)
            {
                if(jugadorQueReporta.nickName.Equals(jugador))
                {
                    jugadorYaReporto = true;
                }
            }

            if(!jugadorYaReporto)
            {
                jugadoresQueReportan.Add(jugadorQueReporta.nickName);

                int numeroDeJugadores = 0;
                statusReporteJugador++;

                foreach(var jugadores in jugadoresJuego)
                {
                    numeroDeJugadores++;
                }

                switch(numeroDeJugadores)
                {

                    case 2:
                        if(statusReporteJugador == 1)
                        {
                            ExpulsarJugador();
                        }
                        break;

                    case 3:
                        if(statusReporteJugador == 2)
                        {
                            ExpulsarJugador();
                        }
                        break;

                    case 4:
                        if(statusReporteJugador == 3)
                        {
                            ExpulsarJugador();
                        }
                        break;
                }

            }
        }

        /// <summary>
        /// Metodo para sacar a un jugador de la partida en caso de que sea reportado
        /// </summary>
        public void ExpulsarJugador()
        {
            MessageBox.Show("Se te ha reportado por todos los jugadores en la partida, estas fuera de la partida");

            Lobby ventanaLobby = new Lobby(jugadoresJuego, jugador);
            Window.GetWindow(this).Close();
            ventanaLobby.Show();
        }
    }
}