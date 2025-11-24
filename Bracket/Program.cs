using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bracket
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Введите строку из скобок: ");
            string input = Console.ReadLine();

            if (string.IsNullOrEmpty(input))
            {
                input = "(()(()))";
            }

            bool isCorrect = IsValidBracketExpression(input, out int maximumNestingDepth);

            if (isCorrect)
            {
                Console.WriteLine($"Строка \"{input}\" корректна. Максимальная глубина вложенности: {maximumNestingDepth}");
            }
            else
            {
                Console.WriteLine($"Строка \"{input}\" некорректна.");
            }
        }

        static bool IsValidBracketExpression(string bracketSequence, out int maxDepth)
        {
            const char OpeningBracket = '(';
            const char ClosingBracket = ')';

            int currentDepth = 0;
            maxDepth = 0;

            for (int i = 0; i < bracketSequence.Length; i++)
            {
                char currentSymbol = bracketSequence[i];

                if (currentSymbol == OpeningBracket)
                {
                    currentDepth++;
                    if (currentDepth > maxDepth)
                    {
                        maxDepth = currentDepth;
                    }

                }
                else if (currentSymbol == ClosingBracket)
                {
                    currentDepth--;
                    if (currentDepth < 0)
                    {
                        return false;
                    }

                }
            }

            return currentDepth == 0;
        }
    }
}
