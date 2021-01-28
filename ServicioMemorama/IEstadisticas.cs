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
    /// La interface IEstadisticaService sirve para administrar las estadisticas de los jugadores con las partidas
    /// </summary>
    [ServiceContract]
    public interface IEstadisticasService
    {
        /// <summary>
        /// Metodo para guardar las estadisticas dentro de la base de datos
        /// </summary>
        /// <param name="estadisticaPartida">Objeto estadistica partida</param>
        /// <param name="jugador">Objeto jugador</param>
        /// <param name="partida">Objeto partida</param>
        /// <returns>Verdadero en caso de exito</returns>
        [OperationContract]
        bool GuardarEstadisticasPartida(EstadisticaPartida estadisticaPartida, Jugador jugador, Partida partida);

        /// <summary>
        /// Metodo para generar la tabla de puntajes
        /// </summary>
        /// <returns>Verdadero en caso de exito</returns>
        [OperationContract]
        bool GenerarTablaDePuntajes();

        /// <summary>
        /// Metodo para obtener los puntajes de los jugadores
        /// </summary>
        /// <returns>Coleccion de puntajes</returns>
        [OperationContract]
        ObservableCollection<Tabla> ObtenerPuntajesJugadores();

        /// <summary>
        /// Meodo para obtener la lisra de puntajes
        /// </summary>
        /// <returns>Lista de puntajes</returns>
        [OperationContract]
        List<int> ObtenerPuntaje();

        /// <summary>
        /// Metodo para obtener los jugadores
        /// </summary>
        /// <returns>Lista de jugadores</returns>
        [OperationContract]
        List<string> ObtenerJugadores();

    }
}