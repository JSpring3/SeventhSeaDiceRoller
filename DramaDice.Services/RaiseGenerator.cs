using Combinatorics.Collections;

namespace DramaDice.Services;

public static class RaiseGenerator
{
    #region KnownSets
    
    
    private static readonly HashSet<List<int>> TargetTenMatchTwo = new()
    {
        new List<int>{1,9},
        new List<int>{2,8},
        new List<int>{3,7},
        new List<int>{4,6},
        new List<int>{5,5},
    };
    
    private static readonly HashSet<List<int>> TargetTenMatchThree = new()
    {
        new List<int>{1,1,8},
        new List<int>{1,2,7},
        new List<int>{1,3,6},
        new List<int>{1,4,5},
        new List<int>{2,2,6},
        new List<int>{2,3,5},
        new List<int>{2,4,4},
        new List<int>{3,3,4},
    };

    private static readonly HashSet<List<int>> TargetTenMatchFour = new()
    {
        new List<int>{1,1,1,7},
        new List<int>{1,1,2,6},
        new List<int>{1,1,3,5},
        new List<int>{1,1,4,4},
        new List<int>{1,2,2,5},
        new List<int>{1,2,3,4},
        new List<int>{1,3,3,3},
        new List<int>{2,2,2,4},
        new List<int>{2,2,3,3},
    };
    
    private static readonly HashSet<List<int>> TargetTenMatchFive = new()
    {
        new List<int>{1,1,1,1,6},
        new List<int>{1,1,1,2,5},
        new List<int>{1,1,1,3,4},
        new List<int>{1,1,2,2,4},
        new List<int>{1,1,2,3,3},
        new List<int>{1,2,2,2,3},
        new List<int>{2,2,2,2,2},
    };
    
    private static readonly HashSet<List<int>> TargetTenMatchSix = new()
    {
        new List<int>{1,1,1,1,1,5},
        new List<int>{1,1,1,1,2,4},
        new List<int>{1,1,1,1,3,3},
        new List<int>{1,1,1,2,2,3},
        new List<int>{1,1,2,2,2,2},
    };
    
    private static readonly HashSet<List<int>> TargetTenMatchSeven = new()
    {
        new List<int>{1,1,1,1,1,1,4},
        new List<int>{1,1,1,1,1,2,3},
        new List<int>{1,1,1,1,2,2,2},
    };
    
    private static readonly HashSet<List<int>> TargetTenMatchEight= new()
    {
        new List<int>{1,1,1,1,1,1,1,3},
        new List<int>{1,1,1,1,1,1,2,2},
    };
    
    private static readonly HashSet<List<int>> TargetTenMatchNine = new()
    {
        new List<int>{1,1,1,1,1,1,1,1,2},
    };
    
    private static readonly HashSet<List<int>> TargetTenMatchTen = new()
    {
        new List<int>{1,1,1,1,1,1,1,1,1,1},
    };
    

    #endregion
    
