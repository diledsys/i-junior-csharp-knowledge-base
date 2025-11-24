using System;

class Program
{
    static void Main()
    {
        Console.CursorVisible = false;
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        ConsoleKey moveUpKey = ConsoleKey.W;
        ConsoleKey moveDownKey = ConsoleKey.S;
        ConsoleKey moveLeftKey = ConsoleKey.A;
        ConsoleKey moveRightKey = ConsoleKey.D;
        ConsoleKey exitKey = ConsoleKey.Escape;

        char[,] map = GetMap();

        int playerX = 1;
        int playerY = 1;

        DrawFullMap(map);
        DrawPlayer(playerX, playerY);
        ShowHelp(moveUpKey, moveLeftKey, moveDownKey, moveRightKey, exitKey);

        bool isRunning = true;

        while (isRunning)
        {
            ConsoleKey pressedKey = Console.ReadKey(true).Key;


            GetDirectionFromInput(pressedKey, out int directionX, out int directionY,
                                  moveUpKey, moveDownKey, moveLeftKey, moveRightKey);

            if (pressedKey == exitKey)
            {
                isRunning = false;
                continue;
            }

            int targetX = playerX + directionX;
            int targetY = playerY + directionY;

            if (IsValidMove(map, targetX, targetY))
            {
                MovePlayer(map, ref playerX, ref playerY, targetX, targetY);
                CheckForTrap(map, playerX, playerY);
            }
        }

        Console.SetCursorPosition(0, map.GetLength(0) + 1);
        Console.CursorVisible = true;
    }

    static char[,] GetMap()
    {
        return new char[,]
        {
            { '#', '#', '#', '#', '#', '#', '#', '#', '#', '#' },
            { '#', '.', '.', '.', '.', '.', '.', '.', '.', '#' },
            { '#', '.', '#', '#', '.', '#', '#', '#', '.', '#' },
            { '#', '.', '.', '.', '^', '.', '.', '#', '.', '#' },
            { '#', '#', '#', '#', '.', '#', '.', '#', '.', '#' },
            { '#', '.', '.', '#', '.', '^', '.', '#', '.', '#' },
            { '#', '.', '.', '.', '.', '.', '.', '#', '.', '#' },
            { '#', '.', '#', '#', '#', '#', '.', '#', '.', '#' },
            { '#', '^', '.', '.', '.', '.', '.', '.', '.', '#' },
            { '#', '#', '#', '#', '#', '#', '#', '#', '#', '#' },
        };
    }

    static void ShowHelp(ConsoleKey up, ConsoleKey left, ConsoleKey down, ConsoleKey right, ConsoleKey exit)
    {
        Console.SetCursorPosition(0, 10);
        Console.Write($"Управление: {up}(up), {left}(left), {down}(down), {right}(right). {exit} - выход.");
    }

    static void DrawFullMap(char[,] map)
    {
        int totalRows = map.GetLength(0);
        int totalColumns = map.GetLength(1);

        for (int row = 0; row < totalRows; row++)
        {
            for (int column = 0; column < totalColumns; column++)
            {
                Console.SetCursorPosition(column, row);
                Console.Write(map[row, column]);
            }
        }
    }

    static void DrawPlayer(int axesX, int axesY)
    {
        Console.SetCursorPosition(axesX, axesY);
        Console.Write('@');
    }

    static void GetDirectionFromInput(ConsoleKey key, out int directionX, out int directionY,
                                      ConsoleKey up,
                                      ConsoleKey down,
                                      ConsoleKey left,
                                      ConsoleKey right
        )
    {
        directionX = 0;
        directionY = 0;

        if (key == up)
        {
            directionY = -1;
        }
        else if (key == down)
        {
            directionY = 1;
        }
        else if (key == left)
        {
            directionX = -1;
        }
        else if (key == right)
        {
            directionX = 1;
        }
    }

    static void MovePlayer(char[,] map, ref int currentX, ref int currentY, int newX, int newY)
    {
        Console.SetCursorPosition(currentX, currentY);
        Console.Write(map[currentY, currentX]);

        currentX = newX;
        currentY = newY;

        DrawPlayer(currentX, currentY);
    }

    static bool IsValidMove(char[,] map, int axesX, int axesY)
    {
        return axesX >= 0 && axesY >= 0 &&
               axesY < map.GetLength(0) &&
               axesX < map.GetLength(1) &&
               map[axesY, axesX] != '#';
    }

    static void CheckForTrap(char[,] map, int playerX, int playerY)
    {
        Console.SetCursorPosition(0, map.GetLength(0) + 1);

        if (map[playerY, playerX] == '^')
        {
            Console.Write("BOOM! Вы наступили на ловушку!               ");
            TriggerTrapEffect();
        }
        else
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Write(new string(' ', 40));
        }
    }

    static void TriggerTrapEffect()
    {
        Console.BackgroundColor = ConsoleColor.Red;
    }
}
