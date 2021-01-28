using Modelo.Modelo;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ServicioMemorama
{
    /// <summary>
    /// Interface que contiene los metodos del control de una partida
    /// </summary>
    [ServiceContract(CallbackContract = typeof(IPartidaServiceCallback))]
    public interface IPartidaService
    {
        /// <summary>
        /// Metodo para crear una partida
        /// </summary>
        /// <param name="partida">Partida a crear</param>
        /// <param name="jugador">Jugador a relacionar con partida</param>
        /// <returns></returns>
        [OperationContract]
        bool CrearPartida(Partida partida, Jugador jugador);

        /// <summary>
        /// Metodo para generar el codigo de la partida
        /// </summary>
        /// <returns>El codigo generado</returns>
        [OperationContract]
        string GenerarCodigo();

        /// <summary>
        /// Metodo para crar la estadistica de la partida
        /// </summary>
        /// <param name="partida">Partida de la estadistica</param>
        /// <param name="jugador">Jugador de la estadistica</param>
        /// <returns></returns>
        [OperationContract]
        bool CrearEstadisticaPartida(Partida partida, Jugador jugador);

        /// <summary>
        /// Metodo para Agregar jugador a la partida
        /// </summary>
        /// <param name="jugador">jugador a conectar</param>
        [OperationContract(IsOneWay = true)]
        void AgregarJugador(Jugador jugador);

        /// <summary>
        /// Metodo para buscar un jugador
        /// </summary>
        /// <param name="nickName">Identificador del jugador</param>
        /// <returns></returns>
        [OperationContract]
        bool BuscarJugadorPorNombre(string nickName);

        /// <summary>
        /// Metodo para comprobar el codigo de la partida
        /// </summary>
        /// <param name="codigo">Codigo a comprobar</param>
        /// <returns>Verdader en caso de exito</returns>
        [OperationContract]
        bool ComprobarCodigoPartida(string codigo);

        /// <summary>
        /// Metodo para desconectarse de una partida
        /// </summary>
        /// <param name="jugador">Jugador a desconectar de la partida</param>
        [OperationContract(IsOneWay = true)]
        void DesconectarseDePartida(Jugador jugador);

        /// <summary>
        /// Metodo para generar el orden de las cartas a usar en la partida
        /// </summary>
        [OperationContract(IsOneWay = true)]
        void GenerarOrdenCartas();
    }

    /// <summary>
    /// Interface que contiene los metodos de callback del servicio
    /// </summary>
    [ServiceContract]
    public interface IPartidaServiceCallback
    {
        /// <summary>
        /// Metodo para actualizar los jugadores en la partida
        /// </summary>
        /// <param name="jugadores">jugadores a actualizar</param>
        [OperationContract(IsOneWay = true)]
        void JugadoresEnPartida(ObservableCollection<Jugador> jugadores);

        /// <summary>
        /// Metodo para obtener el orden de las cartas
        /// </summary>
        /// <param name="numeros">Arreglo de orden de las cartas</param>
        [OperationContract(IsOneWay = true)]
        void OrdenCartas(HashSet<int> numeros);
    }

}