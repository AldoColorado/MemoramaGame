﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Memorama.ProxyLogin {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ProxyLogin.ILoginService", CallbackContract=typeof(Memorama.ProxyLogin.ILoginServiceCallback))]
    public interface ILoginService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILoginService/Login", ReplyAction="http://tempuri.org/ILoginService/LoginResponse")]
        bool Login(string nombre, string contrasenia);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILoginService/Login", ReplyAction="http://tempuri.org/ILoginService/LoginResponse")]
        System.Threading.Tasks.Task<bool> LoginAsync(string nombre, string contrasenia);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/ILoginService/Conectarse")]
        void Conectarse(Modelo.Modelo.Jugador jugador);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/ILoginService/Conectarse")]
        System.Threading.Tasks.Task ConectarseAsync(Modelo.Modelo.Jugador jugador);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILoginService/BuscarClientePorNombre", ReplyAction="http://tempuri.org/ILoginService/BuscarClientePorNombreResponse")]
        bool BuscarClientePorNombre(string nickName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILoginService/BuscarClientePorNombre", ReplyAction="http://tempuri.org/ILoginService/BuscarClientePorNombreResponse")]
        System.Threading.Tasks.Task<bool> BuscarClientePorNombreAsync(string nickName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILoginService/ObtenerClientes", ReplyAction="http://tempuri.org/ILoginService/ObtenerClientesResponse")]
        System.Collections.Generic.Dictionary<Modelo.Modelo.Jugador, object> ObtenerClientes();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILoginService/ObtenerClientes", ReplyAction="http://tempuri.org/ILoginService/ObtenerClientesResponse")]
        System.Threading.Tasks.Task<System.Collections.Generic.Dictionary<Modelo.Modelo.Jugador, object>> ObtenerClientesAsync();
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/ILoginService/Desconectarse")]
        void Desconectarse(Modelo.Modelo.Jugador jugador);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/ILoginService/Desconectarse")]
        System.Threading.Tasks.Task DesconectarseAsync(Modelo.Modelo.Jugador jugador);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ILoginServiceCallback {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILoginService/VerificarUsuarioLogeado", ReplyAction="http://tempuri.org/ILoginService/VerificarUsuarioLogeadoResponse")]
        void VerificarUsuarioLogeado(bool logeado);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/ILoginService/UsuariosConectados")]
        void UsuariosConectados(Modelo.Modelo.Jugador[] jugadores);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ILoginServiceChannel : Memorama.ProxyLogin.ILoginService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class LoginServiceClient : System.ServiceModel.DuplexClientBase<Memorama.ProxyLogin.ILoginService>, Memorama.ProxyLogin.ILoginService {
        
        public LoginServiceClient(System.ServiceModel.InstanceContext callbackInstance) : 
                base(callbackInstance) {
        }
        
        public LoginServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName) : 
                base(callbackInstance, endpointConfigurationName) {
        }
        
        public LoginServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, string remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public LoginServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public LoginServiceClient(System.ServiceModel.InstanceContext callbackInstance, System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, binding, remoteAddress) {
        }
        
        public bool Login(string nombre, string contrasenia) {
            return base.Channel.Login(nombre, contrasenia);
        }
        
        public System.Threading.Tasks.Task<bool> LoginAsync(string nombre, string contrasenia) {
            return base.Channel.LoginAsync(nombre, contrasenia);
        }
        
        public void Conectarse(Modelo.Modelo.Jugador jugador) {
            base.Channel.Conectarse(jugador);
        }
        
        public System.Threading.Tasks.Task ConectarseAsync(Modelo.Modelo.Jugador jugador) {
            return base.Channel.ConectarseAsync(jugador);
        }
        
        public bool BuscarClientePorNombre(string nickName) {
            return base.Channel.BuscarClientePorNombre(nickName);
        }
        
        public System.Threading.Tasks.Task<bool> BuscarClientePorNombreAsync(string nickName) {
            return base.Channel.BuscarClientePorNombreAsync(nickName);
        }
        
        public System.Collections.Generic.Dictionary<Modelo.Modelo.Jugador, object> ObtenerClientes() {
            return base.Channel.ObtenerClientes();
        }
        
        public System.Threading.Tasks.Task<System.Collections.Generic.Dictionary<Modelo.Modelo.Jugador, object>> ObtenerClientesAsync() {
            return base.Channel.ObtenerClientesAsync();
        }
        
        public void Desconectarse(Modelo.Modelo.Jugador jugador) {
            base.Channel.Desconectarse(jugador);
        }
        
        public System.Threading.Tasks.Task DesconectarseAsync(Modelo.Modelo.Jugador jugador) {
            return base.Channel.DesconectarseAsync(jugador);
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ProxyLogin.IRegistroService", CallbackContract=typeof(Memorama.ProxyLogin.IRegistroServiceCallback))]
    public interface IRegistroService {
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IRegistroService/CrearJugador")]
        void CrearJugador(Modelo.Modelo.Jugador jugador);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IRegistroService/CrearJugador")]
        System.Threading.Tasks.Task CrearJugadorAsync(Modelo.Modelo.Jugador jugador);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IRegistroService/EnviarCorreoRegistro")]
        void EnviarCorreoRegistro(string correo, string codigoDeRegistro);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IRegistroService/EnviarCorreoRegistro")]
        System.Threading.Tasks.Task EnviarCorreoRegistroAsync(string correo, string codigoDeRegistro);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IRegistroServiceCallback {
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IRegistroService/VerificarCreacionJugador")]
        void VerificarCreacionJugador(bool creado);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IRegistroService/VerificarEnvioDeCorreo")]
        void VerificarEnvioDeCorreo(bool enviado);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IRegistroServiceChannel : Memorama.ProxyLogin.IRegistroService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class RegistroServiceClient : System.ServiceModel.DuplexClientBase<Memorama.ProxyLogin.IRegistroService>, Memorama.ProxyLogin.IRegistroService {
        
        public RegistroServiceClient(System.ServiceModel.InstanceContext callbackInstance) : 
                base(callbackInstance) {
        }
        
        public RegistroServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName) : 
                base(callbackInstance, endpointConfigurationName) {
        }
        
        public RegistroServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, string remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public RegistroServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public RegistroServiceClient(System.ServiceModel.InstanceContext callbackInstance, System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, binding, remoteAddress) {
        }
        
        public void CrearJugador(Modelo.Modelo.Jugador jugador) {
            base.Channel.CrearJugador(jugador);
        }
        
        public System.Threading.Tasks.Task CrearJugadorAsync(Modelo.Modelo.Jugador jugador) {
            return base.Channel.CrearJugadorAsync(jugador);
        }
        
        public void EnviarCorreoRegistro(string correo, string codigoDeRegistro) {
            base.Channel.EnviarCorreoRegistro(correo, codigoDeRegistro);
        }
        
        public System.Threading.Tasks.Task EnviarCorreoRegistroAsync(string correo, string codigoDeRegistro) {
            return base.Channel.EnviarCorreoRegistroAsync(correo, codigoDeRegistro);
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ProxyLogin.IPartidaService", CallbackContract=typeof(Memorama.ProxyLogin.IPartidaServiceCallback))]
    public interface IPartidaService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPartidaService/CrearPartida", ReplyAction="http://tempuri.org/IPartidaService/CrearPartidaResponse")]
        bool CrearPartida(Modelo.Modelo.Partida partida);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPartidaService/CrearPartida", ReplyAction="http://tempuri.org/IPartidaService/CrearPartidaResponse")]
        System.Threading.Tasks.Task<bool> CrearPartidaAsync(Modelo.Modelo.Partida partida);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPartidaService/GenerarCodigo", ReplyAction="http://tempuri.org/IPartidaService/GenerarCodigoResponse")]
        string GenerarCodigo();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPartidaService/GenerarCodigo", ReplyAction="http://tempuri.org/IPartidaService/GenerarCodigoResponse")]
        System.Threading.Tasks.Task<string> GenerarCodigoAsync();
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IPartidaService/AgregarJugador")]
        void AgregarJugador(Modelo.Modelo.Jugador jugador);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IPartidaService/AgregarJugador")]
        System.Threading.Tasks.Task AgregarJugadorAsync(Modelo.Modelo.Jugador jugador);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPartidaService/BuscarJugadorPorNombre", ReplyAction="http://tempuri.org/IPartidaService/BuscarJugadorPorNombreResponse")]
        bool BuscarJugadorPorNombre(string nickName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPartidaService/BuscarJugadorPorNombre", ReplyAction="http://tempuri.org/IPartidaService/BuscarJugadorPorNombreResponse")]
        System.Threading.Tasks.Task<bool> BuscarJugadorPorNombreAsync(string nickName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPartidaService/ComprobarCodigoPartida", ReplyAction="http://tempuri.org/IPartidaService/ComprobarCodigoPartidaResponse")]
        bool ComprobarCodigoPartida(string codigo);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPartidaService/ComprobarCodigoPartida", ReplyAction="http://tempuri.org/IPartidaService/ComprobarCodigoPartidaResponse")]
        System.Threading.Tasks.Task<bool> ComprobarCodigoPartidaAsync(string codigo);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IPartidaService/DesconectarseDePartida")]
        void DesconectarseDePartida(Modelo.Modelo.Jugador jugador);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IPartidaService/DesconectarseDePartida")]
        System.Threading.Tasks.Task DesconectarseDePartidaAsync(Modelo.Modelo.Jugador jugador);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IPartidaService/GenerarOrdenCartas")]
        void GenerarOrdenCartas();
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IPartidaService/GenerarOrdenCartas")]
        System.Threading.Tasks.Task GenerarOrdenCartasAsync();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IPartidaServiceCallback {
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IPartidaService/JugadoresEnPartida")]
        void JugadoresEnPartida(Modelo.Modelo.Jugador[] jugadores);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IPartidaService/OrdenCartas")]
        void OrdenCartas(int[] numeros);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IPartidaServiceChannel : Memorama.ProxyLogin.IPartidaService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class PartidaServiceClient : System.ServiceModel.DuplexClientBase<Memorama.ProxyLogin.IPartidaService>, Memorama.ProxyLogin.IPartidaService {
        
        public PartidaServiceClient(System.ServiceModel.InstanceContext callbackInstance) : 
                base(callbackInstance) {
        }
        
        public PartidaServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName) : 
                base(callbackInstance, endpointConfigurationName) {
        }
        
        public PartidaServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, string remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public PartidaServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public PartidaServiceClient(System.ServiceModel.InstanceContext callbackInstance, System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, binding, remoteAddress) {
        }
        
        public bool CrearPartida(Modelo.Modelo.Partida partida) {
            return base.Channel.CrearPartida(partida);
        }
        
        public System.Threading.Tasks.Task<bool> CrearPartidaAsync(Modelo.Modelo.Partida partida) {
            return base.Channel.CrearPartidaAsync(partida);
        }
        
        public string GenerarCodigo() {
            return base.Channel.GenerarCodigo();
        }
        
        public System.Threading.Tasks.Task<string> GenerarCodigoAsync() {
            return base.Channel.GenerarCodigoAsync();
        }
        
        public void AgregarJugador(Modelo.Modelo.Jugador jugador) {
            base.Channel.AgregarJugador(jugador);
        }
        
        public System.Threading.Tasks.Task AgregarJugadorAsync(Modelo.Modelo.Jugador jugador) {
            return base.Channel.AgregarJugadorAsync(jugador);
        }
        
        public bool BuscarJugadorPorNombre(string nickName) {
            return base.Channel.BuscarJugadorPorNombre(nickName);
        }
        
        public System.Threading.Tasks.Task<bool> BuscarJugadorPorNombreAsync(string nickName) {
            return base.Channel.BuscarJugadorPorNombreAsync(nickName);
        }
        
        public bool ComprobarCodigoPartida(string codigo) {
            return base.Channel.ComprobarCodigoPartida(codigo);
        }
        
        public System.Threading.Tasks.Task<bool> ComprobarCodigoPartidaAsync(string codigo) {
            return base.Channel.ComprobarCodigoPartidaAsync(codigo);
        }
        
        public void DesconectarseDePartida(Modelo.Modelo.Jugador jugador) {
            base.Channel.DesconectarseDePartida(jugador);
        }
        
        public System.Threading.Tasks.Task DesconectarseDePartidaAsync(Modelo.Modelo.Jugador jugador) {
            return base.Channel.DesconectarseDePartidaAsync(jugador);
        }
        
        public void GenerarOrdenCartas() {
            base.Channel.GenerarOrdenCartas();
        }
        
        public System.Threading.Tasks.Task GenerarOrdenCartasAsync() {
            return base.Channel.GenerarOrdenCartasAsync();
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ProxyLogin.IJuegoService", CallbackContract=typeof(Memorama.ProxyLogin.IJuegoServiceCallback))]
    public interface IJuegoService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IJuegoService/ConectarseJuego", ReplyAction="http://tempuri.org/IJuegoService/ConectarseJuegoResponse")]
        void ConectarseJuego(string nombre);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IJuegoService/ConectarseJuego", ReplyAction="http://tempuri.org/IJuegoService/ConectarseJuegoResponse")]
        System.Threading.Tasks.Task ConectarseJuegoAsync(string nombre);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IJuegoService/MovimientoDeJugador")]
        void MovimientoDeJugador(double x, double y);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IJuegoService/MovimientoDeJugador")]
        System.Threading.Tasks.Task MovimientoDeJugadorAsync(double x, double y);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IJuegoServiceCallback {
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IJuegoService/MostrarMovimiento")]
        void MostrarMovimiento(double x, double y);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IJuegoServiceChannel : Memorama.ProxyLogin.IJuegoService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class JuegoServiceClient : System.ServiceModel.DuplexClientBase<Memorama.ProxyLogin.IJuegoService>, Memorama.ProxyLogin.IJuegoService {
        
        public JuegoServiceClient(System.ServiceModel.InstanceContext callbackInstance) : 
                base(callbackInstance) {
        }
        
        public JuegoServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName) : 
                base(callbackInstance, endpointConfigurationName) {
        }
        
        public JuegoServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, string remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public JuegoServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public JuegoServiceClient(System.ServiceModel.InstanceContext callbackInstance, System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, binding, remoteAddress) {
        }
        
        public void ConectarseJuego(string nombre) {
            base.Channel.ConectarseJuego(nombre);
        }
        
        public System.Threading.Tasks.Task ConectarseJuegoAsync(string nombre) {
            return base.Channel.ConectarseJuegoAsync(nombre);
        }
        
        public void MovimientoDeJugador(double x, double y) {
            base.Channel.MovimientoDeJugador(x, y);
        }
        
        public System.Threading.Tasks.Task MovimientoDeJugadorAsync(double x, double y) {
            return base.Channel.MovimientoDeJugadorAsync(x, y);
        }
    }
}
