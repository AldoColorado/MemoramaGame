
using Modelo;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;


namespace Memorama
{
    public class Tablero
    {
        public ObservableCollection<Rectangle> rectangulos { get; private set; } = new ObservableCollection<Rectangle>();
        public ObservableCollection<Image> imagenes { get; private set; } = new ObservableCollection<Image>();

        int ancho = 9;                                            
        int alto = 6;            
        int indiceAuxiliarCartas = 0;
        
        int x;
        int y;

        public Carta[,] arregloDeCartas { get; set; }                     
        public int[,] arregloImagenesCubiertas { get; set; }

        public Tablero()
        {
            arregloDeCartas = new Carta[ancho, alto];
            arregloImagenesCubiertas = new int[ancho, alto];
        }

        public void CrearArregloDeCartas(List<Carta> listaDeCartas)          
        {
            indiceAuxiliarCartas = 0;
            for(int i = 0; i < ancho; i++)
            {
                for(int k = 0; k < alto; k++)
                {
                    arregloDeCartas[i, k] = listaDeCartas[indiceAuxiliarCartas];
                    indiceAuxiliarCartas++;
                }
            }
        }

        public void DibujarArregloDeCartas()
        {
            imagenes.Clear();
            x = 2;                                                  
            y = 2;
            for(int i = 0; i < ancho; i++)
            {
                for(int k = 0; k < alto; k++)
                {
                    Canvas.SetLeft(arregloDeCartas[i, k].imagen, x);             
                    Canvas.SetTop(arregloDeCartas[i, k].imagen, y);
                    imagenes.Add(arregloDeCartas[i, k].imagen);                   
                    y = y + 130;
                }
                x = x + 130;
                y = 2;
            }
        }

        public void CubrirImagenes()                          
        {
            for(int i = 0; i < ancho; i++)
            {
                for(int k = 0; k < alto; k++)
                {
                    arregloImagenesCubiertas[i, k] = 0;
                }
            }
        }

        public void DibujarArregloCoverturasDeImagenes()                      
        {
            rectangulos.Clear();
            x = 2;                                          
            y = 2;
            for(int i = 0; i < ancho; i++)
            {
                for(int k = 0; k < alto; k++)
                {
                    SolidColorBrush fill = new SolidColorBrush();
                    SolidColorBrush border = new SolidColorBrush();
                    if(arregloImagenesCubiertas[i, k] == 0)             
                    {
                        fill.Color = Colors.DarkTurquoise;
                        border.Color = Colors.Black;
                    }
                    else if(arregloImagenesCubiertas[i, k] == 1)        
                    {
                        fill.Color = Colors.Transparent;
                        border.Color = Colors.Black;
                    }
                    else
                    {
                        fill.Color = SystemColors.WindowColor; 
                        border.Color = SystemColors.WindowColor;
                    }

                    Rectangle rectangulo = new Rectangle()              
                    {
                        Width = 128,
                        Height = 128,
                        Fill = fill,
                        Stroke = border,
                        StrokeThickness = 1,
                    };
                    Canvas.SetLeft(rectangulo, x);                     
                    Canvas.SetTop(rectangulo, y);
                    rectangulos.Add(rectangulo);                            
                    y = y + 130;
                }
                x = x + 130;
                y = 2;
            }
        }

        public void RevelarCarta1(int columna1, int fila1)     
        {
            arregloImagenesCubiertas[columna1, fila1] = 1;            
        }

        public void RevelarCarta2(int columna2, int fila2)     
        {
            arregloImagenesCubiertas[columna2, fila2] = 1;
        }

        public void VolverACubrir(int columna1, int fila1, int columna2, int fila2)
        {
            arregloImagenesCubiertas[columna1, fila1] = 0;          
            arregloImagenesCubiertas[columna2, fila2] = 0;          
        }

        public void RemoverCarta(int columa1, int fila1, int columna2, int fila2)
        {
            arregloImagenesCubiertas[columa1, fila1] = 2;
            arregloImagenesCubiertas[columna2, fila2] = 2;
        }

        public void RemoverTodo()                             
        {
            for(int i = 0; i < ancho; i++)
            {
                for(int k = 0; k < alto; k++)
                {
                    arregloImagenesCubiertas[i, k] = 1;
                }
            }
        }

    }
}
