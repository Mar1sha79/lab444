using System;
using System.Collections.Generic;
using System.IO;

namespace ConsoleApp4
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(@"
      ЛАБОРАТОРНАЯ РАБОТА 4, часть 1

1. Задание 1.15 - List (количество различных элементов в списке)
2. Задание 2.15 - LinkedList (замена соседей элемента E на элемент F, если соседи не равны)
3. Задание 3.15 - HashSet (анализ кооперативов и культур)
4. Задание 4.15 - HashSet (анализ символов в тексте)
5. Задание 5.15 - Обработка данных школ
0. Выход
");

            while (true)
            {
                int choice = GetUserChoice(0, 5);

                if (choice == 0)
                {
                    Console.WriteLine("\nВыход из программы...");
                    break;
                }

                ExecuteTask(choice);

                Console.WriteLine("Нажмите любую клавишу для продолжения...");
                Console.ReadKey();
                Console.Clear();
                ShowMenu();
            }
        }

        static int GetUserChoice(int min, int max)
        {
            while (true)
            {
                try
                {
                    Console.Write($"\nВыберите задание ({min}-{max}, 0 для выхода): ");
                    string input = Console.ReadLine();
                    int choice = int.Parse(input);

                    if (choice == 0 || (choice >= min && choice <= max))
                        return choice;

                    Console.WriteLine($"Ошибка: введите число от {min} до {max}");
                }
                catch
                {
                    Console.WriteLine("Ошибка: введите корректное число");
                }
            }
        }

        static void ShowMenu()
        {
            Console.WriteLine(@"
Текущее меню:
1. Задание 1.15 - List
2. Задание 2.15 - LinkedList  
3. Задание 3.15 - HashSet
4. Задание 4.14 - HashSet
5. Задание 5.15 - Олимпиада
0. Выход
");
        }

        static void ExecuteTask(int taskNumber)
        {

            Console.WriteLine($"ЗАДАНИЕ {taskNumber}.15");

            try
            {
                switch (taskNumber)
                {
                    case 1:
                        ExecuteTask1();
                        break;
                    case 2:
                        ExecuteTask2();
                        break;
                    case 3:
                        ExecuteTask3();
                        break;
                    case 4:
                        ExecuteTask4();
                        break;
                    case 5:
                        ExecuteTask5();
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nОшибка при выполнении задания: {ex.Message}");
            }
        }

        static void ExecuteTask1()
        {
            Console.WriteLine("\nОпределение количества различных элементов в списке");

            List<object> L1 = InputListFromKeyboard("L1");

            if (L1 == null || L1.Count == 0)
            {
                Console.WriteLine("Список L1 пуст. Использую тестовые данные.");
                L1 = new List<object>() { 1, "опакен", "%", -4, "=", "%", 1, 1, "test", -4, "=" };
            }

            try
            {
                int distinctCount = 0;
                Dictionary<string, int> elementCounts = new Dictionary<string, int>();
                List<string> uniqueElements = new List<string>();

                foreach (object item in L1)
                {
                    string itemStr = item.ToString();

                    // Если элемента еще нет в словаре, добавляем его
                    if (!elementCounts.ContainsKey(itemStr))
                    {
                        elementCounts[itemStr] = 1;
                        uniqueElements.Add(itemStr);
                        distinctCount++;
                    }
                    else
                    {
                        // Увеличиваем счетчик для существующего элемента
                        elementCounts[itemStr]++;
                    }
                }

                Console.WriteLine($"Количество различных элементов: {distinctCount}");
                Console.WriteLine($"\nУникальные элементы: [{string.Join(", ", uniqueElements)}]");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }

        static void ExecuteTask2()
        {
            Console.WriteLine("\nЗамена соседей элемента E на элемент F, если соседи не равны");
            Console.WriteLine("\nввод данных");

            // Получаем LinkedList от пользователя
            LinkedList<object> LL = InputLinkedListFromKeyboard();
            if (LL == null || LL.Count == 0)
            {
                Console.WriteLine("Список пуст. Использую тестовые данные.");
                LL = new LinkedList<object>();
                LL.AddLast(1);
                LL.AddLast(2);
                LL.AddLast(1);
                LL.AddLast(3);
                LL.AddLast(4);
                LL.AddLast(5);
                LL.AddLast(1);
                LL.AddLast(2);
                LL.AddLast(1);
            }

            Console.WriteLine("\nИсходный список:");
            DisplayLinkedList(LL);

            Console.Write("\nВведите элемент E (значение, которое ищем): ");
            string elemEInput = Console.ReadLine();
            object elemE = ParseInput(elemEInput);

            Console.Write("Введите элемент F (на который заменяем соседей): ");
            string elemFInput = Console.ReadLine();
            object elemF = ParseInput(elemFInput);

            try
            {
                LinkedList<object> LLCopy = new LinkedList<object>(LL);

                Tasks1_5.ReplaceNeighborsIfNotEqual(LLCopy, elemE, elemF);

                Console.WriteLine($"\nРезультат LL ({LLCopy.Count} элементов):");
                DisplayLinkedList(LLCopy);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }

        static void ExecuteTask3()
        {
            Console.WriteLine("\nАнализ сельскохозяйственных культур в кооперативах");

            HashSet<string> allCrops = new HashSet<string>
    {
        "Пшеница",
        "Кукуруза",
        "Картофель",
        "Соя",
        "Подсолнечник",
        "Ячмень",
        "Рис",
        "Свекла"
    };

            HashSet<string> coop1 = new HashSet<string>
    {
        "Пшеница", "Кукуруза", "Картофель", "Соя"
    };

            HashSet<string> coop2 = new HashSet<string>
    {
        "Соя", "Кукуруза", "Подсолнечник", "Ячмень"
    };

            HashSet<string> coop3 = new HashSet<string>
    {
        "Картофель", "Соя", "Рис", "Свекла"
    };

            HashSet<string> coop4 = new HashSet<string>
    {
        "Пшеница", "Подсолнечник", "Ячмень", "Соя"
    };

            HashSet<HashSet<string>> cooperatives = new HashSet<HashSet<string>>
    {
        coop1,
        coop2,
        coop3,
        coop4
    };

            Console.WriteLine("\nПеречень всех культур:");
            int cropNum = 1;
            foreach (var crop in allCrops)
            {
                Console.WriteLine($"  {cropNum++}. {crop}");
            }

            Console.WriteLine("\nКооперативы и выпащеваемые культуры:");
            int coopNum = 1;
            foreach (var coop in cooperatives)
            {
                Console.WriteLine($"\n  Кооператив {coopNum++}:");
                foreach (var crop in coop)
                {
                    Console.WriteLine($"    • {crop}");
                }
            }

            Console.WriteLine("\nАнализ выращеваемых культур:");
            AnalyzeCrops(allCrops, cooperatives);
        }

        static void AnalyzeCrops(HashSet<string> allCrops, HashSet<HashSet<string>> cooperatives)
        {
            if (allCrops == null || cooperatives == null || cooperatives.Count == 0)
            {
                Console.WriteLine("Ошибка: некорректные входные данные");
                return;
            }

            // 1. Культуры, возделываемые во всех кооперативах
            HashSet<string> commonToAll = null;
            foreach (var coop in cooperatives)
            {
                if (commonToAll == null)
                {
                    commonToAll = new HashSet<string>(coop);
                }
                else
                {
                    commonToAll.IntersectWith(coop);
                }
            }

            Console.WriteLine("\n1. Культуры, возделываемые во ВСЕХ кооперативах:");
            if (commonToAll == null || commonToAll.Count == 0)
            {
                Console.WriteLine("   (таких культур нет)");
            }
            else
            {
                foreach (var crop in commonToAll)
                {
                    Console.WriteLine($"   • {crop}");
                }
            }

            // 2. Культуры, возделываемые в некоторых кооперативах
            HashSet<string> grownInSome = new HashSet<string>();
            foreach (var coop in cooperatives)
            {
                grownInSome.UnionWith(coop);
            }

            // 3. Культуры, НЕ возделываемые ни в одном кооперативе
            HashSet<string> notGrownAnywhere = new HashSet<string>(allCrops);
            notGrownAnywhere.ExceptWith(grownInSome);

            // Культуры, возделываемые только в некоторых (но не во всех и не ни в одном)
            HashSet<string> onlyInSome = new HashSet<string>(grownInSome);
            onlyInSome.ExceptWith(commonToAll);

            Console.WriteLine("\n2. Культуры, возделываемые только в НЕКОТОРЫХ кооперативах:");
            if (onlyInSome.Count == 0)
            {
                Console.WriteLine("   (таких культур нет)");
            }
            else
            {
                foreach (var crop in onlyInSome)
                {
                    Console.WriteLine($"   • {crop}");
                }
            }

            // 4. Культуры, возделываемые ровно в одном кооперативе
            Dictionary<string, int> cropCount = new Dictionary<string, int>();

            // Подсчитываем в скольких кооперативах выращивается каждая культура
            foreach (var coop in cooperatives)
            {
                foreach (var crop in coop)
                {
                    if (cropCount.ContainsKey(crop))
                    {
                        cropCount[crop]++;
                    }
                    else
                    {
                        cropCount[crop] = 1;
                    }
                }
            }

            HashSet<string> grownInExactlyOne = new HashSet<string>();
            foreach (var kvp in cropCount)
            {
                if (kvp.Value == 1)
                {
                    grownInExactlyOne.Add(kvp.Key);
                }
            }

            Console.WriteLine("\n3. Культуры, возделываемые ровно в ОДНОМ кооперативе:");
            if (grownInExactlyOne.Count == 0)
            {
                Console.WriteLine("   (таких культур нет)");
            }
            else
            {
                foreach (var crop in grownInExactlyOne)
                {
                    Console.WriteLine($"   • {crop}");
                }
            }
        }

        static void ExecuteTask4()
        {
            Console.WriteLine("\nАнализ символов в текстовом файле - Какие символы есть в каждом слове?");

            string filePath = "task4.txt";

            if (!File.Exists(filePath))
            {
                Console.WriteLine($"Файл '{filePath}' не найден!");
                Console.WriteLine("Для выполнения этого задания создайте файл task4.txt в папке с программой.");
                return;
            }


            try
            {
                Tasks1_5.AnalyzeCommonCharactersInWords(filePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при анализе файла: {ex.Message}");
            }
        }

        static void ExecuteTask5()
        {
            Console.WriteLine("\nОпределение школ со средним баллом выше среднего по району");

            string filePath = "task5.txt";

            if (!File.Exists(filePath))
            {
                Console.WriteLine($"Файл '{filePath}' не найден!");
                Console.WriteLine("Для выполнения этого задания создайте файл task5.txt в папке с программой.");
                return;
            }

            Console.WriteLine($"\nАнализируем файл: {Path.GetFullPath(filePath)}");

            try
            {
                Tasks1_5.FindSchoolsAboveDistrictAverage(filePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при анализе файла: {ex.Message}");
            }
        }


        static List<object> InputListFromKeyboard(string listName)
        {
            Console.WriteLine($"\nВвод элементов для списка {listName}:");
            Console.WriteLine("Вводите элементы по одному. Для завершения нажмите Enter (оставьте строку пустой)");

            List<object> list = new List<object>();
            int count = 1;

            while (true)
            {
                Console.Write($"Элемент {count}: ");
                string input = Console.ReadLine();

                if (string.IsNullOrEmpty(input))
                    break;

                object element = ParseInput(input);
                list.Add(element);
                count++;
            }

            Console.WriteLine($"Создан список {listName} с {list.Count} элементами");
            return list;
        }

        static LinkedList<object> InputLinkedListFromKeyboard()
        {
            Console.WriteLine("\nВвод элементов для LinkedList:");
            Console.WriteLine("Вводите элементы по одному. Для завершения нажмите Enter (оставьте строку пустой)");

            LinkedList<object> list = new LinkedList<object>();
            int count = 1;

            while (true)
            {
                Console.Write($"Элемент {count}: ");
                string input = Console.ReadLine();

                if (string.IsNullOrEmpty(input))
                    break;

                object element = ParseInput(input);
                list.AddLast(element);
                count++;
            }

            Console.WriteLine($"Создан LinkedList с {list.Count} элементами");
            return list;
        }

        static object ParseInput(string input)
        {
            if (int.TryParse(input, out int intValue))
                return intValue;

            if (double.TryParse(input, System.Globalization.NumberStyles.Any,
                System.Globalization.CultureInfo.InvariantCulture, out double doubleValue))
                return doubleValue;

            return input;
        }

        static void DisplayLinkedList(LinkedList<object> list)
        {
            if (list == null || list.Count == 0)
            {
                Console.WriteLine("[]");
                return;
            }

            Console.Write("[");
            var node = list.First;
            while (node != null)
            {
                Console.Write(node.Value);
                node = node.Next;
                if (node != null) Console.Write(" ");
            }
            Console.WriteLine("]");
        }
    }
}