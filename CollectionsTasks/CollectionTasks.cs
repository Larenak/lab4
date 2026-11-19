using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace Lab4.CollectionsTasks;

internal static class CollectionTasks
{
    internal static void RunTask1()
    {
        Console.WriteLine("Списки вводятся по возрастанию.");
        Console.WriteLine("Повторяющиеся элементы допустимы.");
        List<string> first = Input.ReadList("L1", true);
        List<string> second = Input.ReadList("L2", true);
        MergeLists(first, second);
        PrintCollection("Результат L1", first);
    }

    private static void MergeLists<T>(List<T> first,
        List<T> second)
    {
        var firstIndex = first.Count - 1;
        var secondIndex = second.Count - 1;
        var resultIndex = 0;

        first.AddRange(second);
        resultIndex = first.Count - 1;

        while (secondIndex >= 0)
        {
            if (firstIndex >= 0
                && Comparer<T>.Default.Compare(
                    first[firstIndex], second[secondIndex]) > 0)
            {
                first[resultIndex] = first[firstIndex];
                firstIndex--;
            }
            else
            {
                first[resultIndex] = second[secondIndex];
                secondIndex--;
            }

            resultIndex--;
        }
    }

    internal static void RunTask2()
    {
        List<string> values = Input.ReadList("L", false);
        var count = CountEqualNeighbors(values);
        PrintCollection("Список L", values);
        Console.WriteLine(
            "Учитываются элементы с двумя соседями.");
        Console.WriteLine($"Количество: {count}");
    }

    private static int CountEqualNeighbors<T>(List<T> values)
    {
        var list = new LinkedList<T>(values);
        LinkedListNode<T>? node = list.First?.Next;
        var count = 0;

        while (node != null && node.Next != null)
        {
            if (object.Equals(node.Previous!.Value,
                node.Next.Value))
            {
                count++;
            }

            node = node.Next;
        }

        return count;
    }

    internal static void RunTask3()
    {
        var dishCount = Input.ReadInt("Число блюд (1–1000): ",
            1, 1000);
        var menu = new HashSet<string>();
        var dish = string.Empty;
        var index = 0;
        var visitor = 0;
        var orderCount = 0;
        var orders = new HashSet<string>();

        for (index = 0; index < dishCount; index++)
        {
            dish = Input.ReadText($"Блюдо {index + 1}: ");

            if (!menu.Add(dish))
            {
                Console.WriteLine("Такое блюдо уже есть.");
                index--;
            }
        }

        var visitorCount = Input.ReadInt(
            "Число посетителей (1–1000): ", 1, 1000);
        var orderedByAll = new HashSet<string>(menu);
        var orderedByAnyone = new HashSet<string>();

        for (visitor = 1; visitor <= visitorCount; visitor++)
        {
            Console.WriteLine($"Посетитель {visitor}.");
            PrintCollection("Меню", menu);
            orderCount = Input.ReadInt("Сколько блюд заказано: ",
                0, dishCount);
            orders.Clear();
            index = 0;

            while (index < orderCount)
            {
                dish = Input.ReadText($"Заказ {index + 1}: ");

                if (!menu.Contains(dish))
                {
                    Console.WriteLine("Блюда нет в меню.");
                }
                else if (!orders.Add(dish))
                {
                    Console.WriteLine("Уже учтено в заказе.");
                }
                else
                {
                    index++;
                }
            }

            orderedByAll.IntersectWith(orders);
            orderedByAnyone.UnionWith(orders);
        }

        var orderedByNobody = new HashSet<string>(menu);
        orderedByNobody.ExceptWith(orderedByAnyone);
        orderedByAnyone.ExceptWith(orderedByAll);

        PrintCollection("Заказывали все", orderedByAll);
        PrintCollection("Некоторые, но не все", orderedByAnyone);
        PrintCollection("Не заказывал никто", orderedByNobody);
    }

