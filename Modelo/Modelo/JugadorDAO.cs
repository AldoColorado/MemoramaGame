using Modelo.Modelo;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class JugadorDAO : AbstractCRUD<Jugador>
    {
        /// <summary>
        /// Heredado de AbstractCRUD
        /// </summary>
        public override bool Crear(Jugador entity)
        {
            bool creado = false;

            try
            {
                creado = true;
                db.Jugador.Add(entity);
                db.SaveChanges();
            }
            catch(System.InvalidOperationException ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return creado;
        }

        /// <summary>
        /// Permite verificar si el usuario y la contraseña coinciden con algún registro de la base de datos
        /// </summary>
        /// <param name="nickName">Usuario del jugador que se está buscando</param>
        /// <param name="contrasenia">Contraseña del jugador que se está buscando</param>
        /// <returns>Regresa True si se encuentra algún registro con el nickname y la contrasenia sino regresa False</returns>
        public bool ValidarUsuario(string nickName, string contrasenia)
        {
            bool usuarioValido = false;

            try
            {
                Jugador buscar = db.Jugador.Where(q => q.nickName.Equals(nickName) && q.contrasenia.Equals(contrasenia)).FirstOrDefault();
                if(buscar != null)
                {
                    usuarioValido = true;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return usuarioValido;
        }

        /// <summary>
        /// Heredado de AbstractCRUD
        /// </summary>
        public override bool Eliminar(string pk)
        {
            bool eliminado = false;
            try
            {
                Jugador buscar = db.Jugador.Where(q => q.nickName.Equals(pk)).First<Jugador>();

                if(buscar == null)
                {
                    throw new ArgumentException("Usuario no encontrado en la base de datos");
                }
                else
                {
                    db.Jugador.Remove(buscar);
                    db.SaveChanges();
                    eliminado = true;
                }
            }
            catch(Exception ex)
            {

            }

            return eliminado;
        }

        /// <summary>
        /// Heredado de AbstractCRUD
        /// </summary>
        public override List<Jugador> Obtener()
        {
            return db.Jugador.ToList<Jugador>();
        }

        /// <summary>
        /// Heredado de AbstractCRUD
        /// </summary>
        public override Jugador ObtenerEntidad(string pk)
        {
            return db.Jugador.Where(q => q.nickName.Equals(pk)).First<Jugador>();
        }

        /// <summary>
        /// Heredado de AbstractCRUD
        /// </summary>
        public override void Modificar(Jugador entity)
        {

        }

        /// <summary>
        /// Busca algún usuario que tenga el correo proporcionado
        /// </summary>
        /// <param name="correo">Correo del usuario que se está buscando</param>
        /// <returns>Regresa True si se encontró algún usuario con ese correo sino regresa False</returns>
        public bool ValidarJugadorPorCorreo(string correo)
        {
            bool encontrado = false;
            try
            {
                Jugador buscar = db.Jugador.Where(q => q.correoElectronico.Equals(correo)).FirstOrDefault();
                if(buscar != null)
                {
                    encontrado = true;
                }
            }
            catch(System.InvalidOperationException ex)
            {
                Console.WriteLine(ex.GetBaseException());
            }
            return encontrado;
        }

        /// <summary>
        /// Recupera al jugador que tiene relacionado el correo
        /// </summary>
        /// <param name="correo">Correo del jugador que se quiere obtener</param>
        /// <returns>Regresa al Jugador que tiene asociado el correo mandado</returns>
        public Jugador ObtenerJugadorPorCorreo(string correo)
        {
            try
            {
                Jugador buscar = db.Jugador.Where(q => q.correoElectronico.Equals(correo)).FirstOrDefault();
                return buscar;
            }
            catch(System.InvalidOperationException ex)
            {
                Console.WriteLine(ex.GetBaseException());
            }
            return null;
        }

        /// <summary>
        /// Permite actualizar la contraseña de un jugador
        /// </summary>
        /// <param name="contrasenia">La nueva contraseña</param>
        /// <param name="nickname">Usuario del jugador al cuál se le cambiará la contraseña</param>
        /// <returns>Regresa True si se pudo actualizar la contraseña sino regresa False</returns>
        public bool ActualizarContrasenia(string contrasenia, string nickname)
        {
            Jugador buscar = db.Jugador.Where(q => q.nickName.Equals(nickname)).FirstOrDefault();
            if(buscar != null)
            {
                buscar.contrasenia = contrasenia;
                db.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
