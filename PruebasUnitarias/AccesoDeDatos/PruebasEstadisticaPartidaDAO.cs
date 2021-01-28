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
    /// <summary>
    /// Clase que prueba EstadisticaPartidaDAO
    /// </summary>
    [TestClass]
    public class PruebasEstadisticaPartidaDAO
    {
        Jugador jugador;
        Partida partida;
        EstadisticaPartida estadisticaPartida;
        EstadisticaPartidaDAO estadisticaPartidaDAO;

        /// <summary>
        /// Método que inicializa los datos que se utilizarán en las pruebas
        /// </summary>
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

        /// <summary>
        /// Método que prueba si las estadísticas de partida puede ser creada
        /// </summary>
        [TestMethod]
        public void PruebaCrearEstadisticaPartida()
        {
            InicializarDatos();

            bool creada = estadisticaPartidaDAO.Crear(estadisticaPartida);
            Assert.IsTrue(creada);
        }

        /// <summary>
        /// Método que prueba si las estadísticas de una partida puede ser modificada
        /// </summary>
        [TestMethod]
        public void PruebaModificarEstadisticaPartida()
        {
            InicializarDatos();

            estadisticaPartidaDAO.Modificar(estadisticaPartida);
            
        }

        /// <summary>
        /// Método que prueba si se pueden obtener las estadísticas de una partida
        /// </summary>
        [TestMethod]
        public void PruebaObtenerEstadisticasPartidas()
        {
            InicializarDatos();

            List<EstadisticaPartida> lista = estadisticaPartidaDAO.Obtener();
            Assert.IsNotNull(lista);
        }

    }
}
