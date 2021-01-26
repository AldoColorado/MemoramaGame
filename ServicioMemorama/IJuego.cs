using Modelo.Modelo;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ServicioMemorama
{
    [ServiceContract(CallbackContract = typeof(IJuegoServiceCallback))]
    public interface IJuegoService
    {
            
        [OperationContract(IsOneWay = true)]
        void ConectarseJuego(Jugador jugador);
        
        [OperationContract(IsOneWay = true)]
        void MovimientoDeJugador(double x, double y);

        [OperationContract(IsOneWay = true)]
        void InicializarPuntajes(Jugador jugador, int puntaje);

        [OperationContract(IsOneWay = true)]
        void ModificarPuntajes(Jugador jugador, int puntaje);

    }

    [ServiceContract]
    public interface IJuegoServiceCallback
    {
        [OperationContract(IsOneWay = true)]
        void MostrarMovimiento(double x, double y);

        [OperationContract(IsOneWay = true)]
        void JugadoresEnJuego(ObservableCollection<Jugador> jugadores);

        [OperationContract(IsOneWay = true)]
        void ActualizarPuntajes(ObservableCollection<int> puntajes);

    }
}
