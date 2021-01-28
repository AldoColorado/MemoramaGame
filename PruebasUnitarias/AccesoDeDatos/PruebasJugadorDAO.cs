using Microsoft.VisualStudio.TestTools.UnitTesting;
using Modelo;
using Modelo.Modelo;
using Moq;
using PruebasUnitarias.AccesoDeDatos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PruebasUnitarias
{
    /// <summary>
    /// Clase que prueba JugadorDAO
    /// </summary>
    [TestClass]
    public class PruebaJugadorBaseDeDatos
    {
        Jugador jugador;
        JugadorDAO jugadorDAO;

        /// <summary>
        /// Método que inicializa los datos que se utilizarán en estas pruebas
        /// </summary>
        public void InicializarDatos()
        {
            jugador = new Jugador();
            jugadorDAO = new JugadorDAO();

            string contrasenia = "12344";
            string contraseniaEncriptada = string.Empty;
            byte[] encryted = System.Text.Encoding.Unicode.GetBytes(contrasenia);
            contraseniaEncriptada = Convert.ToBase64String(encryted);


            jugador.nickName = "AldoDiaz";
            jugador.nombre = "AldoColorado";
            jugador.correoElectronico = "aldocoloradocd@gmail.com";
            jugador.contrasenia = contraseniaEncriptada;
        }

        /// <summary>
        /// Método que prueba si un jugador puede ser creado
        /// </summary>
        [TestMethod]
        public void PruebaCrearJugador()
        {
            InicializarDatos();
            
            bool resultado = jugadorDAO.Crear(jugador);
            Assert.IsTrue(resultado);
        }

        /// <summary>
        /// Método que prueba si se validan los datos del login (Usuario y Contraseña)
        /// </summary>
        [TestMethod]
        public void PruebaValidarUsuario()
        {
            InicializarDatos();

            bool resultado = jugadorDAO.ValidarUsuario(jugador.nickName, jugador.contrasenia);
            Assert.IsTrue(resultado);
        }

        /// <summary>
        /// Método que prueba si un jugador puede ser eliminado
        /// </summary>
        [TestMethod]
        public void PruebaEliminarJugador()
        {
            InicializarDatos();

            bool resultado = jugadorDAO.Eliminar(jugador.nickName);
            Assert.IsTrue(resultado);
        }

        /// <summary>
        /// Método que prueba si se puede obtener los jugadores guardados en la base de datos
        /// </summary>
        [TestMethod]
        public void PruebaObtenerJugadores()
        {
            InicializarDatos();
            List<Jugador> jugadores = new List<Jugador>();

            jugadores = jugadorDAO.Obtener();
            Assert.IsNotNull(jugadores);
        }

        /// <summary>
        /// Método que prueba si se puede recuperar un jugador de la base de datos
        /// </summary>
        [TestMethod]
        public void PruebaObtenerEntidad()
        {
            InicializarDatos();

            Jugador jugadorObtener = jugadorDAO.ObtenerEntidad(jugador.nickName);
            Assert.IsNotNull(jugadorObtener);
        }
    }
}
