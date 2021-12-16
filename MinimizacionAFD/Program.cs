using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinimizacionAFD
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Transiciones> transiciones = new List<Transiciones>();
            int i = 0;
            char terminar;
            bool llenar = true;
            Console.WriteLine("Ingrese el automata a minimizar, solo se admiten tamaños de un caracter.");
            do
            {
                transiciones.Add(new Transiciones());
                Console.WriteLine("Ingrese el estado actual: ");
                transiciones[i].estadoInicio = Convert.ToChar(Console.ReadLine());
                Console.WriteLine("Ingrese el simbolo de transicion: ");
                transiciones[i].caracMovimiento = Convert.ToChar(Console.ReadLine());
                Console.WriteLine("Ingrese el estado siguiente: ");
                transiciones[i].estadoSiguiente = Convert.ToChar(Console.ReadLine());
                do
                {
                    Console.WriteLine("¿Añadir otra transicion? S/N");
                    terminar = Convert.ToChar(Console.ReadLine());

                    if (char.ToUpper(terminar) == 'S')
                    {
                        llenar = false;
                        break;

                    }
                    else
                    {
                        if (char.ToUpper(terminar) != 'N')
                        {
                            Console.WriteLine("Entrada no válida");
                        }
                    }
                } while (llenar);

            } while (true);
        }
    }
}
