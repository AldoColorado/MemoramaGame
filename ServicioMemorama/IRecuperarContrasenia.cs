using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace ServicioMemorama
{
    [ServiceContract]
    interface IRecuperarContraseniaService
    {
        [OperationContract]
        bool ValidarJugadorPorCorreo(string correo);
        [OperationContract]
        bool EnviarCorreoRecuperacion(string correo, string codigoRecuperacion);
        [OperationContract]
        bool ActualizarContrasenia(string contrasenia, string correo);
    }
}
