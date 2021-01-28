using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace ServicioMemorama
{
    /// <summary>
    /// El servicio IRecuperarContraseniaService es usado para recuperar la contraseña de un usuario que la ha olvidado
    /// </summary>
    [ServiceContract]
    interface IRecuperarContraseniaService
    {
        /// <summary>
        /// Valida que el correo mandado este relacionado con un jugador en la base de datos.
        /// </summary>
        /// <param name="correo">Correo asociado al usuario que quiere recuperar su contraseña</param>
        /// <returns>Regresa True si se encontró algún usuario con el correo mandado sino regresa False</returns>
        [OperationContract]
        bool ValidarJugadorPorCorreo(string correo);
        /// <summary>
        /// Envia un código de recuperación al correo del usuario que está tratado recuperar contraseña
        /// </summary>
        /// <param name="correo">Correo asociado al usuario que quiere recuperar su contraseña</param>
        /// <param name="codigoRecuperacion">Código generado y mandado al correo del usuario</param>
        /// <returns>Regresa True si el correo se pudo enviar sin errores sino regresa False</returns>
        [OperationContract]
        bool EnviarCorreoRecuperacion(string correo, string codigoRecuperacion);
        /// <summary>
        /// Actuliza en la base de datos la contraseña del usuario que quiere recuperar su contraseña.
        /// </summary>
        /// <param name="contrasenia">Nueva contraseña del usuario</param>
        /// <param name="correo">Correo asociado al usuario que quiere recuperar su contraseña</param>
        /// <returns>Regresa True si se puedo cambiar la contraseña sin errores sino regresa False</returns>
        [OperationContract]
        bool ActualizarContrasenia(string contrasenia, string correo);
    }
}
