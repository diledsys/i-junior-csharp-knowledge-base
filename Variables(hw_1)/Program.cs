
namespace Exit_Control
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Значемые перемене (value)
            int age = 30;                                   // целое число
            double weight = 72.5;                           // число с плавающй точкой
            decimal price = 199.99m;                        // денежные расчеты
            bool isActive = true;                           // буливо значение
            char singleCharacterhter = 'S';                 // одиночный символ
            float temperature = 36.6f;                      // число с плавающей точкой
            long populationPlanetPeople = 8_000_000L;       // длинное целое
            byte level = 3;                                 // уровень (0..255)

            // Ссылочные переменные (reference)
            String firstName = "Hromov";                    // строка
            String lastName = "Dmitry";                     // строка

            // Вывод в консоль значение
            Console.WriteLine($"Significant changes \n--------------------\n" +
                $"age: {age}" +
                $"\nweight: {weight}\n" +
                $"price: {price}\n" +
                $"isActive: {isActive}\n" +
                $"isActive: {isActive}\n" +
                $"singleCharacterhter: {singleCharacterhter}\n" +
                $"temperature: {temperature}\n" +
                $"populationPlanetPeople: {populationPlanetPeople}" +
                $"\nlevel: {level}" +
                $"\n\nReference variablesn\n--------------------\n" +
                $"firstName: {firstName}\nlastName: {lastName}");



        }
    }
}
