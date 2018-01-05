namespace _01.SchoolCompetition
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Startup
    {
        public static void Main()
        {
            var studentScores = new Dictionary<string, int>();
            var studentCategories = new Dictionary<string, HashSet<string>>();

            while (true)
            {
                var input = Console.ReadLine();

                if (input == "END")
                {
                    break;
                }

                var parts = input
                    .Split();

                var name = parts[0];
                var category = parts[1];
                var points = int.Parse(parts[2]);

                if (!studentScores.ContainsKey(name))
                {
                    studentScores[name] = 0;
                }

                if (!studentCategories.ContainsKey(name))
                {
                    studentCategories[name] = new HashSet<string>();
                }

                studentScores[name] += points;
                studentCategories[name].Add(category);
            }

            var orderedStudentsByPoints = studentScores
                .OrderByDescending(kvp => kvp.Value)
                .ThenBy(kvp => kvp.Key);

            foreach (var kvp in orderedStudentsByPoints)
            {
                var name = kvp.Key;
                var points = kvp.Value;

                var orderedCategoriesAsString = $@"[{string.Join(", ", studentCategories[name]
                    .OrderBy(c => c))}]";

                Console.WriteLine($"{name}: {points} {orderedCategoriesAsString}");
            }
        }
    }
}
