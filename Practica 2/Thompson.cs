using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practica_2
{
    class Thompson
    {
        Stack<char> operadores = new Stack<char>();
        Stack<AFN> operandos = new Stack<AFN>();
        Stack<AFN> concatenarPila = new Stack<AFN>();
        bool concatenar = false;
        char op, c;
        int parentesis = 0;
        AFN afn1, afn2;

        public AFN kleene(AFN n)
        {
            AFN result = new AFN(n.estados.Count + 2);
            result.transiciones.Add(new Transiciones(0, 1, 'E'));

            foreach (Transiciones t in n.transiciones)
            {
                result.transiciones.Add(new Transiciones(t.estadoE + 1,
                t.estadoS + 1, t.simbTrans));
            }

            result.transiciones.Add(new Transiciones(n.estados.Count,
                n.estados.Count + 1, 'E'));

            result.transiciones.Add(new Transiciones(n.estados.Count, 1, 'E'));

            result.transiciones.Add(new Transiciones(0, n.estados.Count + 1, 'E'));

            result.estadoFinal = n.estados.Count + 1;
            return result;
        }

        public AFN concatenarAFN(AFN n, AFN m)
        {
            m.estados.Remove(0);

            foreach (Transiciones t in m.transiciones)
            {
                n.transiciones.Add(new Transiciones(t.estadoE + n.estados.Count - 1,
                    t.estadoS + n.estados.Count - 1, t.simbTrans));
            }

            foreach (int s in m.estados)
            {
                n.estados.Add(s + n.estados.Count + 1);
            }

            n.estadoFinal = n.estados.Count + m.estados.Count - 2;
            return n;
        }

        public static AFN union(AFN n, AFN m)
        {
            AFN resultado = new AFN(n.estados.Count + m.estados.Count + 2);

            resultado.transiciones.Add(new Transiciones(0, 1, 'E'));

            foreach (Transiciones t in n.transiciones)
            {
                resultado.transiciones.Add(new Transiciones(t.estadoE + 1,
                t.estadoS + 1, t.simbTrans));
            }

            resultado.transiciones.Add(new Transiciones(n.estados.Count,
                n.estados.Count + m.estados.Count + 1, 'E'));

            resultado.transiciones.Add(new Transiciones(0, n.estados.Count + 1, 'E'));

            foreach (Transiciones t in m.transiciones)
            {
                resultado.transiciones.Add(new Transiciones(t.estadoE + n.estados.Count
                    + 1, t.estadoS + n.estados.Count + 1, t.simbTrans));
            }

            resultado.transiciones.Add(new Transiciones(m.estados.Count + n.estados.Count,
                n.estados.Count + m.estados.Count + 1, 'E'));

            resultado.estadoFinal = n.estados.Count + m.estados.Count + 1;
            return resultado;
        }
        public bool alfa(char c)
        { 
            return c >= 'a' && c <= 'z'; 
        }
        public bool alfabeto(char c) 
        { 
            return alfa(c) || c == 'E'; 
        }
        public bool operadorRegex(char c)
        {
            return c == '(' || c == ')' || c == '*' || c == '+';
        }

        public AFN compilar(String regex)
        {

            for (int i = 0; i < regex.Length; i++)
            {
                c = regex[i];
                if (alfabeto(c))
                {
                    operandos.Push(new AFN(c));
                    if (concatenar)
                    { 
                        operadores.Push('.'); // '.' es usado para la concatenacion.
                    }
                    else
                        concatenar = true;
                }
                else
                {
                    if (c == ')')
                    {
                        concatenar = false;
                        if (parentesis == 0)
                        {
                            Console.WriteLine("Error: Parentesis incorrectos");
                            Environment.Exit(0);
                        }
                        else { parentesis--; }
                        while (!(operadores.Count == 0) && operadores.Peek() != '(')
                        {
                            op = operadores.Pop();
                            if (op == '.')
                            {
                                afn2 = operandos.Pop();
                                afn1 = operandos.Pop();
                                operandos.Push(concatenarAFN(afn1, afn2));
                            }
                            else if (op == '+')
                            {
                                afn2 = operandos.Pop();

                                if (!(operadores.Count == 0) &&
                                    operadores.Peek() == '.')
                                {

                                    concatenarPila.Push(operandos.Pop());
                                    while (!(operadores.Count == 0) &&
                                        operadores.Peek() == '.')
                                    {

                                        concatenarPila.Push(operandos.Pop());
                                        operadores.Pop();
                                    }
                                    afn1 = concatenarAFN(concatenarPila.Pop(),
                                        concatenarPila.Pop());
                                    while (concatenarPila.Count > 0)
                                    {
                                        afn1 = concatenarAFN(afn1, concatenarPila.Pop());
                                    }
                                }
                                else
                                {
                                    afn1 = operandos.Pop();
                                }
                                operandos.Push(union(afn1, afn2));
                            }
                        }
                    }
                    else if (c == '*')
                    {
                        operandos.Push(kleene(operandos.Pop()));
                        concatenar = true;
                    }
                    else if (c == '(')
                    { 
                        operadores.Push(c);
                        parentesis++;
                    }
                    else if (c == '+')
                    {
                        operadores.Push(c);
                        concatenar = false;
                    }
                }
            }
            while (operadores.Count > 0)
            {
                if (operandos.Count == 0)
                {
                    Console.WriteLine("Error: imbalanace in operands and "
                    + "operators");
                    Environment.Exit(1);
                }
                op = operadores.Pop();
                if (op == '.')
                {
                    afn2 = operandos.Pop();
                    afn1 = operandos.Pop();
                    operandos.Push(concatenarAFN(afn1, afn2));
                }
                else if (op == '+')
                {
                    afn2 = operandos.Pop();
                    if (operadores.Count != 0 && operadores.Peek() == '.')
                    {
                        concatenarPila.Push(operandos.Pop());
                        while (operadores.Count != 0 && operadores.Peek() == '.')
                        {
                            concatenarPila.Push(operandos.Pop());
                            operadores.Pop();
                        }
                        afn1 = concatenarAFN(concatenarPila.Pop(),
                            concatenarPila.Pop());
                        while (concatenarPila.Count > 0)
                        {
                            afn1 = concatenarAFN(afn1, concatenarPila.Pop());
                        }
                    }
                    else
                    {
                        afn1 = operandos.Pop();
                    }
                    operandos.Push(union(afn1, afn2));
                }
            }
            return operandos.Pop();
        }
    }
}
