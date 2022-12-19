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

            void MostCarriedCalories()
            {
                Elf.ReadSnackList("input.txt");
                //Console.WriteLine("Amount of elves: " + Elf.GetElves().Count().ToString());

                //for (int i = 0; i < 3; i++)
                //{
                //    Console.WriteLine("Elf Number " + (i+1) + "'s Snack Calorie Values");
                //    Console.WriteLine("-------------------------------------------");
                //    Elf.GetElves()[i].PrintSnacks();
                //    Console.WriteLine("-------------------------------------------");
                //}

                Console.WriteLine("Largest Calorie Sum: " + Elf.GetLargestCalorieSum());
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

        public int SumCalories()
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

        public static int GetLargestCalorieSum()
        {
            int largestCalorieSum = 0;

            foreach(Elf elf in elves)
                if(largestCalorieSum < elf.SumCalories())
                    largestCalorieSum = elf.SumCalories();

            return largestCalorieSum;
        }
    }
}