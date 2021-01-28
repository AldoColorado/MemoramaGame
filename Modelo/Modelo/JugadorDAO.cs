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

        public override List<Jugador> Obtener()
        {
            return db.Jugador.ToList<Jugador>();
        }

        public override Jugador ObtenerEntidad(string pk)
        {
            return db.Jugador.Where(q => q.nickName.Equals(pk)).First<Jugador>();
        }

        public override void Modificar(Jugador entity)
        {

        }

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
