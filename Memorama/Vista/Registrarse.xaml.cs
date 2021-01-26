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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Modelo.Modelo;

namespace Memorama
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class Registrarse : ProxyRegistro.IRegistroServiceCallback
    {
        
        Jugador jugador = new Jugador();
        public string codigo;
        public bool correoEnviado;

        public Registrarse()
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            InitializeComponent();
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void BotonRegistrarse(object sender, RoutedEventArgs e)
        {
            string contraseniaEncriptada = string.Empty;
            byte[] encryted = System.Text.Encoding.Unicode.GetBytes(TextoPassword.Password);
            contraseniaEncriptada = Convert.ToBase64String(encryted);


            jugador.nickName = TextoNickName.Text;
            jugador.nombre = TextoNombre.Text;
            jugador.correoElectronico = TextoCorreo.Text;
            jugador.contrasenia = contraseniaEncriptada;

            GenerarCodigoRegistro();

            InstanceContext contexto = new InstanceContext(this);
            ProxyRegistro.RegistroServiceClient servidor = new ProxyRegistro.RegistroServiceClient(contexto);

            servidor?.EnviarCorreoRegistro(TextoCorreo.Text, codigo);
           
            ConfirmarRegistro ventanaConfirmarRegistro = new ConfirmarRegistro(jugador, codigo);
            ventanaConfirmarRegistro.Show();
            Window.GetWindow(this).Close();
        }

        public void GenerarCodigoRegistro()
        {
            var seed = Environment.TickCount;
            var random = new Random(seed);

            for(int i = 0; i <= 4; i++)
            {
                var value = random.Next(0, 9);
                codigo += value;
            }
        }

        public void VerificarCreacionJugador(bool creado)
        {
            throw new NotImplementedException();
        }

        public void VerificarEnvioDeCorreo(bool enviado)
        {
            this.correoEnviado = enviado;
        }
    }
}