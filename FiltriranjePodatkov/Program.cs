using System;
namespace FiltriranjePodatkov
{
    class Program
    {
        static void Main(string[] args)
        {
            // Podatki
            string[] names = { "Alice", "Bob", "Charlie" };
            int[] ages = { 25, 30, 35 };
            int[] salaries = { 50000, 60000, 70000 };

            // Kriteriji za filtriranje
            string[] criteriaColumns = { "age" };       // Stolpec, po katerem filtriramo
            string[] criteriaOperators = { "<" };       // Operacija za primerjavo
            string[] criteriaValues = { "32" };        // Vrednost za primerjavo

            // Filtriranje podatkov
            int[] filteredIndices = FilterRecords(names, ages, salaries, criteriaColumns, criteriaOperators, criteriaValues);

            // Izpis rezultatov
            Console.WriteLine("Filtrirani zapisi (mlajši od 32 let):");
            foreach (var index in filteredIndices)
            {
                Console.WriteLine($"Ime: {names[index]}, Starost: {ages[index]}, Plača: {salaries[index]}");
            }
        }

        static int[] FilterRecords(string[] names, int[] ages, int[] salaries, string[] criteriaColumns, string[] criteriaOperators, string[] criteriaValues)
        {
            int count = names.Length;
            int[] filteredIndices = new int[count];
            int filteredCount = 0;

            for (int i = 0; i < count; i++)
            {
                bool meetsCriteria = true;

                for (int j = 0; j < criteriaColumns.Length; j++)
                {
                    string column = criteriaColumns[j];
                    string op = criteriaOperators[j];
                    string value = criteriaValues[j];

                    if (column == "name")
                    {
                        if (!Compare(names[i], op, value))
                        {
                            meetsCriteria = false;
                            break;
                        }
                    }
                    else if (column == "age")
                    {
                        if (!Compare(ages[i], op, int.Parse(value)))
                        {
                            meetsCriteria = false;
                            break;
                        }
                    }
                    else if (column == "salary")
                    {
                        if (!Compare(salaries[i], op, int.Parse(value)))
                        {
                            meetsCriteria = false;
                            break;
                        }
                    }
                }

                if (meetsCriteria)
                {
                    filteredIndices[filteredCount] = i;
                    filteredCount++;
                }
            }

            // Zmanjšamo tabelo na dejansko število filtriranih zapisov
            int[] result = new int[filteredCount];
            Array.Copy(filteredIndices, result, filteredCount);
            return result;
        }

        static bool Compare(int a, string op, int b)
        {
            switch (op)
            {
                case ">": return a > b;
                case "<": return a < b;
                case ">=": return a >= b;
                case "<=": return a <= b;
                case "==": return a == b;
                case "!=": return a != b;
                default: throw new ArgumentException("Neveljavna operacija");
            }
        }

        static bool Compare(string a, string op, string b)
        {
            switch (op)
            {
                case "==": return a == b;
                case "!=": return a != b;
                default: throw new ArgumentException("Neveljavna operacija za nize");
            }
        }
    }
}