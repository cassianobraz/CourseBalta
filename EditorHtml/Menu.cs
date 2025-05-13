public static class Menu
{
    public static void Show()
    {
        Console.clear();
        Console.BackgroundColor = ConsoleColor.Blue;
        Console.ForegroundColor = ConsoleColor.Black;

        DrawScreen();
        WriteOptions();

        var option = short.Parse(Console.ReadLine());
    }

    public static void DrawScreen()
    {
        Console.write("+");
        for (int i = 0; i <= 30; i++)
            Console.write("-");

        Console.write("+");
        Console.write("\n");

        for (int lines = 0; lines <= 10; lines++)
        {
            Console.Write("|");
            for (int i = 0; i <= 30; i++)
                Console.write(" ");

            Console.Write("|");
            Console.write("\n");
        }

        Console.write("+");
        for (int i = 0; i <= 30; i++)
            Console.write("-");

        Console.write("+");
        Console.write("\n");
    }

    public static void WriteOptions()
    {
        Console.SetCursorPosition(3, 2);
        Console.WriteLine("Editor HTML");
        Console.SetCursorPosition(3, 3);
        Console.WriteLine("================");
        Console.SetCursorPosition(3, 4);
        Console.WriteLine("Selecione uma opção abaixo");
        Console.SetCursorPosition(3, 6);
        Console.WriteLine("1 - Novo arquivo");
        Console.SetCursorPosition(3, 7);
        Console.WriteLine("2 - Abrir");
        Console.SetCursorPosition(3, 9);
        Console.WriteLine("0 - Sair");
        Console.SetCursorPosition(3, 10);
        Console.Write("Opção: ");
    }
}


