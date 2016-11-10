using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    class Program
    {
        static void Main(string[] args)
        {
            CalculatorOp calc = new CalculatorOp();
            
            Console.WriteLine("Addition:");
            Console.WriteLine(calc.Addition(2,7));
            Console.WriteLine("Multiplication");
            Console.WriteLine(calc.Addition(2,7));
            Console.ReadLine();

        }
    }
}
