using Microsoft.VisualStudio.TestTools.UnitTesting;
using Modelo;
using Modelo.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebasUnitarias.AccesoDeDatos
{
    [TestClass]
    public class PruebasEstadisticaPartidaDAO
    {
        Jugador jugador;
        Partida partida;
        EstadisticaPartida estadisticaPartida;
        EstadisticaPartidaDAO estadisticaPartidaDAO;

        public void InicializarDatos()
        {
            jugador = new Jugador();
            partida = new Partida();
            estadisticaPartida = new EstadisticaPartida();
            estadisticaPartidaDAO = new EstadisticaPartidaDAO();

            estadisticaPartida.idPartida = 1;
            estadisticaPartida.idJugador = 1;
            estadisticaPartida.paresObtenidos = 5;
            estadisticaPartida.puntaje = 5;
        }

        [TestMethod]
        public void PruebaCrearEstadisticaPartida()
        {
            InicializarDatos();

            bool creada = estadisticaPartidaDAO.Crear(estadisticaPartida);
            Assert.IsTrue(creada);
        }

        [TestMethod]
        public void PruebaModificarEstadisticaPartida()
        {
            InicializarDatos();

            estadisticaPartidaDAO.Modificar(estadisticaPartida);
            
        }

        [TestMethod]
        public void PruebaObtenerEstadisticasPartidas()
        {
            InicializarDatos();

            List<EstadisticaPartida> lista = estadisticaPartidaDAO.Obtener();
            Assert.IsNotNull(lista);
        }

    }
}
