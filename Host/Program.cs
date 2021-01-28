﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Host
{
    /// <summary>
    /// Clase que activa y desactiva el servidor
    /// </summary>
    class host
    {
        static void Main(string[] args)
        {
            ServiceHost host = new ServiceHost(typeof(ServicioMemorama.ServicioMemorama));
            host.Open();
            Console.WriteLine("El servicio esta listo");
            Console.ReadLine();
            host.Close();
        }
    }
}
