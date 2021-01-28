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
    [ServiceContract]
    public interface IEstadisticasService
    {
        [OperationContract]
        bool GuardarEstadisticasPartida(EstadisticaPartida estadisticaPartida, Jugador jugador, Partida partida);

        [OperationContract]
        bool GenerarTablaDePuntajes();

        [OperationContract]
        ObservableCollection<Tabla> ObtenerPuntajesJugadores();

        [OperationContract]
        List<int> ObtenerPuntaje();

        [OperationContract]
        List<string> ObtenerJugadores();

    }
}