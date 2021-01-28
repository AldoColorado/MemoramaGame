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
    /// Interface que contiene los metodo de login otorgados por el servicio
    /// </summary>
    [ServiceContract(CallbackContract = typeof(ILoginServiceCallback))]
    public interface ILoginService
    {
        /// <summary>
        /// Metodo para logearse dentro del sistema
        /// </summary>
        /// <param name="nombre">nombre del jugador</param>
        /// <param name="contrasenia">Contrasenia del jugador</param>
        /// <returns></returns>
        [OperationContract]
        bool Login(string nombre, string contrasenia);

        /// <summary>
        /// Metodo para conectarse al juego
        /// </summary>
        /// <param name="jugador">Jugador a conectarse</param>
        [OperationContract(IsOneWay = true)]
        void Conectarse(Jugador jugador);

        /// <summary>
        /// Meotdo para buscar determinado cliente conectado
        /// </summary>
        /// <param name="nickName">Nickname del cliente</param>
        /// <returns></returns>
        [OperationContract]
        bool BuscarClientePorNombre(string nickName);

        /// <summary>
        /// Metodo para obtener los clientes conectados
        /// </summary>
        /// <returns>Diccionario de clientes</returns>
        [OperationContract]
        Dictionary<Jugador, ILoginServiceCallback> ObtenerClientes();

        /// <summary>
        /// Metodo para desconctarse de una partida
        /// </summary>
        /// <param name="jugador">Jugador a desconcetarse</param>
        [OperationContract(IsOneWay = true)]
        void Desconectarse(Jugador jugador);
    }

    /// <summary>
    /// Interface que contiene los metodos de callback del servicio
    /// </summary>
    [ServiceContract]
    public interface ILoginServiceCallback
    {
        /// <summary>
        /// Metodo para verificar que el usaurio se haya logeado
        /// </summary>
        /// <param name="logeado">Verdadero en caso de aceptado</param>
        [OperationContract]
        void VerificarUsuarioLogeado(bool logeado);

        /// <summary>
        /// Metodo para actualizar los usuario conectados
        /// </summary>
        /// <param name="jugadores">Lista de jugadores conectados</param>
        [OperationContract(IsOneWay = true)]
        void UsuariosConectados(ObservableCollection<Jugador> jugadores);
    }
}