    internal static void RunTask4()
    {
        var path = GetDataPath("russian-text.txt");
        FillTextFile(path);
        var text = File.ReadAllText(path);
        MatchCollection words = Regex.Matches(text,
            "[а-яё]+", RegexOptions.IgnoreCase);
        var seen = new HashSet<char>();
        var repeated = new HashSet<char>();
        var wordLetters = new HashSet<char>();
        var consonants = "бвгджзйклмнпрстфхцчшщ";
        var result = new List<char>();
        var word = string.Empty;
        var letter = '\0';
        var wordIndex = 0;
        var letterIndex = 0;

        for (wordIndex = 0; wordIndex < words.Count;
            wordIndex++)
        {
            wordLetters.Clear();
            word = words[wordIndex].Value.ToLower();

            for (letterIndex = 0; letterIndex < word.Length;
                letterIndex++)
            {
                letter = word[letterIndex];

                if (consonants.Contains(letter))
                {
                    if (wordLetters.Add(letter))
                    {
                        if (seen.Contains(letter))
                        {
                            repeated.Add(letter);
                        }
                        else
                        {
                            seen.Add(letter);
                        }
                    }
                }
            }
        }

        seen.ExceptWith(repeated);

        for (letterIndex = 0; letterIndex < consonants.Length;
            letterIndex++)
        {
            if (seen.Contains(consonants[letterIndex]))
            {
                result.Add(consonants[letterIndex]);
            }
        }

        Console.WriteLine($"Файл: {path}");
        PrintCollection("Согласные ровно в одном слове",
            result);
    }

    private static void FillTextFile(string path)
    {
        var count = Input.ReadInt("Число строк (0–1000): ",
            0, 1000);
        var index = 0;
        using var writer = new StreamWriter(path);

        for (index = 1; index <= count; index++)
        {
            writer.WriteLine(
                Input.ReadLine($"Строка {index}: "));
        }
    }

    internal static void RunTask5()
    {
        var path = GetDataPath("competition.txt");
        FillResultsFile(path);
        using var reader = new StreamReader(path);
        var count = int.Parse(reader.ReadLine()!);
        var groups = new Dictionary<int, List<string>>();
        var line = string.Empty;
        var name = string.Empty;
        var total = 0;
        var index = 0;
        List<string>? names = null;
        var selectedCount = 0;
        var requiredCount = Math.Min(3, count);

        for (index = 0; index < count; index++)
        {
            line = reader.ReadLine()!;

            if (!Input.TryParseParticipant(line, out name,
                out total))
            {
                throw new InvalidDataException(
                    "Некорректная запись участника.");
            }

            if (!groups.TryGetValue(total, out names))
            {
                names = new List<string>();
                groups.Add(total, names);
            }

            names.Add(name);
        }

        Console.WriteLine($"Файл: {path}");
        Console.WriteLine("Лучшие участники:");

        for (total = 40; total >= 0; total--)
        {
            if (groups.TryGetValue(total, out names))
            {
                for (index = 0; index < names.Count; index++)
                {
                    Console.WriteLine(
                        $"{names[index]} — {total} балл(ов)");
                }

                selectedCount += names.Count;

                if (selectedCount >= requiredCount)
                {
                    break;
                }
            }
        }
    }

    private static void FillResultsFile(string path)
    {
        var count = Input.ReadInt("Число участников (1–100): ",
            1, 100);
        var index = 0;
        var line = string.Empty;
        using var writer = new StreamWriter(path);
        writer.WriteLine(count);
        Console.WriteLine("Формат: Фамилия Имя Б1 Б2 Б3 Б4");
        Console.WriteLine("Баллы: 0–10; разделитель — пробел.");

        for (index = 1; index <= count; index++)
        {
            line = Input.ReadParticipant(index);

            writer.WriteLine(line);
        }
    }

    private static void PrintCollection<T>(string title,
        ICollection<T> values)
    {
        var text = values.Count == 0
            ? "нет" : string.Join(", ", values);
        Console.WriteLine($"{title}: {text}");
    }

    private static string GetDataPath(string fileName)
    {
        Directory.CreateDirectory("data");
        return Path.Combine("data", fileName);
    }
}
