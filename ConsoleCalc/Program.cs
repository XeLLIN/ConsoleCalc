using System;

namespace ConsoleCalc
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите выражение: ");
            Expression expression = new Expression(Console.ReadLine());
            Console.WriteLine("Ответ: " + expression.FindSolve());
        }
    }
}
