using Memorama.ProxyLogin;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Memorama
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class Login : ProxyLogin.ILoginServiceCallback
    {
        public bool aceptado = false;
        private ObservableCollection<Jugador> jugadoresConectados;
        Dictionary<Jugador, ILoginServiceCallback> jugadoresEnLinea = new Dictionary<Jugador, ILoginServiceCallback>();
        InstanceContext contexto;
        ProxyLogin.LoginServiceClient servidor;

        public Login()
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            InitializeComponent();
            jugadoresConectados = new ObservableCollection<Jugador>();

            contexto = new InstanceContext(this);
            servidor = new ProxyLogin.LoginServiceClient(contexto);
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void BotonAjustes(object sender, RoutedEventArgs e)
        {
            Ajustes ventanaAjustes = new Ajustes();
            ventanaAjustes.Show();

        }

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
                MessageBox.Show("Usuario o contraseña incorrecta");
            }
        }

        private void BotonRegistrarse(object sender, RoutedEventArgs e)
        {
            Registrarse ventanaRegistrase = new Registrarse();
            Window.GetWindow(this).Close();
            ventanaRegistrase.Show();
        }

        private void BotonRecuperarContrasenia(object sender, RoutedEventArgs e)
        {
            RecuperarContrasenia ventanaRecuperarContrasenia = new RecuperarContrasenia();
            Window.GetWindow(this).Close();
            ventanaRecuperarContrasenia.Show();
        }

        public void VerificarUsuarioLogeado(bool logeado)
        {
            this.aceptado = logeado;
        }

        public bool Logearse()
        {
            bool logeado = false;

            try
            {
                logeado = servidor.Login(TextoNickName.Text, TextoPassword.Password);
            }
            catch(Exception ex)
            {
                MessageBox.Show("ERROR: El servidor no esta disponible, intente de nuevo más tarde");
                MessageBox.Show(ex.ToString());

                Application.Current.Shutdown();
            }

            return logeado;
        }

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
                    MessageBox.Show("El jugador ya se encuentra conectado");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("ERROR: El servidor no esta disponible, intente de nuevo más tarde");
                Application.Current.Shutdown();
            }
        }

        public void UsuariosConectados(Jugador[] jugadores)
        {

            jugadoresConectados.Clear();

            foreach(Jugador c in jugadores)
            {
                jugadoresConectados.Add(c);
            }

        }

        public void MostrarVentanaLoby(Jugador jugador)
        {
            Lobby ventanaLobby = new Lobby(jugadoresConectados, jugador);
            Window.GetWindow(this).Close();
            ventanaLobby.Show();
        }

    }
}
