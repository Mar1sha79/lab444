using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

class Tasks1_5
{
    public static void ReplaceNeighborsIfNotEqual(LinkedList<object> list, object elementE, object elementF)
    {
        if (list == null)
            throw new Exception("Список пуст");

        if (list.Count < 3)
        {
            Console.WriteLine("в списке менее 3 элементов");
            return;
        }

        LinkedListNode<object> currentNode = list.First;
        int replacementsMade = 0;

        while (currentNode != null)
        {
            // Проверяем, равен ли текущий элемент элементу E
            if (currentNode.Value != null && currentNode.Value.Equals(elementE) ||
                currentNode.Value == null && elementE == null)
            {
                // Проверяем наличие обоих соседей
                if (currentNode.Previous != null && currentNode.Next != null)
                {
                    object leftNeighbor = currentNode.Previous.Value;
                    object rightNeighbor = currentNode.Next.Value;

                    // Проверяем, НЕ равны ли левый и правый соседи между собой
                    bool areNeighborsEqual = (leftNeighbor == null && rightNeighbor == null) ||
                                             (leftNeighbor != null && leftNeighbor.Equals(rightNeighbor));

                    if (!areNeighborsEqual)
                    {
                        // Заменяем соседей на элемент F
                        currentNode.Previous.Value = elementF;
                        currentNode.Next.Value = elementF;
                        replacementsMade++;
                    }
                }
            }

            currentNode = currentNode.Next;
        }

        if (replacementsMade == 0)
        {
            Console.WriteLine($"Note: No replacements made. Element '{elementE}' not found or neighbors are equal.");
        }
    }


