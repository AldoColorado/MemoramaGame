using Microsoft.VisualStudio.TestTools.UnitTesting;
using Modelo.Modelo;
using Moq;
using System;

namespace PruebasUnitarias.AccesoDeDatos
{
    [TestClass]
    public class PruebasPartidaDAO
    {
        [TestMethod]
        public void PruebaCrearPartida()
        {
            Partida partida = new Partida();
            PartidaDAO partidaDAO = new PartidaDAO();

            partida.idPartida = 0;
            partida.codigo = "12345";
            bool resultado = partidaDAO.Crear(partida);
            Assert.IsTrue(resultado);
        }
    }
}
