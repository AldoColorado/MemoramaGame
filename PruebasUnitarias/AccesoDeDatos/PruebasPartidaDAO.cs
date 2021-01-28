using Microsoft.VisualStudio.TestTools.UnitTesting;
using Modelo.Modelo;
using Moq;
using System;
using System.Collections.Generic;

namespace PruebasUnitarias.AccesoDeDatos
{
    [TestClass]
    public class PruebasPartidaDAO
    {
        Partida partida;
        PartidaDAO partidaDAO;

        public void InicializarDatos()
        {
            partida = new Partida();
            partidaDAO = new PartidaDAO();

            partida.idPartida = 0;
            partida.codigo = "12345";

        }
        [TestMethod]
        public void PruebaCrearPartida()
        {
            InicializarDatos();
            
            bool resultado = partidaDAO.Crear(partida);
            Assert.IsTrue(resultado);
        }

        [TestMethod]
        public void PruebaBuscarPartida()
        {
            InicializarDatos();

            bool resultado = partidaDAO.BuscarPartida(partida.codigo);
            Assert.IsTrue(resultado);
        }

        [TestMethod]
        public void PruebaObtenerPartidas()
        {
            InicializarDatos();

            List<Partida> partida = new List<Partida>();

            partida = partidaDAO.Obtener();
            Assert.IsNotNull(partida);
        }

        [TestMethod]
        public void PruebaObtenerPartida()
        {
            InicializarDatos();

            Partida partidaObtener = partidaDAO.ObtenerEntidad(partida.codigo);
        }
    }
}