    // public static void GetCombos()
    // {
    //     // https://github.com/eoincampbell/combinatorics/blob/deployment/test/UnitTests/CombinatoricTests.cs
    //     //var integers = new List<int> {1,2,3,4,5,6,7,8,9,10,11,12,13,14,15};
    //     var integers = new List<int> {1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20};
    //     
    //     Console.WriteLine($"Sets of {20}");
    //     var c = new Combinations<int>(integers, 11, GenerateOption.WithRepetition);
    //
    //     foreach (var v in c)
    //     {
    //         if (v.Sum() == 20)
    //         {
    //             Console.WriteLine(string.Join(",", v));
    //         }
    //     }
    //
    //     // for (var i = 2; i < 21; i++)
    //     // {
    //     //     Console.WriteLine("-----------------------------------------");
    //     //     Console.WriteLine();
    //     // }
    // }
    public static RaiseResults Generate(IEnumerable<int> diePool, int successTarget = 10)
    {
        var originalDiePool = diePool.ToArray();
        
        var singleDieSuccesses = originalDiePool.Where(x => x == successTarget).ToList();
        if (successTarget == 10)  originalDiePool = originalDiePool.Where(x => x < 10).ToArray();
        
        var raises = new List<List<int>>();
        var traitorDice = new List<int>();
        var remainingDice = originalDiePool.ToList();
        
        foreach (var knownSet in TargetTenMatchTwo)
        {
            var (raiseSets,remaining) = ProcessSetsOfTwo(remainingDice, knownSet);
            raises.AddRange(raiseSets);
            remainingDice = remaining;
        }

        foreach (var knownSet in TargetTenMatchThree)
        {
            var (raiseSets,remaining)  = ProcessSetsOfThree(remainingDice, knownSet);
            raises.AddRange(raiseSets);
            remainingDice = remaining;
        }
        
        foreach (var knownSet in TargetTenMatchFour)
        {
            var (raiseSets,remaining)  = ProcessSetsOfFour(remainingDice, knownSet);
            raises.AddRange(raiseSets);
            remainingDice = remaining;
        }
        
        foreach (var knownSet in TargetTenMatchFive)
        {
            var (raiseSets,remaining)  = ProcessSetsOfFive(remainingDice, knownSet);
            raises.AddRange(raiseSets);
            remainingDice = remaining;
        }
        foreach (var knownSet in TargetTenMatchSix)
        {
            var (raiseSets,remaining)  = ProcessSetsOfSix(remainingDice, knownSet);
            raises.AddRange(raiseSets);
            remainingDice = remaining;
        }
        foreach (var knownSet in TargetTenMatchSeven)
        {
            var (raiseSets,remaining)  = ProcessSetsOfSeven(remainingDice, knownSet);
            raises.AddRange(raiseSets);
            remainingDice = remaining;
        }
        foreach (var knownSet in TargetTenMatchEight)
        {
            var (raiseSets,remaining)  = ProcessSetsOfEight(remainingDice, knownSet);
            raises.AddRange(raiseSets);
            remainingDice = remaining;
        }
        foreach (var knownSet in TargetTenMatchNine)
        {
            var (raiseSets,remaining)  = ProcessSetsOfNine(remainingDice, knownSet);
            raises.AddRange(raiseSets);
            remainingDice = remaining;
        }
        foreach (var knownSet in TargetTenMatchTen)
        {
            var (raiseSets,remaining)  = ProcessSetsOfTen(remainingDice, knownSet);
            raises.AddRange(raiseSets);
            remainingDice = remaining;
        }

        var i = 0;
        var diceCount  =  remainingDice.Count;
        var currentTotal = 0;
        var tempRaiseSet = new List<int>();
        var tempRaiseGroups = new List<List<int>>();
        remainingDice.Sort();
        // will be highest to lowest
        remainingDice.Reverse();
        
        
        // process dice that didn't match known sets
        do
        {
            if (remainingDice.Sum() < successTarget)
            {
                i++;
                continue;
            }

            if (currentTotal == 0)
            {
                currentTotal = remainingDice[0] + remainingDice[1];
                tempRaiseSet.Add(remainingDice[0]);
                tempRaiseSet.Add(remainingDice[1]);
                remainingDice.Remove(remainingDice[0]);
                remainingDice.Remove(remainingDice[0]);
                
                i += 2;
                if(currentTotal < successTarget) continue;
                
                tempRaiseGroups.Add(tempRaiseSet);
                currentTotal = 0;
                tempRaiseSet = new List<int>();
            }
            else
            {
                currentTotal += remainingDice[0];
                tempRaiseSet.Add(remainingDice[0]);
                remainingDice.Remove(remainingDice[0]);

                i++;
                if(currentTotal < successTarget) continue;
                
                tempRaiseGroups.Add(tempRaiseSet);
                currentTotal = 0;
                tempRaiseSet = new List<int>();
            }
        } while (i < diceCount);
        
        raises.AddRange(tempRaiseGroups);
        traitorDice.AddRange(remainingDice);
        
        raises.InsertRange(0,singleDieSuccesses.Select(success => new List<int> {success}));
        return new RaiseResults(raises,traitorDice);
    }
    private static IEnumerable<T> Supersect<T>(this IEnumerable<T> a, ICollection<T> b) => a.Where(b.Remove);
    private static (List<List<int>> RaiseSets, List<int> TraitorDice) ProcessSetsOfTwo( IEnumerable<int> dicePool,IEnumerable<int> knownSet)
    {
        var result = new List<List<int>>();
        
        var workingSet = dicePool.ToList();
        var compareSet = knownSet.ToList();
        
        int intersectCount;
        do
        {
            var currentCompareSet = new List<int>();
            currentCompareSet.AddRange(compareSet);
            
            var set = workingSet.Supersect(currentCompareSet).ToList();
            intersectCount = set.Count;
            
            if (intersectCount != 2) continue;
            
            result.Add(set);
            workingSet.Remove(set[0]);
            workingSet.Remove(set[1]);

        } while (intersectCount == 2);

        return (result, workingSet);
    }
    private static (List<List<int>> RaiseSets, List<int> TraitorDice) ProcessSetsOfThree( IEnumerable<int> dicePool,IEnumerable<int> knownSet)
    {
        var workingSet = dicePool.ToList();
        var compareSet = knownSet.ToList();
        
        var result = new List<List<int>>();
        int intersectCount;
        do
        {
            var set = workingSet.Supersect(compareSet).ToList();
            intersectCount = set.Count;
            
            if (intersectCount != 3) continue;
            
            result.Add(set);
            workingSet.Remove(set[0]);
            workingSet.Remove(set[1]);
            workingSet.Remove(set[2]);

        } while (intersectCount == 3);

        return (result, workingSet);
    }
    private static (List<List<int>> RaiseSets, List<int> TraitorDice) ProcessSetsOfFour( IEnumerable<int> dicePool,IEnumerable<int> knownSet)
    {
        var workingSet = dicePool.ToList();
        var compareSet = knownSet.ToList();
        
        var result = new List<List<int>>();
        int intersectCount;
        do
        {
            var set = workingSet.Supersect(compareSet).ToList();
            intersectCount = set.Count;
            
            if (intersectCount != 4) continue;
            
            result.Add(set);
            workingSet.Remove(set[0]);
            workingSet.Remove(set[1]);
            workingSet.Remove(set[2]);
            workingSet.Remove(set[3]);

        } while (intersectCount == 4);

        return (result, workingSet);
    }
    private static (List<List<int>> RaiseSets, List<int> TraitorDice) ProcessSetsOfFive( IEnumerable<int> dicePool,IEnumerable<int> knownSet)
    {
        var workingSet = dicePool.ToList();
        var compareSet = knownSet.ToList();
        
        var result = new List<List<int>>();
        int intersectCount;
        do
        {
            var set = workingSet.Supersect(compareSet).ToList();
            intersectCount = set.Count;
            
            if (intersectCount != 5) continue;
            
            result.Add(set);
            workingSet.Remove(set[0]);
            workingSet.Remove(set[1]);
            workingSet.Remove(set[2]);
            workingSet.Remove(set[3]);
            workingSet.Remove(set[4]);

        } while (intersectCount == 5);

        return (result, workingSet);
    }
    private static (List<List<int>> RaiseSets, List<int> TraitorDice) ProcessSetsOfSix( IEnumerable<int> dicePool,IEnumerable<int> knownSet)
    {
        var workingSet = dicePool.ToList();
        var compareSet = knownSet.ToList();
        
        var result = new List<List<int>>();
        int intersectCount;
        do
        {
            var set = workingSet.Supersect(compareSet).ToList();
            intersectCount = set.Count;
            
            if (intersectCount != 6) continue;
            
            result.Add(set);
            workingSet.Remove(set[0]);
            workingSet.Remove(set[1]);
            workingSet.Remove(set[2]);
            workingSet.Remove(set[3]);
            workingSet.Remove(set[4]);
            workingSet.Remove(set[5]);

        } while (intersectCount == 6);

        return (result, workingSet);
    }
    private static (List<List<int>> RaiseSets, List<int> TraitorDice) ProcessSetsOfSeven( IEnumerable<int> dicePool,IEnumerable<int> knownSet)
    {
        var workingSet = dicePool.ToList();
        var compareSet = knownSet.ToList();
        
        var result = new List<List<int>>();
        int intersectCount;
        do
        {
            var set = workingSet.Supersect(compareSet).ToList();
            intersectCount = set.Count;
            
            if (intersectCount != 7) continue;
            
            result.Add(set);
            workingSet.Remove(set[0]);
            workingSet.Remove(set[1]);
            workingSet.Remove(set[2]);
            workingSet.Remove(set[3]);
            workingSet.Remove(set[4]);
            workingSet.Remove(set[5]);
            workingSet.Remove(set[6]);

        } while (intersectCount == 7);

        return (result, workingSet);
    }
    private static (List<List<int>> RaiseSets, List<int> TraitorDice) ProcessSetsOfEight( IEnumerable<int> dicePool,IEnumerable<int> knownSet)
    {
        var workingSet = dicePool.ToList();
        var compareSet = knownSet.ToList();
        
        var result = new List<List<int>>();
        int intersectCount;
        do
        {
            var set = workingSet.Supersect(compareSet).ToList();
            intersectCount = set.Count;
            
            if (intersectCount != 8) continue;
            
            result.Add(set);
            workingSet.Remove(set[0]);
            workingSet.Remove(set[1]);
            workingSet.Remove(set[2]);
            workingSet.Remove(set[3]);
            workingSet.Remove(set[4]);
            workingSet.Remove(set[5]);
            workingSet.Remove(set[6]);
            workingSet.Remove(set[7]);

        } while (intersectCount == 8);

        return (result, workingSet);
    }
    private static (List<List<int>> RaiseSets, List<int> TraitorDice) ProcessSetsOfNine( IEnumerable<int> dicePool,IEnumerable<int> knownSet)
    {
        var workingSet = dicePool.ToList();
        var compareSet = knownSet.ToList();
        
        var result = new List<List<int>>();
        int intersectCount;
        do
        {
            var set = workingSet.Supersect(compareSet).ToList();
            intersectCount = set.Count;
            
            if (intersectCount != 9) continue;
            
            result.Add(set);
            workingSet.Remove(set[0]);
            workingSet.Remove(set[1]);
            workingSet.Remove(set[2]);
            workingSet.Remove(set[3]);
            workingSet.Remove(set[4]);
            workingSet.Remove(set[5]);
            workingSet.Remove(set[6]);
            workingSet.Remove(set[7]);
            workingSet.Remove(set[8]);

        } while (intersectCount == 9);

        return (result, workingSet);
    }
    private static (List<List<int>> RaiseSets, List<int> TraitorDice) ProcessSetsOfTen( IEnumerable<int> dicePool,IEnumerable<int> knownSet)
    {
        var workingSet = dicePool.ToList();
        var compareSet = knownSet.ToList();
        
        var result = new List<List<int>>();
        int intersectCount;
        do
        {
            var set = workingSet.Supersect(compareSet).ToList();
            intersectCount = set.Count;
            
            if (intersectCount != 10) continue;
            
            result.Add(set);
            workingSet.Remove(set[0]);
            workingSet.Remove(set[1]);
            workingSet.Remove(set[2]);
            workingSet.Remove(set[3]);
            workingSet.Remove(set[4]);
            workingSet.Remove(set[5]);
            workingSet.Remove(set[6]);
            workingSet.Remove(set[7]);
            workingSet.Remove(set[8]);
            workingSet.Remove(set[9]);

        } while (intersectCount == 10);

        return (result, workingSet);
    }
    
}