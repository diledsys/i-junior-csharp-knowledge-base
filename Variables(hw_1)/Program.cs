using System.Numerics;
using System.Text;

namespace Exit_Control
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Значемые перемене
            int age = 30;                                   // целое число
            double weight = 72.5;                           // число с плавающй точкой
            decimal price = 199.99m;                        // денежные расчеты
            bool isActive = true;                           // буливо значение
            char singleCharacterhter = 'S';                 // одиночный символ
            float temperature = 36.6f;                      // число с плавающей точкой
            long populationPlanetPeople = 8_000_000L;       // длинное целое
            byte level = 3;                                 // уровень (0..255)

            // Ссылочные переменные
            String firstName = "Hromov";                    // строка
            String lastName = "Dmitry";                     // строка

            // Вывод в консоль значений
            Console.WriteLine($"Significant changes \n--------------------\nage: {age} \nweight: {weight}\nprice: {price}\nisActive: {isActive}\nisActive: {isActive}" +
                $"\nsingleCharacterhter: {singleCharacterhter}\ntemperature: {temperature}\npopulationPlanetPeople: {populationPlanetPeople}" +
                $"\nlevel: {level}\n\nReference variablesn\n--------------------\nfirstName: {firstName}\nlastName: {lastName}");



        }
    }
}
