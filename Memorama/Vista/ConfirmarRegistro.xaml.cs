
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Memorama
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class ConfirmarRegistro : ProxyRegistro.IRegistroServiceCallback
    {
   
        private bool creado;
        private Jugador jugador;
        private string codigo;

        public ConfirmarRegistro(Jugador jugador, string codigo)
        {
            this.jugador = jugador;
            this.codigo = codigo;

            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            InitializeComponent();
        }

        public void VerificarCreacionJugador(bool creado)
        {
            throw new NotImplementedException();
        }

        public void VerificarEnvioDeCorreo(bool enviado)
        {
            throw new NotImplementedException();
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void BotonRegistrarse(object sender, RoutedEventArgs e)
        {

            if(codigo == TextoCodigo.Text)
            {
                InstanceContext contexto = new InstanceContext(this);
                ProxyRegistro.RegistroServiceClient servidor = new ProxyRegistro.RegistroServiceClient(contexto);

                servidor?.CrearJugador(jugador);
                MessageBox.Show("Registro completado con exito");
            }
            else
            {
                MessageBox.Show("El codigo ingresado no coincide con el que te fue proporcionado");
            }

            Login ventanaLogin = new Login();
            Window.GetWindow(this).Close();
            ventanaLogin.Show();

        }

    }
}
