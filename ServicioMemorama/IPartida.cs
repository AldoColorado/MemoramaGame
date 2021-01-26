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
    [ServiceContract(CallbackContract = typeof(IPartidaServiceCallback))]

    public interface IPartidaService
    {
        [OperationContract]
        bool CrearPartida(Partida partida, Jugador jugador);

        [OperationContract]
        string GenerarCodigo();

        [OperationContract(IsOneWay = true)]
        void AgregarJugador(Jugador jugador);

        [OperationContract]
        bool BuscarJugadorPorNombre(string nickName);

        [OperationContract]
        bool ComprobarCodigoPartida(string codigo);

        [OperationContract(IsOneWay = true)]
        void DesconectarseDePartida(Jugador jugador);

        [OperationContract(IsOneWay = true)]
        void GenerarOrdenCartas();
    }

    [ServiceContract]
    public interface IPartidaServiceCallback
    {
        [OperationContract(IsOneWay = true)]
        void JugadoresEnPartida(ObservableCollection<Jugador> jugadores);

        [OperationContract(IsOneWay = true)]
        void OrdenCartas(HashSet<int> numeros);
    }

}