using System;
using System.Collections.Generic;

namespace ExplanatoryDictionary
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, string> dictionary = CreateDictionary();
            RunDictionary(dictionary);
        }

        // Метод для создания и заполнения словаря
        static Dictionary<string, string> CreateDictionary()
        {
            return new Dictionary<string, string>
            {
                ["Алгоритм"] = "Конечная последовательность шагов для решения задачи.",
                ["Переменная"] = "Именованная область памяти для хранения изменяемого значения.",
                ["Функция"] = "Блок кода (метод), который можно вызывать по имени; может иметь параметры и результат.",
                ["Класс"] = "Шаблон (тип) для создания объектов: описывает состояние (свойства) и поведение (методы).",
                ["Объект"] = "Экземпляр класса с конкретными значениями свойств.",
                ["Интерфейс"] = "Контракт, задающий набор членов без реализации."
            };
        }

        // Метод для взаимодействия с пользователем
        static void RunDictionary(Dictionary<string, string> dictionary)
        {
            bool isRunning = true;

            while (isRunning)
            {
                Console.Write("Введите слово: ");
                string word = Console.ReadLine();

                if (dictionary.TryGetValue(word, out string definition))
                {
                    Console.WriteLine(definition);
                }
                else
                {
                    Console.WriteLine($"Нет определения для слова: {word}");
                    isRunning = false;
                }
            }
        }
    }
}
