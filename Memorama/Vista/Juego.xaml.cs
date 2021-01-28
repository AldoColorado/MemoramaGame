﻿using Modelo.Modelo;
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
                Lobby ventanaLobby = new Lobby(jugadores , jugador);
                Window.GetWindow(this).Close();
                ventanaLobby.Show();

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

        private void botonReportar(object sender, RoutedEventArgs e)
        {
            if(jugadorReportado != null)
            {
                try
                {
                    servidor.ReportarJugador(jugadorReportado);
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

        private void Seleccion(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                jugadorReportado = ((Jugador)jugadoresEnJuego.SelectedItem).nickName;
                
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void ActualizarReporteJugador()
        {
            statusReporteJugador++;

            if(statusReporteJugador == 3)
            {
                MessageBox.Show("Se te ha reportado hasta en 3 ocasiones, estas fuera de la partida");

                Lobby ventanaLobby = new Lobby(jugadoresJuego, jugador);
                Window.GetWindow(this).Close();
                ventanaLobby.Show();
            }
        }
    }
}