
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
        /// <summary>
        /// Constructor
        /// </summary>
        protected MemoramaEntities db = new MemoramaEntities();
        /// <summary>
        /// Crea un registro en la base de datos, según la entidad mandada.
        /// </summary>
        /// <param name="entity">Nombre de la instancia del objeto objeto</param>
        /// <returns>Regresa True si se pudo guardar el registro sino regresa False</returns>
        public abstract bool Crear(T entity);
        /// <summary>
        /// Permite modificar algún atributo de la base de datos del objeto mandado 
        /// </summary>
        /// <param name="entity">Nombre de la instancia del objeto mandado</param>
        public abstract void Modificar(T entity);
        /// <summary>
        /// Permite eliminar de la base de datos, la entidad que está relacionada con el atributo mandado como parámetro.
        /// </summary>
        /// <param name="pk">Nombre del parámetro mandado</param>
        /// <returns>Regresa True si se pudo elimar el registro sino regresa False</returns>
        public abstract bool Eliminar(string pk);
        /// <summary>
        /// Permite obtener todos los registros del tipo de obejeto mandado.
        /// </summary>
        /// <returns>Regresa una lista con todos los registros de la base de datos</returns>
        public abstract List<T> Obtener();
        /// <summary>
        /// Método que permite obtener un registro de la base de datos
        /// </summary>
        /// <param name="pk">Nombre del parámetro con el cual se utilizará para buscar el registro de la base de datos</param>
        /// <returns>Regresa el registro obtenido</returns>
        public abstract T ObtenerEntidad(string pk);

    }
}
