using Microsoft.VisualStudio.TestTools.UnitTesting;
using Modelo.Modelo;
using Moq;
using System;
using System.Collections.Generic;

namespace PruebasUnitarias.AccesoDeDatos
{
    /// <summary>
    /// Clase que prueba PartidaDAO
    /// </summary>
    [TestClass]
    public class PruebasPartidaDAO
    {
        Partida partida;
        PartidaDAO partidaDAO;

        /// <summary>
        /// Método que inicializa los datos que se utilizarán en las pruebas
        /// </summary>
        public void InicializarDatos()
        {
            partida = new Partida();
            partidaDAO = new PartidaDAO();

            partida.idPartida = 0;
            partida.codigo = "12345";

        }
        
        /// <summary>
        /// Método que prueba si una partida puede ser creada
        /// </summary>
        [TestMethod]
        public void PruebaCrearPartida()
        {
            InicializarDatos();
            
            bool resultado = partidaDAO.Crear(partida);
            Assert.IsTrue(resultado);
        }

        /// <summary>
        /// Método que prueba si se puede buscar partida en la base de datos
        /// </summary>
        [TestMethod]
        public void PruebaBuscarPartida()
        {
            InicializarDatos();

            bool resultado = partidaDAO.BuscarPartida(partida.codigo);
            Assert.IsTrue(resultado);
        }

        /// <summary>
        /// Método que prueba si se pueden obtener todas las partidas de la base de datos
        /// </summary>
        [TestMethod]
        public void PruebaObtenerPartidas()
        {
            InicializarDatos();

            List<Partida> partida = new List<Partida>();

            partida = partidaDAO.Obtener();
            Assert.IsNotNull(partida);
        }

        /// <summary>
        /// Método que prueba si se puede obtener una partida de la base de datos
        /// </summary>
        [TestMethod]
        public void PruebaObtenerPartida()
        {
            InicializarDatos();

            Partida partidaObtener = partidaDAO.ObtenerEntidad(partida.codigo);
        }
    }
}
