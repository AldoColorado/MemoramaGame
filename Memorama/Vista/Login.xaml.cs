﻿using Memorama.ProxyLogin;
using Modelo.Modelo;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Memorama
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class Login : ProxyLogin.ILoginServiceCallback
    {
        ResourceManager recurso;
        bool aceptado = false;
        ObservableCollection<Jugador> jugadoresConectados;
        Dictionary<Jugador, ILoginServiceCallback> jugadoresEnLinea = new Dictionary<Jugador, ILoginServiceCallback>();
        InstanceContext contexto;
        ProxyLogin.LoginServiceClient servidor;

        /// <summary>
        /// Constructor de la clase
        /// </summary>
        public Login()
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            InitializeComponent();
            jugadoresConectados = new ObservableCollection<Jugador>();

            this.recurso = new ResourceManager("Memorama.Properties.Idiomas.Idioma", Assembly.GetExecutingAssembly());

            contexto = new InstanceContext(this);
            servidor = new ProxyLogin.LoginServiceClient(contexto);
        }

        /// <summary>
        /// Metodo que maneja el evento de movimiento de la pantalla
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        /// <summary>
        /// Metodo para abrir la ventana de ajustes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BotonAjustes(object sender, RoutedEventArgs e)
        {
            Ajustes ventanaAjustes = new Ajustes();
            ventanaAjustes.Show();

        }

        /// <summary>
        /// Metodo para ingresar a el lobby del juego
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BotonIngresar(object sender, RoutedEventArgs e)
        {
            Jugador jugador = new Jugador();
            jugador.nickName = TextoNickName.Text;
            jugador.contrasenia = TextoPassword.Password;

            if(Logearse())
            {
                Conectarse(jugador);
            }
            else
            {
                string msj1 = this.recurso.GetString("vLoginMsj1");
                MessageBox.Show(msj1);
            } 
        }

        /// <summary>
        /// Metodo que abre la ventana de registrarse
        /// </summary>
        /// <param name="sender">Propiedad del evento</param>
        /// <param name="e">Propiedad del evento</param>
        private void BotonRegistrarse(object sender, RoutedEventArgs e)
        {
            Registrarse ventanaRegistrase = new Registrarse();
            Window.GetWindow(this).Close();
            ventanaRegistrase.Show();
        }

        /// <summary>
        /// Meotdo que abre la ventana de recuperar contrasenia
        /// </summary>
        /// <param name="sender">Propiedad del evento</param>
        /// <param name="e">Propiedad del evento</param>
        private void BotonRecuperarContrasenia(object sender, RoutedEventArgs e)
        {
            RecuperarContrasenia ventanaRecuperarContrasenia = new RecuperarContrasenia();
            Window.GetWindow(this).Close();
            ventanaRecuperarContrasenia.Show();
        }

        /// <summary>
        /// Metodo para verificar si es correcto el logeo del jugador
        /// </summary>
        /// <param name="logeado">Verdadero cuando sus creedenciales son correctas</param>
        public void VerificarUsuarioLogeado(bool logeado)
        {
            this.aceptado = logeado;
        }

        /// <summary>
        /// Metodo para logearse dentro del sistema
        /// </summary>
        /// <returns>Regresa verdadero en caso de que las creedenciales sean correctas</returns>
        public bool Logearse()
        {
            bool logeado = false;

            try
            {
                logeado = servidor.Login(TextoNickName.Text, TextoPassword.Password);
            }
            catch(CommunicationException ex)
            {
                string msj2 = this.recurso.GetString("vLoginMsj2"+ex.ToString());
                MessageBox.Show(msj2);
                Application.Current.Shutdown();
            }

            return logeado;
        }

        /// <summary>
        /// Metodo para conctarse al servidor
        /// </summary>
        /// <param name="jugador">Jugador que va a conectarse</param>
        public void Conectarse(Jugador jugador)
        {
            bool yaEstaConectado = false;

            try
            {
                foreach(var j in servidor.ObtenerClientes())
                {
                    if(j.Key.nickName == jugador.nickName)
                    {
                        yaEstaConectado = true;
                    }
                }

                if(!yaEstaConectado)
                {
                    servidor.Conectarse(jugador);
                    MostrarVentanaLoby(jugador);
                }
                else
                {
                    string msj3 = this.recurso.GetString("vLoginMsj3");
                    MessageBox.Show(msj3);
                }
            }
            catch(CommunicationException ex)
            {
                string msj2 = this.recurso.GetString("vLoginMsj2"+ex.ToString());
                MessageBox.Show(msj2);
                Application.Current.Shutdown();
            }
        }

        /// <summary>
        /// Metodo para actualizar los jugadores que se conectan al servidor
        /// </summary>
        /// <param name="jugadores">Arreglo de jugadores conectados</param>
        public void UsuariosConectados(Jugador[] jugadores)
        {
            jugadoresConectados.Clear();

            foreach(Jugador c in jugadores)
            {
                jugadoresConectados.Add(c);
            }

        }

        /// <summary>
        /// Metodo para abrir la ventana de Lobby
        /// </summary>
        /// <param name="jugador">Jugador que se ha logeado</param>
        public void MostrarVentanaLoby(Jugador jugador)
        {
            Lobby ventanaLobby = new Lobby(jugadoresConectados, jugador);
            Window.GetWindow(this).Close();
            ventanaLobby.Show();
        }

    }
}
