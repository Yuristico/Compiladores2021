using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practica_2
{
    class Program
    {
        static void Main(string[] args)
        {
            string expresion;
            Console.WriteLine("Ingrese la expresión regular.\nInstrucciones: " +
                "Alfabeto ['a','z'] & E para epsilon \n* para cerradura de Kleene " +
                "\nelementos sin nada entre ellos se considera concatenacion " +
                "\n+ para union");
            expresion = Console.ReadLine();
            Thompson thompson = new Thompson();
            AFN afn = thompson.compilar(expresion);
            Console.WriteLine("AFN:");
            afn.mostrar();
            Console.ReadKey();
        }
    }
}
