using System;
using Modelo.Modelo;
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
using System.Windows.Shapes;

namespace Memorama.Vista
{
    /// <summary>
    /// Lógica de interacción para Puntajes.xaml
    /// </summary>
    public partial class Puntajes : ProxyEstadisticas.IEstadisticasServiceCallback
    {
        ObservableCollection<Jugador> listaJugadores;
        ObservableCollection<int> listaPuntajes;
        Jugador[] jugadores;
        int[] puntajes;
        InstanceContext contexto;
        ProxyEstadisticas.EstadisticasServiceClient servidor;

        public Puntajes()
        {
            InitializeComponent();
            contexto = new InstanceContext(this);
            servidor = new ProxyEstadisticas.EstadisticasServiceClient(contexto);

            listaJugadores = new ObservableCollection<Jugador>();
            listaPuntajes = new ObservableCollection<int>();
            jugadores = new Jugador[100];
            puntajes = new int[100];

            try
            {
                servidor.GenerarTablaDePuntajes();

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            InicializarCollecionJugadores();
            InicializarCollecionPuntajes();

            int i = 0;
        }

        private void BotonRegresar(object sender, RoutedEventArgs e)
        {

        }

        public void InicializarCollecionJugadores()
        {
            try
            {
                servidor.ObtenerListaDeJugadores();

                foreach(var j in jugadores)
                {
                    listaJugadores.Add(j);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        public void InicializarCollecionPuntajes()
        {
            try
            {
                servidor.ObtenerPuntajesJugadores();

                foreach(var j in puntajes)
                {
                    listaPuntajes.Add(j);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        public void MostrarEstadistica()
        {
            throw new NotImplementedException();
        }


    }
}
