namespace ImageAlbumLayout
{
    class Program
    {
        static void Main(string[] args)
        {
            const int MinutesPerPatient = 10;
            const int MinutesPerHour = 60;

            Console.Write("Введите кол-во пациентов: ");
            int patientCount = Convert.ToInt32(Console.ReadLine());

            int totalMinutes = patientCount * MinutesPerPatient;
            int hours = totalMinutes / MinutesPerHour;
            int remainingMinutes = totalMinutes % MinutesPerHour;

            Console.WriteLine($"Вы должны отстоять в очереди {hours} часа и {remainingMinutes} минут.");
        }
    }
}