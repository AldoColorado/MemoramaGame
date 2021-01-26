using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.Modelo
{
    public class EstadisticaPartidaDAO : AbstractCRUD<EstadisticaPartida>
    {
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

        public override bool Eliminar(string pk)
        {
            throw new NotImplementedException();
        }

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

        public override List<EstadisticaPartida> Obtener()
        {
            return db.EstadisticaPartida.ToList<EstadisticaPartida>();
        }

        public override EstadisticaPartida ObtenerEntidad(string pk)
        {
            throw new NotImplementedException();
        }
    }
}
