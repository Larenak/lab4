using System;
using System.Globalization;
using System.IO;

namespace Lab4.PointTasks;

internal static class Input
{
    internal static Point ReadPoint(string name)
    {
        Console.WriteLine($"Точка {name}:");
        var x = ReadDouble("X: ");
        var y = ReadDouble("Y: ");
        return new Point(x, y);
    }

    internal static double ReadDouble(string prompt)
    {
        double value = 0;
        var text = ReadLine(prompt).Replace(',', '.');

        while (!double.TryParse(text, NumberStyles.Float,
            CultureInfo.InvariantCulture, out value)
            || !double.IsFinite(value))
        {
            Console.WriteLine(
                "Введите конечное число, например 2,5.");
            text = ReadLine(prompt).Replace(',', '.');
        }

        return value;
    }

    internal static int ReadInt(string prompt)
    {
        var value = 0;

        while (!int.TryParse(ReadLine(prompt), out value))
        {
            Console.WriteLine("Введите целое число типа int.");
        }

        return value;
    }

    private static string ReadLine(string prompt)
    {
        Console.Write(prompt);
        return Console.ReadLine()
            ?? throw new EndOfStreamException();
    }
}
