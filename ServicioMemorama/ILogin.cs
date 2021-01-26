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
    [ServiceContract(CallbackContract = typeof(ILoginServiceCallback))]
    public interface ILoginService
    {
        [OperationContract]
        bool Login(string nombre, string contrasenia);

        [OperationContract(IsOneWay = true)]
        void Conectarse(Jugador jugador);

        [OperationContract]
        bool BuscarClientePorNombre(string nickName);

        [OperationContract]
        Dictionary<Jugador, ILoginServiceCallback> ObtenerClientes();

        [OperationContract(IsOneWay = true)]
        void Desconectarse(Jugador jugador);
    }

    [ServiceContract]
    public interface ILoginServiceCallback
    {
        [OperationContract]
        void VerificarUsuarioLogeado(bool logeado);

        [OperationContract(IsOneWay = true)]
        void UsuariosConectados(ObservableCollection<Jugador> jugadores);
    }
}

