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
    [TestClass]
    public class PruebaJugadorBaseDeDatos
    {
        Jugador jugador;
        JugadorDAO jugadorDAO;

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

        [TestMethod]
        public void PruebaCrearJugador()
        {
            InicializarDatos();
            
            bool resultado = jugadorDAO.Crear(jugador);
            Assert.IsTrue(resultado);
        }

        [TestMethod]
        public void PruebaValidarUsuario()
        {
            InicializarDatos();

            bool resultado = jugadorDAO.ValidarUsuario(jugador.nickName, jugador.contrasenia);
            Assert.IsTrue(resultado);
        }

        [TestMethod]
        public void PruebaEliminarJugador()
        {
            InicializarDatos();

            bool resultado = jugadorDAO.Eliminar(jugador.nickName);
            Assert.IsTrue(resultado);
        }

        [TestMethod]
        public void PruebaObtenerJugadores()
        {
            List<Jugador> jugadores = new List<Jugador>();

            jugadores = jugadorDAO.Obtener();
            Assert.IsNotNull(jugadores);
        }

        [TestMethod]
        public void PruebaObtenerEntidad()
        {
            InicializarDatos();

            Jugador jugadorObtener = jugadorDAO.ObtenerEntidad(jugador.nickName);
            Assert.IsNotNull(jugadorObtener);
        }
    }
}
