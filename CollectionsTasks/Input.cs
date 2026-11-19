using System;
using System.Collections.Generic;
using System.IO;

namespace Lab4.CollectionsTasks;

internal static class Input
{
    internal static string ReadParticipant(int index)
    {
        var name = string.Empty;
        var total = 0;
        var line = ReadLine($"Участник {index}: ");

        while (!TryParseParticipant(line, out name, out total))
        {
            Console.WriteLine(
                "Нужны фамилия (до 20), имя (до 15)");
            Console.WriteLine("Фамилия и имя: только буквы.");
            Console.WriteLine(
                "и 4 целых балла от 0 до 10.");
            line = ReadLine("Повторите запись: ");
        }

        return line;
    }

    internal static bool TryParseParticipant(string line,
        out string name, out int total)
    {
        var parts = line.Split(' ');
        var index = 0;
        var score = 0;
        name = string.Empty;
        total = 0;

        if (parts.Length != 6
            || !IsName(parts[0], 20)
            || !IsName(parts[1], 15))
        {
            return false;
        }

        for (index = 2; index < parts.Length; index++)
        {
            if (!int.TryParse(parts[index], out score)
                || score < 0 || score > 10)
            {
                return false;
            }

            total += score;
        }

        name = $"{parts[0]} {parts[1]}";
        return true;
    }

    private static bool IsName(string text, int maxLength)
    {
        var index = 0;

        if (text.Length == 0 || text.Length > maxLength)
        {
            return false;
        }

        for (index = 0; index < text.Length; index++)
        {
            if (!char.IsLetter(text[index]))
            {
                return false;
            }
        }

        return true;
    }

    internal static List<string> ReadList(string name, bool sorted)
    {
        return ReadList<string>(name, sorted);
    }

    internal static List<T> ReadList<T>(string name, bool sorted)
    {
        var count = ReadInt(
            $"Число элементов {name} (0–10000): ", 0, 10000);
        var values = new List<T>(count);
        var index = 0;
        var text = string.Empty;
        T? value = default;

        for (index = 0; index < count; index++)
        {
            text = ReadLine($"{name}[{index + 1}]: ");

            if (!TryParseElement(text, out value)
                || value is null)
            {
                Console.WriteLine("Некорректный элемент.");
                index--;
                continue;
            }

            if (sorted && index > 0
                && Comparer<T>.Default.Compare(
                    value, values[index - 1]) < 0)
            {
                Console.WriteLine(
                    "Элемент меньше предыдущего. Повторите ввод.");
                index--;
                continue;
            }

            values.Add(value);
        }

        return values;
    }

    private static bool TryParseElement<T>(string text,
        out T? value)
    {
        var number = 0;
        var symbol = '\0';
        value = default;

        if (typeof(T) == typeof(int)
            && int.TryParse(text, out number))
        {
            value = (T)(object)number;
            return true;
        }

        if (typeof(T) == typeof(char)
            && char.TryParse(text, out symbol))
        {
            value = (T)(object)symbol;
            return true;
        }

        if (typeof(T) == typeof(string)
            && !string.IsNullOrWhiteSpace(text))
        {
            value = (T)(object)text;
            return true;
        }

        return false;
    }

    internal static string ReadText(string prompt)
    {
        var text = ReadLine(prompt).Trim();

        while (text.Length == 0)
        {
            Console.WriteLine("Введите непустое название.");
            text = ReadLine(prompt).Trim();
        }

        return text;
    }

    internal static int ReadInt(string prompt, int minimum,
        int maximum)
    {
        var value = 0;

        while (!int.TryParse(ReadLine(prompt), out value)
            || value < minimum || value > maximum)
        {
            Console.WriteLine(
                $"Введите целое от {minimum} до {maximum}.");
        }

        return value;
    }

    internal static string ReadLine(string prompt)
    {
        Console.Write(prompt);
        return Console.ReadLine()
            ?? throw new EndOfStreamException();
    }
}
