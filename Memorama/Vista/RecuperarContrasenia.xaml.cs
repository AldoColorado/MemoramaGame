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

namespace Memorama
{
    /// <summary>
    /// Lógica de interacción para RecuperarContrasenia.xaml
    /// </summary>
    public partial class RecuperarContrasenia : Window
    {

        public string codigo;
        public bool correoEnviado;
        public bool jugadorEncontrado;

        public RecuperarContrasenia()
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            InitializeComponent();
        }

        private void BotonContinuar(object sender, RoutedEventArgs e)
        {
            ProxyRecuperarContrasenia.RecuperarContraseniaServiceClient servidor = new ProxyRecuperarContrasenia.RecuperarContraseniaServiceClient();
            jugadorEncontrado = (bool)(servidor?.ValidarJugadorPorCorreo(TextoCorreo.Text));

            if (jugadorEncontrado)
            {
                GenerarCodigoRecuperacion();
                servidor?.EnviarCorreoRecuperacion(TextoCorreo.Text, codigo);

                RecuperarContraseniaCodigo ventanaRecuperarContraseniaCodigo = new RecuperarContraseniaCodigo(codigo, TextoCorreo.Text);
                ventanaRecuperarContraseniaCodigo.Show();
                Window.GetWindow(this).Close();
            }
            else
            {
                MessageBox.Show("No se encontró ningún jugador con ese correo");
            }
        }

        public void GenerarCodigoRecuperacion()
        {
            var seed = Environment.TickCount;
            var random = new Random(seed);

            for (int i = 0; i <= 4; i++)
            {
                var value = random.Next(0, 9);
                codigo += value;
            }
        }
    }
}
