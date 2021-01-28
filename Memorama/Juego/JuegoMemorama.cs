using Modelo;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace Memorama
{
    /// <summary>
    /// La clase JuegoMemorama se utiliza para dar funcionalidad a la ventana de Juego.
    /// </summary>
    public class JuegoMemorama : INotifyPropertyChanged
    {
        public Tablero tablero { get; } = new Tablero();
        public event PropertyChangedEventHandler PropertyChanged;
        int contadorParesDeCartas = 27;

        ObservableCollection<int> numerosOrdenCartas = new ObservableCollection<int>();
        List<Carta> listaDeCartas = new List<Carta>();
        string nombreDeLaCarta;

        DispatcherTimer tiempoCartasVolteadas = new DispatcherTimer() { Interval = TimeSpan.FromMilliseconds(1000) };
        bool bloquearClick = false;
        int click = 0;
        int columna;
        int fila;
        int columna1;
        int columna2;
        int fila1;
        int fila2;
        int paresDeCartas;
        public int paresRestantes { get { return paresDeCartas; } set { paresDeCartas = value; NotificarCambio("paresRestantes"); } }

        /// <summary>
        /// Constructor de la clase
        /// </summary>
        public JuegoMemorama()
        {
            tiempoCartasVolteadas.Tick += CubrirORemoverCartas;
        }

        /// <summary>
        /// Metodo donde se inicializan otro metodos de la misma clase y se ejecutan metodos de la ckase Tablero
        /// </summary>
        public void NuevoJuego()
        {
            CrearListaDeCartas();
            tablero.CrearArregloDeCartas(listaDeCartas);
            tablero.DibujarArregloDeCartas();
            tablero.CubrirImagenes();
            tablero.DibujarArregloCoverturasDeImagenes();
            paresRestantes = contadorParesDeCartas;
        }

        /// <summary>
        /// Metodo donde se inician otros metodos de la clase tablero, aqui mismo se inicializa el tiempo para la cartas volteadas.
        /// </summary>
        private void CubrirORemoverCartas(object sender, EventArgs e)
        {
            tablero.DibujarArregloCoverturasDeImagenes();
            tiempoCartasVolteadas.Stop();
            bloquearClick = false;
        }

        /// <summary>
        /// Metodo que crea la lista de cartas necesaria para desplegarse dentro del tablero
        /// </summary>
        private void CrearListaDeCartas()
        {
            listaDeCartas.Clear();
            foreach(int c in numerosOrdenCartas)
            {
                int numeroDeCarta = c;
                if(numeroDeCarta > 27)
                    numeroDeCarta -= 27;
                nombreDeLaCarta = numeroDeCarta.ToString();

                Uri uri = new Uri("C:/Users/Aldo/source/repos/MemoramaGame/Memorama/bin/Debug/img/" + nombreDeLaCarta + ".png");
               
                BitmapImage bmp = new BitmapImage(uri);
                Image imagen = new Image();
                imagen.Source = bmp;
                Carta card = new Carta(imagen, numeroDeCarta);
                listaDeCartas.Add(card);
            }
        }

        /// <summary>
        /// Metodo donde se controla la accion del click dentro del tablero.
        /// </summary>
        /// <param name="x">representa la posicion vertical del click</param>
        /// <param name="y">representa la posicion horizontal del click</param>
        /// <returns>Regresa el resultado al comparar un par de cartas</returns>
        public bool Click(double x, double y)
        {
            bool resultadoCartasVolteadas = false;

            if(bloquearClick)
                return false;
            bloquearClick = true;

            columna = (int)x / 130;
            fila = (int)y / 130;

            if(columna > 8 || fila > 5)
            {
                bloquearClick = false;
                return false;
            }

            if(tablero.arregloImagenesCubiertas[columna, fila] == 2)
            {
                bloquearClick = false;
                return false;
            }

            click++;
            if(click == 1)
            {
                columna1 = columna;
                fila1 = fila;
                tablero.RevelarCarta1(columna1, fila1);
                tablero.DibujarArregloCoverturasDeImagenes();
                bloquearClick = false;
            }
            else if(click == 2)
            {
                columna2 = columna;
                fila2 = fila;
                if(columna1 == columna2 && fila1 == fila2)
                {
                    bloquearClick = false;
                    click = 1;
                    return false;
                }
                tablero.RevelarCarta2(columna2, fila2);
                tablero.DibujarArregloCoverturasDeImagenes();

                resultadoCartasVolteadas = CompararCartasElegidas(columna1, fila1, columna2, fila2);
                if(resultadoCartasVolteadas)
                {
                    tablero.RemoverCarta(columna1, fila1, columna2, fila2);
                    tiempoCartasVolteadas.Start();
                    if(paresRestantes == 0)
                    {
                        tablero.RemoverTodo();
                    }
                }
                else
                {
                    tablero.VolverACubrir(columna1, fila1, columna2, fila2);
                    tiempoCartasVolteadas.Start();
                }
                click = 0;
            }

            return resultadoCartasVolteadas;
        }

        /// <summary>
        /// Compara un par de cartas elegidas por un jugador
        /// </summary>
        /// <param name="columna1">columna de la carta 1</param>
        /// <param name="fila1">fila de la carta 1</param>
        /// <param name="columna2">columna de la carta 2</param>
        /// <param name="fila2">fila de la carta 2</param>
        /// <returns>Regresa verdadero en caso de que las cartas sean las mismas</returns>
        private bool CompararCartasElegidas(int columna1, int fila1, int columna2, int fila2)
        {
            int numero1 = tablero.arregloDeCartas[columna1, fila1].numero;
            int numero2 = tablero.arregloDeCartas[columna2, fila2].numero;

            if(numero1 == numero2)
            {
                paresRestantes--;
                return true;
            }
            else
                return false;
        }

        /// <summary>
        /// Inicializa el arreglo de cartas donde se asigna el orden de cada una de ellas
        /// </summary>
        /// <param name="numeros">Arreglo de 54 numeros ordenados aleatoriamente</param>
        public void InicializarOrdenCartas(ObservableCollection<int> numeros)
        {
            numerosOrdenCartas.Clear();
            foreach(var n in numeros)
            {
                this.numerosOrdenCartas.Add(n);
            }
        }

        /// <summary>
        /// Metodo en el cual se controla dinamicamente el cambio de la propiedad "paresrestantes"
        /// </summary>
        /// <param name="propiedadQueCambia">Propiedad a la cual se le notificara el cambio</param>
        protected void NotificarCambio(string propiedadQueCambia)
        {
            if(PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propiedadQueCambia));
        }
    }
}
