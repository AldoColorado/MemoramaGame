using Modelo.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ServicioMemorama
{
    [ServiceContract(CallbackContract = typeof(IRegistroServiceCallback))]
    public interface IRegistroService
    {
        [OperationContract(IsOneWay = true)]
        void CrearJugador(Jugador jugador);

        [OperationContract(IsOneWay = true)]
        void EnviarCorreoRegistro(string correo, string codigoDeRegistro);
    }

    public interface IRegistroServiceCallback
    {
        [OperationContract(IsOneWay = true)]
        void VerificarCreacionJugador(bool creado);

        [OperationContract(IsOneWay = true)]
        void VerificarEnvioDeCorreo(bool enviado);
    }
}
