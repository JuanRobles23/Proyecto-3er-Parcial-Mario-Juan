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
        /* Método principal del programa */
        static void Main()
        {
            /* Variable para almacenar la última matriz resultante */
            double[,] ultimaMatriz = null;

            /* Nombre del archivo donde se guardará la matriz resultante */
            string nombreArchivo = "matriz_resultado.txt";

            /* Variable para controlar el ciclo del menú */
            bool continuar = true;

            /* Ciclo del menú principal */
            while (continuar)
            {
                Console.Clear();
                Console.WriteLine("Menú:");
                Console.WriteLine("1. Sumar matrices");
                Console.WriteLine("2. Restar matrices");
                Console.WriteLine("3. Multiplicar matrices");
                Console.WriteLine("4. Mostrar última matriz resultante");
                Console.WriteLine("5. Salir");
                Console.Write("Seleccione una opción: ");
                string opcion = Console.ReadLine();

                try
                {
                    /* Manejo de las diferentes opciones del menú */
                    switch (opcion)
                    {
                        case "1": // Opción para sumar matrices
                            ultimaMatriz = RealizarOperacionMatrices("suma");
                            break;

                        case "2": // Opción para restar matrices
                            ultimaMatriz = RealizarOperacionMatrices("resta");
                            break;

                        case "3": // Opción para multiplicar matrices
                            ultimaMatriz = RealizarOperacionMatrices("multiplicación");
                            break;

                        case "4": // Opción para mostrar la última matriz resultante
                            if (ultimaMatriz == null)
                            {
                                Console.WriteLine("No se ha realizado ninguna operación de matriz aún.");
                            }
                            else
                            {
                                MostrarMatriz(ultimaMatriz);
                            }
                            break;

                        case "5": // Opción para salir del programa
                            Console.WriteLine("¡Gracias por usar el programa!");
                            continuar = false;
                            break;

                        default: // Opción no válida
                            Console.WriteLine("Opción no válida, intente nuevamente.");
                            break;
                    }

                    /* Guardar la matriz resultante en un archivo si se ha realizado alguna operación */
                    if (ultimaMatriz != null)
                    {
                        GuardarMatrizEnArchivo(ultimaMatriz, nombreArchivo);
                        Console.WriteLine($"La matriz resultante fue almacenada en el archivo {nombreArchivo}.");
                    }

                    /* Preguntar si desea realizar otra operación */
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
                catch (Exception ex)
                {
                    /* Manejo de errores */
                    Console.WriteLine($"Se produjo un error: {ex.Message}");
                }
            }
        }

        /* Método para leer una matriz desde la consola */
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
                            /* Solicitar el valor de cada elemento de la matriz */
                            Console.Write($"Elemento [{i + 1}][{j + 1}]: ");
                            matriz[i, j] = Convert.ToDouble(Console.ReadLine());
                            break;
                        }
                        catch (FormatException)
                        {
                            /* Manejo de errores por formato incorrecto */
                            Console.WriteLine("Por favor, ingrese un valor numérico válido.");
                        }
                    }
                }
            }
            return matriz;
        }

        /* Método para mostrar una matriz en la consola */
        static void MostrarMatriz(double[,] matriz)
        {
            int filas = matriz.GetLength(0);
            int columnas = matriz.GetLength(1);
            Console.WriteLine("Matriz Resultante:");
            for (int i = 0; i < filas; i++)
            {
                for (int j = 0; j < columnas; j++)
                {
                    Console.Write($"{matriz[i, j],6:F2} "); // Mostrar cada elemento con formato de 2 decimales
                }
                Console.WriteLine();
            }
        }

        /* Método para sumar dos matrices */
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

        /* Método para restar dos matrices */
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

        /* Método para multiplicar dos matrices */
        static double[,] MultiplicarMatrices(double[,] m1, double[,] m2)
        {
            int filas1 = m1.GetLength(0);
            int columnas1 = m1.GetLength(1);
            int filas2 = m2.GetLength(0);
            int columnas2 = m2.GetLength(1);

            /* Verificar si las matrices se pueden multiplicar */
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

        /* Método para guardar una matriz en un archivo de texto */
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
                        writer.Write($"{matriz[i, j],6:F2} "); // Guardar cada elemento con formato de 2 decimales
                    }
                    writer.WriteLine(); // Nueva línea después de cada fila
                }
            }
        }

        /* Método que coordina la lectura y operación de las matrices */
        static double[,] RealizarOperacionMatrices(string operacion)
        {
            /* Solicitar las dimensiones de la primera matriz */
            Console.Write("Ingrese el número de filas de la primera matriz: ");
            int filas1 = Convert.ToInt32(Console.ReadLine());
            Console.Write("Ingrese el número de columnas de la primera matriz: ");
            int columnas1 = Convert.ToInt32(Console.ReadLine());

            double[,] matriz1 = LeerMatriz(filas1, columnas1);

            /* Solicitar las dimensiones de la segunda matriz */
            Console.Write("Ingrese el número de filas de la segunda matriz: ");
            int filas2 = Convert.ToInt32(Console.ReadLine());
            Console.Write("Ingrese el número de columnas de la segunda matriz: ");
            int columnas2 = Convert.ToInt32(Console.ReadLine());

            double[,] matriz2 = LeerMatriz(filas2, columnas2);

            double[,] resultado = null;

            /* Realizar la operación solicitada (suma, resta o multiplicación) */
            if (operacion == "suma")
            {
                /* Verificar que las dimensiones sean iguales para poder sumar */
                if (filas1 != filas2 || columnas1 != columnas2)
                {
                    Console.WriteLine("Las matrices deben tener las mismas dimensiones para la suma.");
                    return null;
                }
                resultado = SumarMatrices(matriz1, matriz2);
            }
            else if (operacion == "resta")
            {
                /* Verificar que las dimensiones sean iguales para poder restar */
                if (filas1 != filas2 || columnas1 != columnas2)
                {
                    Console.WriteLine("Las matrices deben tener las mismas dimensiones para la resta.");
                    return null;
                }
                resultado = RestarMatrices(matriz1, matriz2);
            }
            else if (operacion == "multiplicación")
            {
                /* Verificar que las matrices se puedan multiplicar */
                if (columnas1 != filas2)
                {
                    Console.WriteLine("El número de columnas de la primera matriz debe ser igual al número de filas de la segunda matriz.");
                    return null;
                }
                resultado = MultiplicarMatrices(matriz1, matriz2);
            }

            Console.WriteLine($"Resultado de la {operacion}:");
            MostrarMatriz(resultado);

            return resultado;
        }
    }
}
