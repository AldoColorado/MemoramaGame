using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.Modelo
{
    public class PartidaDAO : AbstractCRUD<Partida>
    {
        /// <summary>
        /// Heredado de AbstractCRUD
        /// </summary>
        public override bool Crear(Partida entity)
        {
            bool creado = false;

            try
            {
                db.Partida.Add(entity);
                db.SaveChanges();
                creado = true;
            }
            catch(System.InvalidOperationException ex)
            {
                Console.WriteLine(ex.GetBaseException());
            }
            catch (NullReferenceException ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return creado;

        }

        /// <summary>
        /// Permite buscar una partida creada
        /// </summary>
        /// <param name="codigo">Código de la partida que se está buscando</param>
        /// <returns>Regresa True si se encontró la partida sino regresa False</returns>
        public bool BuscarPartida(string codigo)
        {
            bool partidaEncontrada = false;

            try
            {
                Partida buscar = db.Partida.Where(q => q.codigo.Equals(codigo)).FirstOrDefault();
                if(buscar != null)
                {
                    partidaEncontrada = true;
                }
                else
                {
                    partidaEncontrada = false;
                }


            }
            catch(System.InvalidOperationException ex)
            {
                Console.WriteLine(ex.GetBaseException());
                partidaEncontrada = false;
            }
            catch (NullReferenceException ex)
            {
                Console.WriteLine(ex.ToString());
            }


            return partidaEncontrada;

        }

        /// <summary>
        /// Heredado de AbstractCRUD
        /// </summary>
        public override bool Eliminar(string llavePrimaria)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Heredado de AbstractCRUD
        /// </summary>
        public override void Modificar(Partida entity)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Heredado de AbstractCRUD
        /// </summary>
        public override List<Partida> Obtener()
        {
            return db.Partida.ToList<Partida>();
        }

        /// <summary>
        /// Heredado de AbstractCRUD
        /// </summary>
        public override Partida ObtenerEntidad(string llavePrimaria)
        {
            return db.Partida.Where(q => q.codigo.Equals(llavePrimaria)).First<Partida>();
        }
    }
}
