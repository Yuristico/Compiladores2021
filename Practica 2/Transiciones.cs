using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practica_2
{
    class Transiciones
    {
        public int estadoE;
        public int estadoS;
        public char simbTrans;
        public Transiciones(int v1, int v2, char sym)
        {
            this.estadoE = v1;
            this.estadoS = v2;
            this.simbTrans = sym;
        }
    }
}
