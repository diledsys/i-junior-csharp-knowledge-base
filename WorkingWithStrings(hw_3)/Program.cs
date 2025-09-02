
using System.Runtime.CompilerServices;

namespace WorkingWithStrings
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string inp; // вспомогательная переменная для ввода данных

            Console.WriteLine("-= Опрос пользователя =-");

            Console.Write("Введите имя: ");
            string firstName =  Console.ReadLine() ?? "";

            Console.Write("Введите фомилию: ");
            string lastName = Console.ReadLine() ?? "";

            Console.Write("Введите возраст: ");
            inp = Console.ReadLine() ?? "";
            int age = inp == "" ? 0 : Convert.ToInt32(inp); // проверяем был ли ввод если нет то делаем 0 

            Console.Write("Введите вес: ");
            inp = Console.ReadLine() ?? "";                 //проверяем был ли ввод если нет то делаем пусто
            int weight = inp == "" ? 0 : Convert.ToInt32(inp);

            Console.Write("Введите рост: ");
            inp = Console.ReadLine() ?? "";
            double height = inp == "" ? 0 : Convert.ToInt32(inp);

            Console.Write("Введите номер телефона: ");
            string phoneNumber = Console.ReadLine() ?? "";

            Console.Write("Введите место работы: ");
            string work = Console.ReadLine() ?? "";

            Console.Write("Введите знак зодиака: ");
            string zodiacSign = Console.ReadLine() ?? "doesn't want to talk";

            // Создаём объект Person
            Person user = new Person(firstName, lastName, age, weight, height, phoneNumber, work, zodiacSign);
           
            user.GetDescription();
        }

    }
    class Person    // класс персона 
    {
        // иницилизируем переменные
        public string FirstName;
        public string LastName;
        public int Age;
        public int Weight;
        public double Height;
        public string PhoneNumber;
        public string Work;
        public string ZodiacSign;

        // конструктор
        public Person(string firstName, string lastName, int age, int weight, double height, string phoneNumber, string work, string zodiacSign)
        {
            FirstName = firstName;
            LastName = lastName;
            Age = age;
            Weight = weight;
            Height = height;
            PhoneNumber = phoneNumber;
            Work = work;
            ZodiacSign = zodiacSign;
        }

        // метод выводит информацию о персоне
        public void GetDescription()
        {
            Console.WriteLine("---Характеристика пользователя--");
            Console.WriteLine($"Имя: {FirstName} {LastName}");
            Console.WriteLine($"Возраст: {Age} лет");
            Console.WriteLine($"Вес: {Weight} кг");
            Console.WriteLine($"Рост: {Height} см");
            Console.WriteLine($"Телефон: {PhoneNumber}");
            Console.WriteLine($"Работа: {Work}");
            Console.WriteLine($"Знак зодиака: {ZodiacSign}");

        }

    }
   



}
