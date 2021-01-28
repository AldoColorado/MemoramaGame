using Modelo.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Modelo
{
    public class Carta
    {
        /// <summary>
        /// Imagen de la carta
        /// </summary>
        public Image imagen { get; private set; }    
        /// <summary>
        /// Número de la carta
        /// </summary>
        public int numero { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="imagen">Imagen asociada</param>
        /// <param name="numero">Número asociado</param>
        public Carta(Image imagen, int numero)
        {
            this.imagen = imagen;
            this.numero = numero;
        }
    }
}

