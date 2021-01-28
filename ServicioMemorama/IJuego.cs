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
    /// <summary>
    /// Esta interface sirve para administrar la funcionalidad del juego
    /// </summary>
    [ServiceContract(CallbackContract = typeof(IJuegoServiceCallback))]
    public interface IJuegoService
    {
        /// <summary>
        /// Metodo para conectarse al juego
        /// </summary>
        /// <param name="jugador">Jugador a conectar</param>
        [OperationContract(IsOneWay = true)]
        void ConectarseJuego(Jugador jugador);
         
        /// <summary>
        /// Metodo para actualizar el movimiento de las cartas
        /// </summary>
        /// <param name="x">Posicion vertical de la carta</param>
        /// <param name="y">Posicion horizontal de la carta</param>
        [OperationContract(IsOneWay = true)]
        void MovimientoDeJugador(double x, double y);

        /// <summary>
        /// Metodo para inicalizar los puntajes del juego
        /// </summary>
        /// <param name="jugador">Jugador a inicilizar el puntaje</param>
        /// <param name="puntaje">Puntaje del jugador</param>
        [OperationContract(IsOneWay = true)]
        void InicializarPuntajes(Jugador jugador, int puntaje);
        
        /// <summary>
        /// Metodo para modificar los puntajes de cierto jugador
        /// </summary>
        /// <param name="jugador">Jugador a modificar puntaje</param>
        /// <param name="puntaje">Puntaje aumentado</param>
        [OperationContract(IsOneWay = true)]
        void ModificarPuntajes(Jugador jugador, int puntaje);

        /// <summary>
        /// Metodo para reportar determinado jugador
        /// </summary>
        /// <param name="jugador">Jugador a reportar</param>
        [OperationContract (IsOneWay = true)]
        void ReportarJugador(string jugador, Jugador jugadorQueReporta);

        [OperationContract]
        void DeconectarseDeJuego(Jugador jugador);

    }

    /// <summary>
    /// Interface de callback, para actualizar los datos con el cliente
    /// </summary>
    [ServiceContract]
    public interface IJuegoServiceCallback
    {
        /// <summary>
        /// Metodo para mostrar el movimiento a los jugadores
        /// </summary>
        /// <param name="x">Posicion vertical de la carta</param>
        /// <param name="y">Posicion horizontal de la carta</param>
        [OperationContract(IsOneWay = true)]
        void MostrarMovimiento(double x, double y);

        /// <summary>
        /// Metodo para actualizar los jugadores en juego
        /// </summary>
        /// <param name="jugadores">jugadores conectados</param>
        [OperationContract(IsOneWay = true)]
        void JugadoresEnJuego(ObservableCollection<Jugador> jugadores);

        /// <summary>
        /// Meotdo para actualizar los puntajes de los jugadores
        /// </summary>
        /// <param name="puntajes">Coleccion de puntajes</param>
        [OperationContract(IsOneWay = true)]
        void ActualizarPuntajes(ObservableCollection<int> puntajes);
        
        /// <summary>
        /// Metodo para actualizar el reporte a un jugador
        /// </summary>
        [OperationContract(IsOneWay = true)]
        void ActualizarReporteJugador(Jugador jugadorQueReporta);

    }
}
