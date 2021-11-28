using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practica_2
{
    class AFN
    {
        public List<int> estados;
        public List<Transiciones> transiciones;
        public int estadoFinal;

        public AFN()
        {
            this.estados = new List<int>();
            this.transiciones = new List<Transiciones>();
            this.estadoFinal = 0;
        }
        public AFN(int tamaño)
        {
            this.estados = new List<int>();
            this.transiciones = new List<Transiciones>();
            this.estadoFinal = 0;
            this.tamanioEstado(tamaño);
        }
        public AFN(char c)
        {
            this.estados = new List<int>();
            this.transiciones = new List<Transiciones>();
            this.tamanioEstado(2);
            this.estadoFinal = 1;
            this.transiciones.Add(new Transiciones(0, 1, c));
        }

        public void tamanioEstado(int size)
        {
            for (int i = 0; i < size; i++)
                this.estados.Add(i);
        }

        public void mostrar()
        {
            foreach (Transiciones t in transiciones)
            {
                Console.WriteLine("(" + t.estadoE + ", " + t.simbTrans +
                    ", " + t.estadoS + ")");
            }
           
        }
    }
}
