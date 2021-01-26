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
        public Image imagen { get; private set; }    
        public int numero { get; private set; }

        public Carta(Image imagen, int numero)
        {
            this.imagen = imagen;
            this.numero = numero;
        }
    }
}

