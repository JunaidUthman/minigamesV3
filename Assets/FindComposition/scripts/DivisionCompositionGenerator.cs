using UnityEngine;
using System.Collections.Generic;
using System;

public class DivisionCompositionGenerator
{
    public static List<string> GenerateRightDivisionCompositionsAsText(int target, int maxNumberRange, int requiredCount = 7)
    {
        var allResults = new List<string>();
        var seen = new HashSet<string>();

        void Backtrack(List<int> path)
        {
            if (path.Count > 1)
            {
                double result = path[0];
                for (int i = 1; i < path.Count; i++)
                    result /= path[i];

                if (Math.Abs(result - target) < 0.0001)
                {
                    string composition = string.Join("/", path);
                    if (seen.Add(composition))
                        allResults.Add(composition);
                }
            }

            if (path.Count >= 3) return;

            for (int i = 1; i <= maxNumberRange; i++)
            {
                path.Add(i);
                Backtrack(path);
                path.RemoveAt(path.Count - 1);
            }
        }

        for (int start = 1; start <= maxNumberRange; start++)
        {
            Backtrack(new List<int> { start });
        }

        // Limiter à requiredCount si possible
        if (allResults.Count > requiredCount)
            return allResults.GetRange(0, requiredCount);

        return allResults; // s’il y en a moins, on retourne ce qu’on a
    }

    public static List<string> GenerateWrongDivisionCompositionsAsText(int target, int maxNumberRange, int requiredCount)
    {
        var wrongResults = new List<string>();
        var seen = new HashSet<string>();

        for (int a = 1; a <= maxNumberRange; a++)
        {
            for (int b = 1; b <= maxNumberRange; b++)
            {
                double res2 = a / (double)b;
                if (Math.Abs(res2 - target) > 0.0001)
                {
                    string comp2 = $"{a}/{b}";
                    if (seen.Add(comp2))
                        wrongResults.Add(comp2);
                }

                for (int c = 1; c <= maxNumberRange; c++)
                {
                    double res3 = (a / (double)b) / c;
                    if (Math.Abs(res3 - target) > 0.0001)
                    {
                        string comp3 = $"{a}/{b}/{c}";
                        if (seen.Add(comp3))
                            wrongResults.Add(comp3);
                    }
                }
            }
        }

        // Limiter à requiredCount
        if (wrongResults.Count > requiredCount)
            return wrongResults.GetRange(0, requiredCount);

        return wrongResults;
    }

    // Évaluation entière
    private static int EvaluateComposition(List<int> composition)
    {
        int result = composition[0];
        for (int i = 1; i < composition.Count; i++)
        {
            if (composition[i] == 0) return int.MinValue;
            result /= composition[i];
        }
        return result;
    }
}
