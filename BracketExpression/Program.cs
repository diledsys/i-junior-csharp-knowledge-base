            //             Дана строка из символов '(' и ')'.Определить, является ли она корректным скобочным выражением. Определить максимальную глубину вложенности скобок.
            // Текущая глубина равняется разности открывающихся и закрывающихся скобок в момент подсчета каждого символа.
            // К символу в строке можно обратиться по индексу
            // Пример “( ()(()) )” -строка корректная и максимум глубины равняется 3.
            // Пример некорректных строк:
            //             "(()", "())", ")(", "(()))(()"
using System;

namespace BracketExpression
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string bracketExpression = "(()(())())"; //  Пример некорректных строк: "(()\", \"())\", \")(\", \"(()))(()"
            int currentDepth = 0;
            int maxDepth = 0;
            bool isBalanced = true;

            foreach (char c in bracketExpression)
            {
                if (c == '(')
                {
                    currentDepth++;
                    if (currentDepth > maxDepth)
                    {
                        maxDepth = currentDepth;
                    }
                }
                else if (c == ')')
                {
                    currentDepth--;
                    if (currentDepth < 0)
                    {
                        isBalanced = false;
                        break;
                    }
                }
            }
            if (currentDepth != 0)
            {
                isBalanced = false;
            }
            if (isBalanced)
            {
                Console.WriteLine($"Строка корректна. Максимальная глубина вложенности: {maxDepth}");
            }
            else
            {
                Console.WriteLine("Строка некорректна.");
            }


        }
    }
}
