using System;
using System.Collections.Generic;
using System.Linq;

namespace ATMCombinations
{
    class Program
    {
        static void Main(string[] args)
        {
            // Available denominations in the ATM cartridges
            int[] denominations = { 10, 50, 100 };

            // Payout amounts to calculate combinations for
            int[] payoutAmounts = { 30, 50, 60, 80, 140, 230, 370, 610, 980 };

            foreach (var amount in payoutAmounts)
            {
                Console.WriteLine($"Possible combinations for {amount} EUR:");
                var combinations = CalculateCombinations(amount, denominations);

                foreach (var combination in combinations)
                {
                    Console.WriteLine(combination);
                }

                Console.WriteLine("-------------------------------------------------");
                Console.WriteLine();
            }
            Console.WriteLine("Press any key to exit....");
            Console.ReadLine();
        }

        // Function to calculate combinations for a given amount and denominations
        static List<string> CalculateCombinations(int amount, int[] denominations)
        {
            List<string> combinations = new List<string>();
            // Call the recursive function to calculate combinations
            CalculateCombinationsRecursive(amount, denominations, new List<int>(), 0, combinations);
            return combinations;
        }

        // Recursive function to find combinations
        static void CalculateCombinationsRecursive(int amount, int[] denominations, List<int> currentCombination, int startIndex, List<string> combinations)
        {
            // Base cases: when the amount is reached or not possible to form combinations
            if (amount == 0)
            {
                // Format and add the combination to the list
                string combinationString = FormatCombination(currentCombination, denominations);
                combinations.Add(combinationString);
                return;
            }

            if (amount < 0 || startIndex >= denominations.Length)
            {
                return;
            }

            // Iterate through denominations starting from the current index
            for (int i = startIndex; i < denominations.Length; i++)
            {
                // Create a new combination by adding the current denomination
                List<int> newCombination = new List<int>(currentCombination);
                newCombination.Add(denominations[i]);

                // Recursively call the function with the updated amount, combination, and index
                CalculateCombinationsRecursive(amount - denominations[i], denominations, newCombination, i, combinations);
            }
        }

        // Function to format combinations into the expected string format
        static string FormatCombination(List<int> combination, int[] denominations)
        {
            var counts = new Dictionary<int, int>();

            // Count occurrences of each denomination in the combination
            foreach (var denom in denominations)
            {
                int count = combination.Count(d => d == denom);
                if (count > 0)
                {
                    counts.Add(denom, count);
                }
            }

            // Format the counts into the desired string format
            return string.Join(" + ", counts.Select(kvp => $"{kvp.Value} x {kvp.Key} EUR"));
        }
    }
}


