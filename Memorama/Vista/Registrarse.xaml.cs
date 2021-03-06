﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Modelo.Modelo;

namespace Memorama
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class Registrarse : ProxyRegistro.IRegistroServiceCallback
    {
        
        Jugador jugador = new Jugador();
        public string codigo;
        public bool correoEnviado;

        /// <summary>
        /// Constructor de la clase
        /// </summary>
        public Registrarse()
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            InitializeComponent();
        }

        /// <summary>
        /// Metodo propiedad de la clase
        /// </summary>
        /// <param name="sender">Propiedad del evento</param>
        /// <param name="e">Propiedad del evento</param>
        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        /// <summary>
        /// Motodo para hacer el registro del jugador
        /// </summary>
        /// <param name="sender">Propiedad del evento</param>
        /// <param name="e">Propiedad del evento</param>
        private void BotonRegistrarse(object sender, RoutedEventArgs e)
        {
            string contraseniaEncriptada = string.Empty;
            byte[] encryted = System.Text.Encoding.Unicode.GetBytes(TextoPassword.Password);
            contraseniaEncriptada = Convert.ToBase64String(encryted);
            string contrasenia = TextoPassword.Password;
            
            jugador.nickName = TextoNickName.Text;
            jugador.nombre = TextoNombre.Text;
            jugador.correoElectronico = TextoCorreo.Text;
            jugador.contrasenia = contraseniaEncriptada;

            bool nicknameValido = ValidarCampo(TextoNickName.Text);
            bool nombreValido = ValidarCampo(TextoNombre.Text);
            bool correoValido = ValidarCampo(TextoCorreo.Text);
            bool contraseniaValida = ValidarCampo(contrasenia);

            GenerarCodigoRegistro();

            InstanceContext contexto = new InstanceContext(this);
            ProxyRegistro.RegistroServiceClient servidor = new ProxyRegistro.RegistroServiceClient(contexto);

            if(nicknameValido && nombreValido && correoValido && contraseniaValida)
            {
                if(validarCorreoElectronico())
                {
                    try
                    {
                        bool enviado = servidor.EnviarCorreoRegistro(TextoCorreo.Text, codigo);

                        if(enviado)
                        {
                            ConfirmarRegistro ventanaConfirmarRegistro = new ConfirmarRegistro(jugador, codigo);
                            ventanaConfirmarRegistro.Show();
                            Window.GetWindow(this).Close();
                        }
                        else
                        {
                            MessageBox.Show("No se pudo enviar el correo, rectifique que sea valido");
                        }      
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show("ERROR: El servidor no se encuentra disponible, intenta más tarde");
                        Window.GetWindow(this).Close();
                    }
                }
                else
                {
                    MessageBox.Show("Formato de correo invalido, ingresa el correo correctamente");
                }
                
            }
            else
            {
                MessageBox.Show("Campos inválidos, por favor ingresa los datos correctamente");
            }
        }

        /// <summary>
        /// Metodo para generar el codigo de registro
        /// </summary>
        public void GenerarCodigoRegistro()
        {
            var seed = Environment.TickCount;
            var random = new Random(seed);

            for(int i = 0; i <= 4; i++)
            {
                var value = random.Next(0, 9);
                codigo += value;
            }
        }

        /// <summary>
        /// Metodo no implementado
        /// </summary>
        /// <param name="creado"></param>
        public void VerificarCreacionJugador(bool creado)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Metodo para verificar el envio de correo
        /// </summary>
        /// <param name="enviado"></param>
        public void VerificarEnvioDeCorreo(bool enviado)
        {
            this.correoEnviado = enviado;
        }

        /// <summary>
        /// Metodo para verificar el envio del correo
        /// </summary>
        /// <returns></returns>
        public bool validarCorreoElectronico()
        {
            try
            {
                MailAddress correo = new MailAddress(TextoCorreo.Text);
                return true;
            }
            catch(FormatException)
            {
                return false;
            }
        }
    
        public bool ValidarCampo(string campo)
        {
            bool esValido = false;
            if (campo != "")
            {
                for (int i = 0; i < campo.Length; i++)
                {
                    if (campo.Substring(i,1) == " ")
                    {
                        return esValido;
                    }
                }
                esValido = true;
            }
            else
            {
               esValido = false;
            }
            return esValido;
        }
    }
}