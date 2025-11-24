using System;

class Program
{
    static void Main(string[] args)
    {
        int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

        Console.WriteLine("До перемешивания:");
        Console.WriteLine(string.Join(", ", numbers));

        Shuffle(numbers);

        Console.WriteLine("После перемешивания:");
        Console.WriteLine(string.Join(", ", numbers));
    }

    public static void Shuffle(int[] array)
    {
        Random rng = new Random();

        for (int i = array.Length - 1; i > 0; i--)
        {
            int j = rng.Next(i + 1);
            (array[i], array[j]) = (array[j], array[i]);
        }
    }
}
