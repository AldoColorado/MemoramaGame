using Modelo.Modelo;
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

namespace Memorama.Vista
{
    /// <summary>
    /// Lógica de interacción para UnirseAPartida.xaml
    /// </summary>
    public partial class UnirseAPartida
    {
        private string codigoPartida = "";
        private Jugador jugador;

        public UnirseAPartida(Jugador jugador)
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            InitializeComponent();
            this.jugador = jugador;

            IngresarCodigo();
        }

        private void BotonUnirse(object sender, RoutedEventArgs e)
        {
            codigoPartida = TextoCodigo.Text;

            PrePartida ventanaPrePartida = new PrePartida(jugador, codigoPartida);
            ventanaPrePartida.Show();
            Window.GetWindow(this).Close();
        }

        public void IngresarCodigo()
        {
            codigoPartida = TextoCodigo.Text;
        }
    }
}
