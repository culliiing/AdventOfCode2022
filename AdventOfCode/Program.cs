using System;
using System.Data;

namespace AdventOfCode
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MostCarriedCalories();
            Console.ReadKey();
        }

        public static void MostCarriedCalories()
        {
            Elf.ReadSnackList("input.txt");

            // Print Elf Count
            Console.WriteLine("Amount of elves: " + Elf.GetElves().Count().ToString());

            // Print Elf inventories, starting with first elf in the list
            PrintElfInventory(3);

            Console.WriteLine("Largest Calorie Sum: " + Elf.GetLargestCalorieSum());
            Console.WriteLine("Sum of the largest calorie sums: " + Elf.GetLargestCalorieSum(3));
        }

        public static void PrintElfInventory(int amountOfElves)
        {
            for (int i = 0; i < amountOfElves; i++)
            {
                Console.WriteLine("Elf Number " + (i + 1) + "'s Snack Calorie Values");
                Console.WriteLine("-------------------------------------------");
                Elf.GetElves()[i].PrintSnacks();
                Console.WriteLine("-------------------------------------------");
            }
        }
    }

    internal class Snack
    {
        int calories;

        public Snack(int calories)
        {
            this.calories = calories;
        }

        public int GetCalories()
        {
            return calories;
        }
    }

    internal class Elf
    {
        static List<Elf> elves = new List<Elf>();
        List<Snack> snacks;

        public Elf()
        {
            snacks = new List<Snack>();
        }

        public void AddSnack(Snack snack)
        {
            snacks.Add(snack);
        }

        public static List<Elf> GetElves()
        {
            return elves;
        }

        public void PrintSnacks()
        {
            foreach(Snack snack in snacks)
            {
                Console.WriteLine(snack.GetCalories().ToString());
            }
        }

        // Sum the calories of all snacks carried by an elf
        public int SumCarriedCalories()
        {
            int calorieSum = 0;

            foreach (Snack snack in snacks)
            {
                calorieSum += snack.GetCalories();
            }

            return calorieSum;
        }

        public static void ReadSnackList(string fileName)
        {
            string[] lines = File.ReadAllLines("..\\..\\..\\" + fileName);

            Elf elf = new Elf();
            elves.Add(elf);

            for (int i = 0; i < lines.Length; i++)
            {
                if (!string.IsNullOrEmpty(lines[i]))
                {
                    elf.AddSnack(new Snack(Convert.ToInt32(lines[i])));
                }
                else
                {
                    elf = new Elf();
                    elves.Add(elf);
                }
            }
        }

        // Calculates the sum of calories carried by the elf carrying the most calories
        public static int GetLargestCalorieSum()
        {
            int largestCalorieSum = 0;

            foreach(Elf elf in elves)
                if(largestCalorieSum < elf.SumCarriedCalories())
                    largestCalorieSum = elf.SumCarriedCalories();

            return largestCalorieSum;
        }
        
        // Calculates the sums of calories carried by the elves who carry the largest sums of calories
        public static int GetLargestCalorieSum(int amountOfTopSums)
        {
            int[] largestCalorieSums = new int[amountOfTopSums];
            int largestCalorieSumsSum = 0;

            Elf[] excludedElves = new Elf[amountOfTopSums];

            for (int i = 0; i < amountOfTopSums; i++)
            {
                foreach(Elf elf in elves)
                {
                    if (excludedElves.Contains(elf))
                        continue;
                    else if (largestCalorieSums[i] < elf.SumCarriedCalories())
                        largestCalorieSums[i] = elf.SumCarriedCalories();
                }

                largestCalorieSumsSum += GetElfWithCalorieAmount(largestCalorieSums[i]).SumCarriedCalories();
                excludedElves[i] = GetElfWithCalorieAmount(largestCalorieSums[i]);
            }

            return largestCalorieSumsSum;
        }

        // Find the elf with a certain calorie count
        public static Elf GetElfWithCalorieAmount(int totalCarriedCalories)
        {
            foreach (Elf elf in elves)
                if (elf.SumCarriedCalories() == totalCarriedCalories)
                    return elf;

            return null;
        }
    }
}