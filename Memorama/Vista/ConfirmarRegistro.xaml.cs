
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
   
        private Jugador jugador;
        private string codigo;

        /// <summary>
        /// Constructor de la clase
        /// </summary>
        /// <param name="jugador">Jugador a registrarse</param>
        /// <param name="codigo">Codigo de registro</param>
        public ConfirmarRegistro(Jugador jugador, string codigo)
        {
            this.jugador = jugador;
            this.codigo = codigo;

            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            InitializeComponent();
        }

        /// <summary>
        /// Metodo no implementado
        /// </summary>
        /// <param name="creado">Verdader en caso de jugador creado</param>
        public void VerificarCreacionJugador(bool creado)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Metodo no implementado
        /// </summary>
        /// <param name="enviado">Verdadero si se envia el correo</param>
        public void VerificarEnvioDeCorreo(bool enviado)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Propiedad de la ventana
        /// </summary>
        /// <param name="sender">Propiedad del evento</param>
        /// <param name="e">Propiedad del evento</param>
        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        /// <summary>
        /// Evento del boton registrar
        /// </summary>
        /// <param name="sender">Propiedad del evento</param>
        /// <param name="e">Propiedad del evento</param>
        private void BotonRegistrarse(object sender, RoutedEventArgs e)
        {
            if(codigo == TextoCodigo.Text)
            {
                InstanceContext contexto = new InstanceContext(this);
                ProxyRegistro.RegistroServiceClient servidor = new ProxyRegistro.RegistroServiceClient(contexto);
                try
                {
                    bool creado = servidor.CrearJugador(jugador);

                    if(creado)
                    {
                        MessageBox.Show("Registro completado con exito");

                        Login ventanaLogin = new Login();
                        Window.GetWindow(this).Close();
                        ventanaLogin.Show();
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show("El servidor no se encuentra disponible");
                    Window.GetWindow(this).Close();
                }
            }
            else
            {
                MessageBox.Show("El codigo ingresado no coincide con el que te fue proporcionado");
            }
        }

        /// <summary>
        /// Evento del boton regresar
        /// </summary>
        /// <param name="sender">Propiedad del evento</param>
        /// <param name="e">Propiedad del evento</param>
        private void BotonRegresar(object sender, RoutedEventArgs e)
        {
            Login login = new Login();
            Window.GetWindow(this).Close();
            login.Show();   
        }
    }
}
