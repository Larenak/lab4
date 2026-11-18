using System;

namespace Lab4.PointTasks;

internal static class PointExamples
{
    internal static void RunTask6()
    {
        var origin = new Point();
        Console.WriteLine(
            $"Конструктор без параметров: {origin}");
        Point first = Input.ReadPoint("P");
        var copy = new Point(first);
        Console.WriteLine($"Конструктор с параметрами: {first}");
        Console.WriteLine($"Конструктор копирования: {copy}");
        copy.X = Input.ReadDouble("Новое X копии: ");
        copy.Y = Input.ReadDouble("Новое Y копии: ");
        Console.WriteLine(
            $"Свойства копии: X={copy.X}; Y={copy.Y}");
        Console.WriteLine($"Исходная точка P: {first}");
        Point second = Input.ReadPoint("Q");
        var distance = first.DistanceTo(second);
        Console.WriteLine($"Расстояние P–Q: {distance:G10}");
    }

    internal static void RunTask7()
    {
        Point first = Input.ReadPoint("P");
        Point second = Input.ReadPoint("Q");
        var number = Input.ReadInt("Целое число для сдвига: ");
        var changed = new Point(first);
        double y = first;

        Console.WriteLine($"P = {first}; Q = {second}");
        Console.WriteLine($"P++ возвращает: {changed++}");
        Console.WriteLine($"После P++: {changed}");
        Console.WriteLine($"--P возвращает: {--changed}");
        Console.WriteLine($"++P возвращает: {++changed}");
        Console.WriteLine($"P-- возвращает: {changed--}");
        Console.WriteLine($"После P--: {changed}");

        try
        {
            Console.WriteLine($"(int)P = {(int)first}");
        }
        catch (OverflowException)
        {
            Console.WriteLine(
                "Целая часть X не помещается в int.");
        }

        Console.WriteLine($"Неявное double: {y}");
        Console.WriteLine($"P + Q = {first + second:G10}");
        Console.WriteLine($"P + {number} = {first + number}");
        Console.WriteLine($"{number} + P = {number + first}");
        Console.WriteLine($"Исходная P: {first}");
    }
}
