using UnityEngine;
using System.Collections.Generic;
using System;

public class DivisionCompositionGenerator
{
    public static List<string> GenerateRightDivisionCompositionsAsText(int target, int maxNumberRange, int requiredCount = 7)
    {
        var results = new List<string>();
        var seen = new HashSet<string>();

        for (int a = 1; a <= maxNumberRange; a++)
        {
            for (int b = 1; b <= maxNumberRange; b++)
            {
                if (b == 0) continue;

                double result = a / (double)b;
                if (Math.Abs(result - target) < 0.0001)
                {
                    string composition = $"{a}/{b}";
                    if (seen.Add(composition))
                        results.Add(composition);

                    if (results.Count >= requiredCount)
                        return results;
                }
            }
        }

        return results;
    }

    public static List<string> GenerateWrongDivisionCompositionsAsText(int target, int maxNumberRange, int requiredCount)
    {
        var wrongResults = new List<string>();
        var seen = new HashSet<string>();
        var random = new System.Random();

        // On tente un grand nombre d’essais aléatoires (ex: 1000)
        int attempts = 0;
        int maxAttempts = 10000;

        while (wrongResults.Count < requiredCount && attempts < maxAttempts)
        {
            int a = random.Next(1, maxNumberRange + 1);
            int b = random.Next(1, maxNumberRange + 1); // éviter b=0 automatiquement

            double result = a / (double)b;

            if (Math.Abs(result - target) > 0.0001)
            {
                string composition = $"{a}/{b}";
                if (seen.Add(composition))
                    wrongResults.Add(composition);
            }

            attempts++;
        }

        return wrongResults;
    }


}
