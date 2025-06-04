using UnityEngine;
using System.Collections.Generic;
using System;

public class DivisionCompositionGenerator
{
    public static List<string> GenerateRightDivisionCompositionsAsText(int target, int maxNumberRange, int requiredCount = 7)
    {
        List<string> results = new List<string>();
        HashSet<string> seen = new HashSet<string>();

        void Backtrack(List<int> path)
        {
            if (path.Count > 1)
            {
                // Évaluer la division de gauche à droite
                double result = path[0];
                for (int i = 1; i < path.Count; i++)
                {
                    result /= path[i];
                }

                if (Math.Abs(result - target) < 0.0001) // comparaison flottante
                {
                    string composition = string.Join("/", path);
                    if (!seen.Contains(composition))
                    {
                        seen.Add(composition);
                        results.Add(composition);
                    }
                }
            }

            if (results.Count >= requiredCount || path.Count >= 3) return; // Limite à 3 nombres

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

        return results.GetRange(0, Math.Min(requiredCount, results.Count));
    }



    public static List<string> GenerateWrongDivisionCompositionsAsText(int target, int maxNumberRange, int requiredCount)
    {
        List<string> wrongResults = new List<string>();
        HashSet<string> seen = new HashSet<string>();

        System.Random rng = new System.Random();

        while (wrongResults.Count < requiredCount)
        {
            List<int> composition = new List<int>();

            int length = rng.Next(2, 4); // length between 2 and 3 numbers only
            for (int i = 0; i < length; i++)
            {
                int num = rng.Next(1, maxNumberRange + 1);
                composition.Add(num);
            }

            int result = EvaluateComposition(composition);
            string text = string.Join("/", composition);

            if (result != target && !seen.Contains(text))
            {
                seen.Add(text);
                wrongResults.Add(text);
            }
        }

        return wrongResults;
    }


    // Helper method to evaluate the integer result of a composition
    private static int EvaluateComposition(List<int> composition)
    {
        int result = composition[0];
        for (int i = 1; i < composition.Count; i++)
        {
            if (composition[i] == 0) return int.MinValue; // avoid division by zero
            result /= composition[i];
        }
        return result;
    }
}
