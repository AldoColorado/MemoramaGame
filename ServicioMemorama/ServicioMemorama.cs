using Modelo;
using Modelo.Modelo;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServicioMemorama
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "CalculatorService" in both code and config file together.
    //[ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Reentrant)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single,
    ConcurrencyMode = ConcurrencyMode.Multiple, UseSynchronizationContext = true)]

    public partial class ServicioMemorama : ILoginService
    {
        private ILoginServiceCallback callBackActual = null;
        private ObservableCollection<Jugador> jugadores;
        private Dictionary<Jugador, ILoginServiceCallback> clientes;

        public ServicioMemorama()
        {
            jugadores = new ObservableCollection<Jugador>();
            clientes = new Dictionary<Jugador, ILoginServiceCallback>();
        }

        public bool Login(string nombre, string contrasenia)
        {
            bool usuarioLogueado = false;
            JugadorDAO usuarioALoguear = new JugadorDAO();

            string contraseniaEncriptada = string.Empty;
            byte[] encryted = System.Text.Encoding.Unicode.GetBytes(contrasenia);
            contraseniaEncriptada = Convert.ToBase64String(encryted);

            usuarioLogueado = usuarioALoguear.ValidarUsuario(nombre, contraseniaEncriptada);

            if(usuarioLogueado)
            {
                Console.WriteLine("Usuario Logeado");
            }
            else
            {
                Console.WriteLine("No paso Logeado");
            }
            return usuarioLogueado;
        }

        public void Conectarse(Jugador jugador)
        {
            
            try
            {
                if(!BuscarClientePorNombre(jugador.nickName))
                {
                    callBackActual = OperationContext.Current.GetCallbackChannel<ILoginServiceCallback>();

                    if(callBackActual != null)
                    {
                        clientes.Add(jugador, callBackActual);
                        jugadores.Add(jugador);
                        clientes?.ToList().ForEach(c => c.Value.UsuariosConectados(jugadores));
                        Console.WriteLine($"{jugador.nickName} se ha conectado");
                    }
                }
            }
            catch(CommunicationException ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }

        public bool BuscarClientePorNombre(string nickName)
        {
            bool encontrado = false;

            foreach(Jugador c in clientes.Keys)
            {
                if(c.nickName == nickName)
                {
                    encontrado = true;
                }
            }
            return encontrado;
        }

        public Dictionary<Jugador, ILoginServiceCallback> ObtenerClientes()
        {
            return clientes;
        }

        public void Desconectarse(Jugador jugador)
        {
            foreach(Jugador c in clientes.Keys)
            {
                if(jugador.nickName == c.nickName)
                {

                    this.clientes.Remove(c);
                    this.jugadores.Remove(c);
                    foreach(ILoginServiceCallback callback in clientes.Values)
                    {
                        callback.UsuariosConectados(this.jugadores);
                    }

                }
            }
        }
    }

    public partial class ServicioMemorama : IRegistroService
    {
        public bool CrearJugador(Jugador jugador)
        {
            bool creado = false;
            bool jugadorExistente = false;
            JugadorDAO jugadorDAO = new JugadorDAO();

            jugadorExistente = jugadorDAO.ValidarJugadorPorCorreo(jugador.correoElectronico);

            if(!jugadorExistente)
            {
                creado = jugadorDAO.Crear(jugador);
            }

            if(creado)
            {
                Console.WriteLine("Jugador creado");
                creado = true;
            }
            else
            {
                Console.WriteLine("No se creo");
            }

            return creado;
        }

        public bool EnviarCorreoRegistro(string correo, string codigoDeRegistro)
        {
            bool correoEnviado = false;

            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();

            mail.To.Add(correo);
            mail.Subject = "Confirmación de registro en Memorama";
            mail.SubjectEncoding = System.Text.Encoding.UTF8;
            mail.Body = "Tu codigo de confirmación es:" + codigoDeRegistro;
            mail.BodyEncoding = System.Text.Encoding.UTF8;
            mail.IsBodyHtml = true;
            mail.From = new System.Net.Mail.MailAddress("memogamelisuv@gmail.com");

            System.Net.Mail.SmtpClient cliente = new System.Net.Mail.SmtpClient();

            cliente.Credentials = new System.Net.NetworkCredential("memogamelisuv@gmail.com", "luzio1234");
            cliente.Port = 587;
            cliente.EnableSsl = true;
            cliente.Host = "smtp.gmail.com";

            try
            {
                cliente.Send(mail);
                correoEnviado = true;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.GetBaseException());
            }

            return correoEnviado;
        }
    }

    public partial class ServicioMemorama : IRecuperarContraseniaService
    {
        public bool EnviarCorreoRecuperacion(string correo, string codigoRecuperacion)
        {
            bool correoEnviado;

            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();

            mail.To.Add(correo);
            mail.Subject = "Código de recuperación de contraseña";
            mail.SubjectEncoding = System.Text.Encoding.UTF8;
            mail.Body = "Tu código de recuperación es: " + codigoRecuperacion;
            mail.BodyEncoding = System.Text.Encoding.UTF8;
            mail.IsBodyHtml = true;
            mail.From = new System.Net.Mail.MailAddress("memogamelisuv@gmail.com");

            System.Net.Mail.SmtpClient cliente = new System.Net.Mail.SmtpClient();

            cliente.Credentials = new System.Net.NetworkCredential("memogamelisuv@gmail.com", "luzio1234");

            cliente.Port = 587;
            cliente.EnableSsl = true;

            cliente.Host = "smtp.gmail.com";

            try
            {
                cliente.Send(mail);
                correoEnviado = true;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.GetBaseException());
                correoEnviado = false;
            }

            if(correoEnviado)
            {
                Console.WriteLine("El correo se ha enviado");
                return true;
            }
            else
            {
                Console.WriteLine("No se pudo enviar el correo");
                return false;
            }
        }

        public bool ValidarJugadorPorCorreo(string correo)
        {
            bool encontrado;
            JugadorDAO jugadorABuscar = new JugadorDAO();

            encontrado = jugadorABuscar.ValidarJugadorPorCorreo(correo);

            if(encontrado)
            {
                Console.WriteLine("Se encontró jugador");
                return true;
            }
            else
            {
                Console.WriteLine("No se encontró jugador");
                return false;
            }
        }

        public bool ActualizarContrasenia(string contrasenia, string correo)
        {
            JugadorDAO jugadorDAO = new JugadorDAO();
            Jugador jugador = jugadorDAO.ObtenerJugadorPorCorreo(correo);
            bool seCambioContrasenia = false;

            if(jugador != null)
            {
                seCambioContrasenia = jugadorDAO.ActualizarContrasenia(contrasenia, jugador.nickName);
                Console.WriteLine("Se actulizó contraseña");
            }
            else
            {
                Console.WriteLine("No se encontró jugador");
                return false;
            }
            return seCambioContrasenia;
        }
    }


    public partial class ServicioMemorama : IPartidaService
    {
        private ObservableCollection<Jugador> jugadoresEnPartida = new ObservableCollection<Jugador>();
        private Dictionary<Jugador, IPartidaServiceCallback> clientesEnPartida = new Dictionary<Jugador, IPartidaServiceCallback>();
        private IPartidaServiceCallback callBackPartida = null;
        HashSet<int> numeros = new HashSet<int>();
        Random random = new Random();

        public void AgregarJugador(Jugador jugador)
        {
            if(!BuscarJugadorPorNombre(jugador.nickName))
            {

                callBackPartida = OperationContext.Current.GetCallbackChannel<IPartidaServiceCallback>();

                if(callBackPartida != null)
                {
                    clientesEnPartida.Add(jugador, callBackPartida);
                    jugadoresEnPartida.Add(jugador);

                    clientesEnPartida?.ToList().ForEach(c => c.Value.JugadoresEnPartida(jugadoresEnPartida));
                    Console.WriteLine($"{jugador.nickName} se ha conectado a la partida");
                }
            }
        }

        public bool BuscarJugadorPorNombre(string nickName)
        {
            bool encontrado = false;

            foreach(Jugador c in clientesEnPartida.Keys)
            {
                if(c.nickName == nickName)
                {
                    encontrado = true;
                }
            }
            return encontrado;
        }

        public bool ComprobarCodigoPartida(string codigo)
        {
            bool codigoCorrecto = false;

            PartidaDAO partidaDAO = new PartidaDAO();


            codigoCorrecto = partidaDAO.BuscarPartida(codigo);

            if(codigoCorrecto)
            {
                Console.WriteLine("Codigo correcto");
            }
            else
            {
                Console.WriteLine("Codigo incorrecto");
            }

            return codigoCorrecto;
        }

        public bool CrearEstadisticaPartida(Partida partida, Jugador jugador)
        {
            bool creada = false;
            JugadorDAO jugadorDAO = new JugadorDAO();
            PartidaDAO partidaDAO = new PartidaDAO();
            EstadisticaPartidaDAO estadisticaPartidaDAO = new EstadisticaPartidaDAO();
            EstadisticaPartida estadisticaPartida = new EstadisticaPartida();

            Jugador jugadorConsultado;
            Partida partidaConsultada;
            jugadorConsultado = jugadorDAO.ObtenerEntidad(jugador.nickName);
            partidaConsultada = partidaDAO.ObtenerEntidad(partida.codigo);

            estadisticaPartida.idJugador = jugadorConsultado.idJugador;
            estadisticaPartida.idPartida = partidaConsultada.idPartida;

            if(estadisticaPartidaDAO.Crear(estadisticaPartida))
            {
                creada = true;
            }
           
            return creada;
        }

        public bool CrearPartida(Partida partida, Jugador jugador)
        {
            bool creada = false;
            PartidaDAO partidaDAO = new PartidaDAO();

            try
            {
                creada = partidaDAO.Crear(partida);    
            }
            catch(NullReferenceException ex)
            {
                Console.WriteLine(ex.ToString());
            }

            if(creada)
            {
                Console.WriteLine("Partida creada");
            }
            else
            {
                Console.WriteLine("Partida no creada");
            }
            return creada;
        }

        public void DesconectarseDePartida(Jugador jugador)
        {
            foreach(Jugador c in clientesEnPartida.Keys)
            {
                if(jugador.nickName == c.nickName)
                {

                    this.clientesEnPartida.Remove(c);
                    this.jugadoresEnPartida.Remove(c);
                    foreach(IPartidaServiceCallback callback in clientesEnPartida.Values)
                    {
                        callback.JugadoresEnPartida(this.jugadoresEnPartida);
                    }

                }
            }
        }

        public string GenerarCodigo()
        {
            string codigo = "";

            var seed = Environment.TickCount;
            var random = new Random(seed);

            for(int i = 0; i <= 4; i++)
            {
                var valor = random.Next(0, 9);
                codigo += valor;
            }

            return codigo;
        }

        public void GenerarOrdenCartas()
        {
            callBackPartida = OperationContext.Current.GetCallbackChannel<IPartidaServiceCallback>();

            if(callBackPartida != null)
            {
                int contador = 27;

                if(numeros.Count() == 0)
                {
                    numeros.Clear();
                    do
                    {
                        int number = random.Next(1, 2 * contador + 1);
                        numeros.Add(number);

                    }
                    while(numeros.Count < 2 * contador);

                    foreach(var c in clientesEnPartida)
                    {
                        c.Value.OrdenCartas(numeros);
                    }

                }
                else
                {
                    foreach(var c in clientesEnPartida)
                    {
                        c.Value.OrdenCartas(numeros);
                    }
                }

                string nums = "";
                foreach(var n in numeros)
                {
                    nums += n;
                }
            }
        }
    }

    public partial class ServicioMemorama : IJuegoService
    {
        private Dictionary<Jugador, IJuegoServiceCallback> clientesEnJuego = new Dictionary<Jugador, IJuegoServiceCallback>();
        private IJuegoServiceCallback callBackActualJuego = null;
        private ObservableCollection<Jugador> jugadoresEnJuego = new ObservableCollection<Jugador>();
        private ObservableCollection<Jugador> jugadoresPuntajes = new ObservableCollection<Jugador>();
        private ObservableCollection<int> puntajesJugadores = new ObservableCollection<int>();

        public void ConectarseJuego(Jugador jugador)
        {
            callBackActualJuego = OperationContext.Current.GetCallbackChannel<IJuegoServiceCallback>();

            try
            {
                if(callBackActualJuego != null)
                {
                    clientesEnJuego.Add(jugador, callBackActualJuego);

                    jugadoresEnJuego.Add(jugador);

                    foreach(var c in clientesEnJuego)
                    {
                        c.Value.JugadoresEnJuego(jugadoresEnJuego);
                    }
                    Console.WriteLine($"{jugador.nickName} se ha conectado");
                }
            }
            catch(CommunicationException ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }

        public void DeconectarseDeJuego(Jugador jugadorADeconectar)
        {
            try
            {
                foreach(Jugador jugador in clientesEnJuego.Keys)
                {
                    if(jugadorADeconectar.nickName == jugador.nickName)
                    {
                        this.clientesEnJuego.Remove(jugador);
                        this.jugadoresEnJuego.Remove(jugador);
                        foreach(var c in clientesEnJuego)
                        {
                            c.Value.JugadoresEnJuego(jugadoresEnJuego);
                        }
                    }
                }
            }
            catch(CommunicationException ex)
            {
                Console.WriteLine(ex.ToString());
            }
            
        }

        public void InicializarPuntajes(Jugador jugador, int puntaje)
        {
            callBackActualJuego = OperationContext.Current.GetCallbackChannel<IJuegoServiceCallback>();

            try
            {
                if(callBackActualJuego != null)
                {
                    jugadoresPuntajes.Add(jugador);
                    puntajesJugadores.Add(puntaje);

                    foreach(var c in clientesEnJuego)
                    {
                        c.Value.ActualizarPuntajes(puntajesJugadores);
                    }
                }
            }
            catch(CommunicationException ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }

        public void ModificarPuntajes(Jugador jugador, int puntaje)
        {
            callBackActualJuego = OperationContext.Current.GetCallbackChannel<IJuegoServiceCallback>();

            try
            {
                ObservableCollection<int> puntajesTemporal = new ObservableCollection<int>();

                if(callBackActualJuego != null)
                {
                    int contadorColeccionJugadores = 0;
                    int indice = 0;
                    foreach(var c in puntajesJugadores)
                    {
                        puntajesTemporal.Add(c);
                    }

                    foreach(var j in jugadoresPuntajes)
                    {
                        if(j.nickName.Equals(jugador.nickName))
                        {
                            indice = contadorColeccionJugadores;
                        }
                        contadorColeccionJugadores++;
                    }

                    int contadorColeccionPuntajes = 0;

                    foreach(var c in puntajesTemporal)
                    {
                        if(indice == contadorColeccionPuntajes)
                        {
                            puntajesJugadores.RemoveAt(indice);
                            puntajesJugadores.Insert(indice, puntaje);
                        }
                        contadorColeccionPuntajes++;
                    }

                    foreach(var c in clientesEnJuego)
                    {
                        c.Value.ActualizarPuntajes(puntajesJugadores);
                    }
                }

            }
            catch(CommunicationException ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public void MovimientoDeJugador(double x, double y)
        {
            callBackActualJuego = OperationContext.Current.GetCallbackChannel<IJuegoServiceCallback>();
            try
            {
                if(callBackActualJuego != null)
                {
                    foreach(var c in clientesEnJuego)
                    {
                        if(c.Value != callBackActualJuego)
                        {
                            c.Value.MostrarMovimiento(x, y);
                        }
                    }
                }
            }
            catch(CommunicationException ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public void ReportarJugador(string jugador, Jugador jugadorQueReporta)
        {
            callBackActualJuego = OperationContext.Current.GetCallbackChannel<IJuegoServiceCallback>();

            try
            {
                if(callBackActual != null)
                {
                    foreach(var c in clientesEnJuego)
                    {
                        if(c.Key.nickName == jugador)
                        {
                            c.Value.ActualizarReporteJugador(jugadorQueReporta);
                        }
                    }
                }
            }
            catch(CommunicationException ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }

    public partial class ServicioMemorama : IEstadisticasService
    {

        List<int> listaDepuntajes;
        List<string> listaJugadores;
        ObservableCollection<Tabla> jugadorPuntaje = new ObservableCollection<Tabla>();


        public bool GuardarEstadisticasPartida(EstadisticaPartida estadisticaPartida, Jugador jugador, Partida partida)
        {
            bool estadisticaGuardada = false;
            Jugador jugadorConsultado = new Jugador();
            Partida partidaConsultada = new Partida();

            EstadisticaPartidaDAO estadisticaDAO = new EstadisticaPartidaDAO();
            JugadorDAO jugadorDAO = new JugadorDAO();
            PartidaDAO partidaDAO = new PartidaDAO();

            try
            {
                jugadorConsultado = jugadorDAO.ObtenerEntidad(jugador.nickName);
                partidaConsultada = partidaDAO.ObtenerEntidad(partida.codigo);

                estadisticaPartida.idJugador = jugadorConsultado.idJugador;
                estadisticaPartida.idPartida = partidaConsultada.idPartida;

                estadisticaDAO.Modificar(estadisticaPartida);
            }
            catch(NullReferenceException ex)
            {
                Console.WriteLine(ex.ToString());
            }
            
            return estadisticaGuardada;
        }

        public bool GenerarTablaDePuntajes()
        {
            bool generada = false;
            listaJugadores = new List<string>();
            listaDepuntajes = new List<int>();
            
            List<EstadisticaPartida> estadisticas = new List<EstadisticaPartida>();
            JugadorDAO jugadorDAO = new JugadorDAO();
            EstadisticaPartidaDAO estadisticaPartidaDAO = new EstadisticaPartidaDAO();

            try
            {
                List<Jugador> listaDeJugadores = new List<Jugador>();
                listaDeJugadores = jugadorDAO.Obtener();
                estadisticas = estadisticaPartidaDAO.Obtener();

                foreach(var jugador in listaDeJugadores)
                {
                    Tabla tablaJugador = new Tabla();
                    listaJugadores.Add(jugador.nickName);
                    int puntaje = 0;

                    foreach(var e in estadisticas)
                    {
                        if(jugador.idJugador == e.idJugador)
                        {
                            puntaje += e.puntaje.GetValueOrDefault();
                        }
                    }
                    
                    listaDepuntajes.Add(puntaje);
                }     
                generada = true;
            }
            catch(CommunicationException ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return generada;
        }

        public ObservableCollection<Tabla> ObtenerPuntajesJugadores()
        {
            return jugadorPuntaje;
        }

        public List<int> ObtenerPuntaje()
        {
            return listaDepuntajes;
        }

        public List<string> ObtenerJugadores()
        {
            return listaJugadores;
        }
    }
}
