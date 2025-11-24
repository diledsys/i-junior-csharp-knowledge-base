using System;
using NAudio.CoreAudioApi;

namespace BarHealth
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int value = 10;
            int maxValue = 100;
            int position = 0;
            const ConsoleColor Red = ConsoleColor.Red;
            const ConsoleColor Green = ConsoleColor.Green;

            while (true)
            {
                var deviceEnumerator = new MMDeviceEnumerator();
                var device = deviceEnumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);

                float volume = device.AudioEndpointVolume.MasterVolumeLevelScalar;
                bool isMuted = device.AudioEndpointVolume.Mute;

                BarHalth(Convert.ToInt32(volume*100), 0, maxValue, Red);
                //Console.WriteLine($"Громкость: {volume}");
                //Console.Clear();
                //Console.WriteLine($"Отключен звук: {isMuted}");
            }

            BarHalth(value, 0, maxValue, Red);
            Console.ReadKey();
            BarHalth(5, 0, maxValue, Red);
            Console.ReadKey();
        }

        static void BarHalth(int value, int position, int maxValue, ConsoleColor color)
        {
            Console.CursorVisible = false;
            ConsoleColor defaultColor = Console.BackgroundColor;

            Console.SetCursorPosition(0, position);
            Console.Write("[");

            Console.BackgroundColor = color;

            for (int i = 1; i <= value+1; i++)
            {
                Console.SetCursorPosition(i, position);
                Console.Write(" ");
            }

            Console.BackgroundColor = defaultColor;

            for (int i = value+1; i <= maxValue; i++)
            {
                Console.SetCursorPosition(i, position);
                Console.Write(" ");
            }

            Console.SetCursorPosition(maxValue, position);
            Console.Write("]");
        }
    }
