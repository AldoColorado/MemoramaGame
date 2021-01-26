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
using Modelo.Modelo;

namespace Memorama
{
    /// <summary>
    /// Lógica de interacción para CambiarContrasenia.xaml
    /// </summary>
    public partial class CambiarContrasenia : Window
    {
        string correo;
        public CambiarContrasenia(string correo)
        {
            this.correo = correo;
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            InitializeComponent();
        }

        public void BotonCambiarContrasenia(object sender, RoutedEventArgs e)
        {
            bool seActualizoContrasenia = false;
            if(TextoNuevaContrasenia.Password == TextoConfirmacionContrasenia.Password)
            {
                string contraseniaEncriptada = string.Empty;
                byte[] encryted = System.Text.Encoding.Unicode.GetBytes(TextoNuevaContrasenia.Password);
                contraseniaEncriptada = Convert.ToBase64String(encryted);

                ProxyRecuperarContrasenia.RecuperarContraseniaServiceClient servidor = new ProxyRecuperarContrasenia.RecuperarContraseniaServiceClient();
                seActualizoContrasenia = (bool)(servidor?.ActualizarContrasenia(contraseniaEncriptada, correo));

                MessageBox.Show("Se ha actualizado la contraseña con éxito");

                Login ventanaLogin = new Login();
                Window.GetWindow(this).Close();
                ventanaLogin.Show();
            }
            else
            {
                MessageBox.Show("Las contraseñas no coinciden");
                TextoNuevaContrasenia.Clear();
                TextoConfirmacionContrasenia.Clear();
            }
        }
    }
}