    public static void AnalyzeCultur(HashSet<string> allBooks, HashSet<HashSet<string>> readersBooks)
    {
        if (allBooks == null || readersBooks == null)
        {
            throw new ArgumentException("Error: Invalid input data");
        }

        // Переименуем переменные для сельскохозяйственного контекста
        HashSet<string> allCrops = allBooks;
        HashSet<HashSet<string>> cooperatives = readersBooks;

        // 1. Культуры, возделываемые во всех кооперативах
        HashSet<string> commonToAll = null;
        foreach (HashSet<string> coop in cooperatives)
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

        // 2. Культуры, возделываемые в некоторых кооперативах
        HashSet<string> grownInSome = new HashSet<string>();
        foreach (HashSet<string> coop in cooperatives)
        {
            grownInSome.UnionWith(coop);
        }

        // 3. Культуры, возделываемые ровно в одном кооперативе
        Dictionary<string, int> cropCount = new Dictionary<string, int>();
        foreach (HashSet<string> coop in cooperatives)
        {
            foreach (string crop in coop)
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

        Console.WriteLine("\nКультуры, возделываемые во всех кооперативах:");
        if (commonToAll == null || commonToAll.Count == 0)
            Console.WriteLine("(нет таких культур)");
        else
            foreach (string crop in commonToAll)
                Console.WriteLine($"- {crop}");

        Console.WriteLine("\nКультуры, возделываемые только в некоторых кооперативах:");
        HashSet<string> onlyInSome = new HashSet<string>(grownInSome);
        if (commonToAll != null)
            onlyInSome.ExceptWith(commonToAll);

        if (onlyInSome.Count == 0)
            Console.WriteLine("(нет таких культур)");
        else
            foreach (string crop in onlyInSome)
                Console.WriteLine($"- {crop}");

        Console.WriteLine("\nКультуры, возделываемые ровно в одном кооперативе:");
        if (grownInExactlyOne.Count == 0)
            Console.WriteLine("(нет таких культур)");
        else
            foreach (string crop in grownInExactlyOne)
                Console.WriteLine($"- {crop}");
    }

    public static void AnalyzeCommonCharactersInWords(string filePath)
    {
        if (string.IsNullOrEmpty(filePath) || !File.Exists(filePath))
        {
            throw new ArgumentException("Error: The file doesn't exist or the path to it is empty");
        }

        string text = File.ReadAllText(filePath);

        if (string.IsNullOrWhiteSpace(text))
        {
            Console.WriteLine("Файл пуст или содержит только пробелы");
            return;
        }

        Console.WriteLine("\nДанные в файле:");
        Console.WriteLine(text);

        char[] separators = new char[] {
        ' ', ',', '.', '!', '?', ';', ':', '(', ')',
        '"', '\t', '\n', '\r', '-', '—', '[', ']',
        '{', '}', '/', '\\', '«', '»'
    };

        string[] words = text.Split(separators, StringSplitOptions.RemoveEmptyEntries);

        Console.WriteLine($"\nВсего найдено слов: {words.Length}");

        if (words.Length < 2)
        {
            Console.WriteLine("\nВ тексте меньше 2 слов. Анализ невозможен.");
            return;
        }

        List<HashSet<char>> wordCharSets = new List<HashSet<char>>();

        for (int i = 0; i < words.Length; i++)
        {
            HashSet<char> chars = new HashSet<char>();

            foreach (char c in words[i])
            {
                if (char.IsLetter(c))
                {
                    chars.Add(char.ToLower(c));
                }
            }

            if (chars.Count > 0)
            {
                wordCharSets.Add(chars);
            }
        }

        if (wordCharSets.Count == 0)
        {
            Console.WriteLine("\nВ словах не найдено буквенных символов.");
            return;
        }

        // Находим пересечение всех наборов символов
        HashSet<char> commonChars = new HashSet<char>(wordCharSets[0]);

        for (int i = 1; i < wordCharSets.Count; i++)
        {
            commonChars.IntersectWith(wordCharSets[i]);
        }

        if (commonChars.Count == 0)
        {
            Console.WriteLine("\nНет символов, которые присутствуют в КАЖДОМ слове.");
        }
        else
        {
            Console.WriteLine($"\nСимволы, которые есть в КАЖДОМ слове ({commonChars.Count}):");
            Console.WriteLine($"[{string.Join(", ", commonChars.OrderBy(c => c))}]");
        }
    }
    public static void FindSchoolsAboveDistrictAverage(string inputFilePath)
    {
        if (string.IsNullOrEmpty(inputFilePath) || !File.Exists(inputFilePath))
        {
            throw new ArgumentException("Error: The file doesn't exist or the path to it is empty");
        }

        string[] lines = File.ReadAllLines(inputFilePath);

        if (lines.Length == 0)
        {
            Console.WriteLine("Файл пуст");
            return;
        }

        // Парсим количество учеников
        if (!int.TryParse(lines[0].Trim(), out int N) || N < 5)
        {
            Console.WriteLine("Ошибка: некорректное количество учеников (должно быть не менее 5)");
            return;
        }

        Console.WriteLine("\nСодержимое файла:");
        Console.WriteLine(lines[0]);

        for (int i = 1; i < lines.Length && i <= N; i++)
        {
            Console.WriteLine(lines[i]);
        }

        // Словари для хранения данных по школам
        Dictionary<int, int> schoolTotalScores = new Dictionary<int, int>();
        Dictionary<int, int> schoolStudentCounts = new Dictionary<int, int>();

        int totalScoreAll = 0;
        int totalStudentsAll = 0;

        Console.WriteLine("\nОбработка данных:");

        // Обрабатываем данные учеников
        for (int i = 1; i <= N && i < lines.Length; i++)
        {
            string line = lines[i].Trim();
            if (string.IsNullOrEmpty(line))
                continue;

            string[] parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length != 4)
            {
                Console.WriteLine($"  Ошибка в строке {i}: неверный формат данных");
                continue;
            }

            string lastName = parts[0];
            string firstName = parts[1];

            if (!int.TryParse(parts[2], out int school) || !int.TryParse(parts[3], out int score))
            {
                Console.WriteLine($"  Ошибка в строке {i}: некорректные числовые данные");
                continue;
            }

            if (school < 1 || school > 99)
            {
                Console.WriteLine($"  Ошибка в строке {i}: номер школы вне диапазона 1-99");
                continue;
            }

            if (score < 1 || score > 100)
            {
                Console.WriteLine($"  Ошибка в строке {i}: балл вне диапазона 1-100");
                continue;
            }

            totalStudentsAll++;
            totalScoreAll += score;

            // Обновляем данные по школе
            if (schoolTotalScores.ContainsKey(school))
            {
                schoolTotalScores[school] += score;
                schoolStudentCounts[school]++;
            }
            else
            {
                schoolTotalScores[school] = score;
                schoolStudentCounts[school] = 1;
            }

            Console.WriteLine($"  {lastName} {firstName}, школа {school}, балл {score}");
        }

        Console.WriteLine($"\nОбработано учеников: {totalStudentsAll}");

        if (totalStudentsAll < 5)
        {
            Console.WriteLine("Ошибка: недостаточно данных (менее 5 учеников)");
            return;
        }

        double districtAverage = (double)totalScoreAll / totalStudentsAll;
        Console.WriteLine($"Средний балл по району: {districtAverage:F2}");

        // Находим школы с баллом выше среднего
        List<int> schoolsAboveAverage = new List<int>();
        Dictionary<int, double> schoolAverages = new Dictionary<int, double>();

        Console.WriteLine("\nСредние баллы по школам:");
        foreach (int school in schoolTotalScores.Keys.OrderBy(s => s))
        {
            double schoolAverage = (double)schoolTotalScores[school] / schoolStudentCounts[school];
            schoolAverages[school] = schoolAverage;

            string status = schoolAverage > districtAverage ? "ВЫШЕ среднего" : "ниже или равно среднему";
            Console.WriteLine($"  Школа {school}: {schoolAverage:F2} ({status})");

            if (schoolAverage > districtAverage)
            {
                schoolsAboveAverage.Add(school);
            }
        }

        Console.WriteLine("\nВЫВОД ПРОГРАММЫ:");

        // Формируем вывод согласно условиям задачи
        if (schoolsAboveAverage.Count == 0)
        {
            // По условию задачи, если нет школ выше среднего, ничего не выводим
            Console.WriteLine("(Нет школ со средним баллом выше среднего по району)");
        }
        else if (schoolsAboveAverage.Count == 1)
        {
            // Если школа одна - выводим номер школы и средний балл
            int schoolNumber = schoolsAboveAverage[0];
            int roundedAverage = (int)Math.Round(schoolAverages[schoolNumber]);
            Console.WriteLine(schoolNumber);
            Console.WriteLine($"Средний балл = {roundedAverage}");
        }
        else
        {
            // Если школ несколько - выводим только номера школ через пробел
            schoolsAboveAverage.Sort();
            Console.WriteLine(string.Join(" ", schoolsAboveAverage));
        }
    }
}