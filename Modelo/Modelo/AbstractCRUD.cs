
using Modelo.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public abstract class AbstractCRUD<T>
    {
        protected MemoramaEntities db = new MemoramaEntities();
        public abstract bool Crear(T entity);
        public abstract void Modificar(T entity);
        public abstract bool Eliminar(string pk);
        public abstract List<T> Obtener();
        public abstract T ObtenerEntidad(string pk);

    }
}
