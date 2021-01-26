using Modelo.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ServicioMemorama
{
    [ServiceContract(CallbackContract = typeof(IEstadisticasServiceCallback))]
    public interface IEstadisticasService
    {
        [OperationContract]
        bool GuardarEstadisticasPartida(EstadisticaPartida estadisticaPartida, Jugador jugador, Partida partida);

        [OperationContract]
        bool GenerarTablaDePuntajes();

        [OperationContract(IsOneWay = false)]
        List<Jugador> ObtenerListaDeJugadores();

        [OperationContract(IsOneWay = false)]
        List<int> ObtenerPuntajesJugadores();

    }

    [ServiceContract]
    public interface IEstadisticasServiceCallback
    {
        [OperationContract]
        void MostrarEstadistica();
    }
}