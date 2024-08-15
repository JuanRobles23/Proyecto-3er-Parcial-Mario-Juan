using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_3er_Parcial_Mario_Juan
{
    internal class Program
    {
        static void Main()
        {
            /* Declaración de variables: 
               - ultimaMatriz almacenará el resultado de la última operación de matrices.
               - nombreArchivo es el nombre del archivo donde se guardará la matriz resultante.
               - continuar controla si el ciclo del menú sigue ejecutándose. */
            double[,] ultimaMatriz = null;
            string nombreArchivo = "matriz_resultado.txt";
            bool continuar = true;

            /* Bucle principal del programa que muestra el menú y procesa las opciones del usuario. */
            while (continuar)
            {
                Console.Clear();  /* Limpia la consola para una mejor presentación del menú. */
                Console.WriteLine("Menú:");
                Console.WriteLine("1. Sumar matrices");
                Console.WriteLine("2. Restar matrices");
                Console.WriteLine("3. Multiplicar matrices");
                Console.WriteLine("4. Mostrar última matriz resultante");
                Console.WriteLine("5. Salir");
                Console.Write("Seleccione una opción: ");
                string opcion = Console.ReadLine();  /* Lee la opción seleccionada por el usuario. */

                try
                {
                    /* Evalúa la opción seleccionada por el usuario y ejecuta la operación correspondiente. */
                    switch (opcion)
                    {
                        case "1": // Sumar matrices
                            ultimaMatriz = RealizarOperacionMatrices("suma");
                            break;

                        case "2": // Restar matrices
                            ultimaMatriz = RealizarOperacionMatrices("resta");
                            break;

                        case "3": // Multiplicar matrices
                            ultimaMatriz = RealizarOperacionMatrices("multiplicación");
                            break;

                        case "4": // Mostrar última matriz resultante
                            if (ultimaMatriz == null)
                            {
                                Console.WriteLine("No se ha realizado ninguna operación de matriz aún.");
                            }
                            else
                            {
                                MostrarMatriz(ultimaMatriz);  /* Muestra la última matriz resultante. */
                            }
                            break;

                        case "5": // Salir
                            Console.WriteLine("¡Gracias por usar el programa!");
                            continuar = false;  /* Cambia continuar a false para salir del bucle. */
                            break;

                        default:  /* Maneja la entrada de opciones no válidas. */
                            Console.WriteLine("Opción no válida, intente nuevamente.");
                            break;
                    }

                    /* Si se realizó una operación de matriz (ultimaMatriz no es null), guarda el resultado en un archivo. */
                    if (ultimaMatriz != null)
                    {
                        GuardarMatrizEnArchivo(ultimaMatriz, nombreArchivo);
                        Console.WriteLine($"La matriz resultante fue almacenada en el archivo {nombreArchivo}.");
                    }

                    /* Pregunta al usuario si desea realizar otra operación antes de salir del programa. */
                    if (continuar)
                    {
                        Console.Write("¿Desea realizar otro cálculo de matrices? (s/n): ");
                        if (Console.ReadLine().ToLower() != "s")
                        {
                            continuar = false;
                            Console.WriteLine("¡Gracias por usar el programa!");
                        }
                    }
                }
                /* Captura y maneja cualquier excepción que pueda ocurrir durante la ejecución del programa. */
                catch (Exception ex)
                {
                    Console.WriteLine($"Se produjo un error: {ex.Message}");
                }
            }
        }

        /* Método para leer una matriz desde la entrada del usuario. */
        static double[,] LeerMatriz(int filas, int columnas)
        {
            double[,] matriz = new double[filas, columnas];
            Console.WriteLine($"Ingrese los elementos de la matriz de {filas}x{columnas}:");
            for (int i = 0; i < filas; i++)
            {
                for (int j = 0; j < columnas; j++)
                {
                    while (true)
                    {
                        try
                        {
                            Console.Write($"Elemento [{i + 1}][{j + 1}]: ");
                            matriz[i, j] = Convert.ToDouble(Console.ReadLine());
                            break;  /* Sale del bucle una vez que se ha ingresado un valor válido. */
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Por favor, ingrese un valor numérico válido.");
                        }
                    }
                }
            }
            return matriz;
        }

        /* Método para mostrar una matriz en la consola. */
        static void MostrarMatriz(double[,] matriz)
        {
            int filas = matriz.GetLength(0);
            int columnas = matriz.GetLength(1);
            Console.WriteLine("Matriz Resultante:");
            for (int i = 0; i < filas; i++)
            {
                for (int j = 0; j < columnas; j++)
                {
                    Console.Write($"{matriz[i, j],6:F2} ");  /* Formato de salida con 2 decimales. */
                }
                Console.WriteLine();
            }
        }

        /* Método para sumar dos matrices. */
        static double[,] SumarMatrices(double[,] m1, double[,] m2)
        {
            int filas = m1.GetLength(0);
            int columnas = m1.GetLength(1);
            double[,] resultado = new double[filas, columnas];
            for (int i = 0; i < filas; i++)
            {
                for (int j = 0; j < columnas; j++)
                {
                    resultado[i, j] = m1[i, j] + m2[i, j];
                }
            }
            return resultado;
        }

        /* Método para restar dos matrices. */
        static double[,] RestarMatrices(double[,] m1, double[,] m2)
        {
            int filas = m1.GetLength(0);
            int columnas = m1.GetLength(1);
            double[,] resultado = new double[filas, columnas];
            for (int i = 0; i < filas; i++)
            {
                for (int j = 0; j < columnas; j++)
                {
                    resultado[i, j] = m1[i, j] - m2[i, j];
                }
            }
            return resultado;
        }

        /* Método para multiplicar dos matrices. */
        static double[,] MultiplicarMatrices(double[,] m1, double[,] m2)
        {
            int filas1 = m1.GetLength(0);
            int columnas1 = m1.GetLength(1);
            int filas2 = m2.GetLength(0);
            int columnas2 = m2.GetLength(1);

            /* Verifica que las matrices puedan ser multiplicadas (columnas de m1 = filas de m2). */
            if (columnas1 != filas2)
                throw new InvalidOperationException("Las matrices no se pueden multiplicar.");

            double[,] resultado = new double[filas1, columnas2];
            for (int i = 0; i < filas1; i++)
            {
                for (int j = 0; j < columnas2; j++)
                {
                    resultado[i, j] = 0;
                    for (int k = 0; k < columnas1; k++)
                    {
                        resultado[i, j] += m1[i, k] * m2[k, j];
                    }
                }
            }
            return resultado;
        }

        /* Método para guardar una matriz en un archivo de texto. */
        static void GuardarMatrizEnArchivo(double[,] matriz, string nombreArchivo)
        {
            using (StreamWriter writer = new StreamWriter(nombreArchivo))
            {
                int filas = matriz.GetLength(0);
                int columnas = matriz.GetLength(1);
                for (int i = 0; i < filas; i++)
                {
                    for (int j = 0; j < columnas; j++)
                    {
                        writer.Write($"{matriz[i, j],6:F2} ");
                    }
                    writer.WriteLine();
                }
            }
        }

        /* Método que realiza la operación seleccionada por el usuario (suma, resta o multiplicación). */
        static double[,] RealizarOperacionMatrices(string operacion)
        {
            /* Solicita al usuario el tamaño de las matrices. */
            Console.Write("Ingrese el número de filas de la primera matriz: ");
            int filas1 = Convert.ToInt32(Console.ReadLine());
            Console.Write("Ingrese el número de columnas de la primera matriz: ");
            int columnas1 = Convert.ToInt32(Console.ReadLine());

            double[,] matriz1 = LeerMatriz(filas1, columnas1);

            Console.Write("Ingrese el número de filas de la segunda matriz: ");
            int filas2 = Convert.ToInt32(Console.ReadLine());
            Console.Write("Ingrese el número de columnas de la segunda matriz: ");
            int columnas2 = Convert.ToInt32(Console.ReadLine());

            double[,] matriz2 = LeerMatriz(filas2, columnas2);

            double[,] resultado = null;

            /* Realiza la operación seleccionada, verificando que las dimensiones sean correctas. */
            if (operacion == "suma")
            {
                if (filas1 != filas2 || columnas1 != columnas2)
                {
                    Console.WriteLine("Las matrices deben tener las mismas dimensiones para la suma.");
                    return null;
                }
                resultado = SumarMatrices(matriz1, matriz2);
            }
            else if (operacion == "resta")
            {
                if (filas1 != filas2 || columnas1 != columnas2)
                {
                    Console.WriteLine("Las matrices deben tener las mismas dimensiones para la resta.");
                    return null;
                }
                resultado = RestarMatrices(matriz1, matriz2
                resultado = RestarMatrices(matriz1, matriz2);
            }
            else if (operacion == "multiplicación")
            {
                if (columnas1 != filas2)
                {
                    Console.WriteLine("El número de columnas de la primera matriz debe ser igual al número de filas de la segunda matriz.");
                    return null;
                }
                resultado = MultiplicarMatrices(matriz1, matriz2);
            }

            /* Muestra el resultado de la operación seleccionada. */
            Console.WriteLine($"Resultado de la {operacion}:");
            MostrarMatriz(resultado);

            return resultado;  /* Devuelve la matriz resultante de la operación. */
        }
    }
}


                                           
