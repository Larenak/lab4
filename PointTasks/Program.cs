using System;
using System.Globalization;
using System.IO;
using System.Text;

namespace Lab4.PointTasks;

internal static class Program
{
    private static void Main()
    {
        Console.InputEncoding = Encoding.UTF8;
        Console.OutputEncoding = Encoding.UTF8;
        CultureInfo.CurrentCulture =
            CultureInfo.GetCultureInfo("ru-RU");

        try
        {
            RunMenu();
        }
        catch (EndOfStreamException)
        {
            Console.WriteLine("\nВвод завершён.");
        }
    }

    private static void RunMenu()
    {
        var choice = 0;

        while (true)
        {
            Console.WriteLine("\nКласс Point. Вариант 3");
            Console.WriteLine("6 — Конструкторы и расстояние");
            Console.WriteLine("7 — Перегруженные операции");
            Console.WriteLine("0 — Выход");
            choice = Input.ReadInt("Выберите задание: ");

            switch (choice)
            {
                case 0:
                    return;
                case 6:
                    PointExamples.RunTask6();
                    break;
                case 7:
                    PointExamples.RunTask7();
                    break;
                default:
                    Console.WriteLine("Выберите 0, 6 или 7.");
                    break;
            }
        }
    }
}
