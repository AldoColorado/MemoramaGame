using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Lógica de interacción para RecuperarContraseniaCodigo.xaml
    /// </summary>
    public partial class RecuperarContraseniaCodigo : Window
    {
        string correo;
        string codigo;
        public RecuperarContraseniaCodigo(string codigo, string correo)
        {
            this.codigo = codigo;
            this.correo = correo;
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            InitializeComponent();
        }

        private void BotonContinuar(object sender, RoutedEventArgs e)
        {
            string codigoIngresado = TextoCodigo.Text;

            if(codigoIngresado == codigo)
            {
                CambiarContrasenia ventanaCambiarContrasenia = new CambiarContrasenia(correo);
                ventanaCambiarContrasenia.Show();
                Window.GetWindow(this).Close();
            }
            else
            {
                MessageBox.Show("El código de comprobación no es el correcto");
            }
        }
    }
}
