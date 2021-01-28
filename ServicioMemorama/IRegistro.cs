using Modelo.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ServicioMemorama
{
    /// <summary>
    /// Interface que contiene los metodos de registro del servicio
    /// </summary>
    [ServiceContract(CallbackContract = typeof(IRegistroServiceCallback))]
    public interface IRegistroService
    {
        /// <summary>
        /// Metodo para crear un jugador
        /// </summary>
        /// <param name="jugador">Jugador a crear</param>
        /// <returns></returns>
        [OperationContract]
        bool CrearJugador(Jugador jugador);

        /// <summary>
        /// Metodo para enviar el correo del registro
        /// </summary>
        /// <param name="correo">Correo a enviar</param>
        /// <param name="codigoDeRegistro">Codigo de registro</param>
        /// <returns></returns>
        [OperationContract]
        bool EnviarCorreoRegistro(string correo, string codigoDeRegistro);
    }

    /// <summary>
    /// Interface del callback del servicio
    /// </summary>
    public interface IRegistroServiceCallback
    {
        /// <summary>
        /// Metodo para verificar la creacion del jugador
        /// </summary>
        /// <param name="creado">Verdadero en caso de exito</param>
        [OperationContract(IsOneWay = true)]
        void VerificarCreacionJugador(bool creado);

    }
}
