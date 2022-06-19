using System.Security.Cryptography;

namespace DramaDice.Generators;

public static class DiceGenerator
{
    private static int GenerateInteger(int min, int max)
    {
        return RandomNumberGenerator.GetInt32(min, max + 1);
    }

    public static IEnumerable<int> Roll(int dicePool)
    {
        var results = new List<int>();
        for (var i = 1; i <= dicePool; i++)
        {
            results.Add(GenerateInteger(1,10));
        }
        return results;
    }
}