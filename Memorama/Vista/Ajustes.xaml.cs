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
    /// Lógica de interacción para Ajustes.xaml
    /// </summary>
    public partial class Ajustes : Window
    {
        public Ajustes()
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            InitializeComponent();
        }

        private void BotonAplicar(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(Application.ResourceAssembly.Location); 
            Application.Current.Shutdown();
        }

        private void ComboBoxLenguajeSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ComboBoxLenguaje.SelectedIndex == 0)
            {
                Properties.Settings.Default.lenguaje = "es-MX";
            }
            else
            {
                Properties.Settings.Default.lenguaje = "en-US";
            }
            Properties.Settings.Default.Save();
        }
    }
}
