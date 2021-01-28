using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.Modelo
{
    /// <summary>
    /// Heredado de AbstractCRUD
    /// </summary>
    public class EstadisticaPartidaDAO : AbstractCRUD<EstadisticaPartida>
    {
        /// <summary>
        /// Metodo para crear estadistica
        /// </summary>
        /// <param name="entity">Entidad a crear</param>
        /// <returns>Verdader si se creo</returns>
        public override bool Crear(EstadisticaPartida entity)
        {
            bool creado = false;

            try
            {
                db.EstadisticaPartida.Add(entity);
                db.SaveChanges();
                creado = true;
            }
            catch(System.InvalidOperationException ex)
            {
                creado = false;
                Console.WriteLine(ex.ToString());
            }
            return creado;
        }

        /// <summary>
        /// Metodo para eliminar estadistica
        /// </summary>
        /// <param name="pk">Llave primaria de la estadistica</param>
        /// <returns>Verdadero en caso de exito</returns>
        public override bool Eliminar(string pk)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Metodo para modificar estadistica
        /// </summary>
        /// <param name="entity">Entidad a modificar</param>
        public override void Modificar(EstadisticaPartida entity)
        {
            EstadisticaPartida buscar = db.EstadisticaPartida.Where(q => q.idPartida.Equals(entity.idPartida) && q.idJugador.Equals(entity.idJugador)).FirstOrDefault();

            if(buscar != null)
            {
                buscar.paresObtenidos = entity.paresObtenidos;
                buscar.puntaje = entity.puntaje;
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Metodo para obtener las estadisticas de la partida
        /// </summary>
        /// <returns></returns>
        public override List<EstadisticaPartida> Obtener()
        {
            return db.EstadisticaPartida.ToList<EstadisticaPartida>();
        }

        /// <summary>
        /// Metodo para obtener cierta entidad
        /// </summary>
        /// <param name="pk">Llave de la entidad</param>
        /// <returns>Regresa la entidad</returns>
        public override EstadisticaPartida ObtenerEntidad(string pk)
        {
            throw new NotImplementedException();
        }
    }
}
