
// Даны две переменные. Поменять местами значения двух переменных. Вывести на экран значения переменных до перестановки и после.
//
// Два примера.
// 1.Есть две переменные имя и фамилия, они сразу инициализированные, но данные не верные, перепутанные. Вот эти данные и надо поменять местами через код.
// 2. Есть две чашки, в одном кофе, во втором чай. Вам надо поменять местами содержимое чашек

namespace PermutationOfValues
{
    internal class Program
    {
        static void Main(string[] args)
        {
            String firstName = "Ivanov";
            String lastName = "Valera";

            Console.WriteLine($"До:  firstName = {firstName}, lastName = {lastName}");

            (firstName, lastName) = (lastName, firstName); // кортежный метод

            Console.WriteLine($"После: firstName = {firstName}, lastName = {lastName}");

            //-------------------------------------- меняем чай, на кофе через пустую переменную ---------------------------------------------

            string cup1 = "кофе";
            string cup2 = "чай";
            

            Console.WriteLine($"До:  cup1 = {cup1}, cup2 = {cup2}");

            string emptyCup = cup1; // пустая чашка
            cup1 = cup2;
            cup2 = emptyCup;

            Console.WriteLine($"После: cup1 = {cup1}, cup2 = {cup2}");

        }
    }
}