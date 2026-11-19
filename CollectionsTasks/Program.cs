using System;
using System.IO;
using System.Text;

namespace Lab4.CollectionsTasks;

internal static class Program
{
    private static void Main()
    {
        Console.InputEncoding = Encoding.UTF8;
        Console.OutputEncoding = Encoding.UTF8;

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
            Console.WriteLine("\nЛабораторная №4. Вариант 3");
            Console.WriteLine("1 — Слияние списков");
            Console.WriteLine("2 — Элементы с равными соседями");
            Console.WriteLine("3 — Заказы посетителей кафе");
            Console.WriteLine("4 — Согласные в одном слове");
            Console.WriteLine("5 — Лучшие участники многоборья");
            Console.WriteLine("0 — Выход");
            choice = Input.ReadInt("Выберите задание: ", 0, 5);

            try
            {
                switch (choice)
                {
                    case 0:
                        return;
                    case 1:
                        CollectionTasks.RunTask1();
                        break;
                    case 2:
                        CollectionTasks.RunTask2();
                        break;
                    case 3:
                        CollectionTasks.RunTask3();
                        break;
                    case 4:
                        CollectionTasks.RunTask4();
                        break;
                    case 5:
                        CollectionTasks.RunTask5();
                        break;
                }
            }
            catch (EndOfStreamException)
            {
                throw;
            }
            catch (IOException exception)
            {
                Console.WriteLine(
                    $"Ошибка файла: {exception.Message}");
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine("Нет доступа к файлу.");
            }
        }
    }

}
