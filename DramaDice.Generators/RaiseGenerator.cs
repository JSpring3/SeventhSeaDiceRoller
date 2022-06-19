using Combinatorics.Collections;
using DramaDice.Generators.Data;
using DramaDice.Models;

namespace DramaDice.Generators;
public static class RaiseGenerator
{
    private static IEnumerable<T> Supersect<T>(this IEnumerable<T> a, ICollection<T> b) => a.Where(b.Remove);
    private static List<List<int>> GetKnownSets(int successTarget, int setSize)
    {
        return Lookups.KnownSets[successTarget][setSize];
    }
    public static RaiseResults Generate(IEnumerable<int> diePool, int successTarget = 10)
    {
        var originalDiePool = diePool.ToArray();
        
        var singleDieSuccesses = originalDiePool.Where(x => x == successTarget).ToList();
        if (successTarget == 10)  originalDiePool = originalDiePool.Where(x => x < 10).ToArray();
        
        var raises = new List<List<int>>();
        var traitorDice = new List<int>();
        
        try
        {
            // process dice against known sets
            var (raiseSets, remaining) = ProcessKnownSets(originalDiePool,successTarget);
        
            raises.AddRange(raiseSets);
        
            // process dice that didn't match known sets
            var results = ProcessUnknownSets(remaining,successTarget);
        
            raises.AddRange(results.RaiseSets);
            traitorDice.AddRange(results.TraitorDice);
            raises.InsertRange(0,singleDieSuccesses.Select(success => new List<int> {success}));
        }
        catch (Exception e)
        {
            Console.WriteLine("An Error Occurred");
            foreach (var item in originalDiePool)
            {
                Console.WriteLine(string.Join(",", item));
            }
            Console.WriteLine();
            Console.WriteLine(e);
        }
        
        var (raiseFinal, traitorDiceFinal) = OptimizeRaises(raises, traitorDice);
        return new RaiseResults(raiseFinal,traitorDiceFinal);
    }
    private static (List<List<int>> RaiseSets, List<int> TraitorDice) ProcessKnownSets(IEnumerable<int> originalDiePool, int successTarget)
    {
        var diePool = originalDiePool.ToList();
        var (raises, remaining) = Process(diePool,successTarget);
        
        return (raises, remaining);
    }
    
    private static (List<List<int>> RaiseSets, List<int> TraitorDice)Process(List<int> dicePool, int successTarget)
    {
        var raises = new List<List<int>>();
        var originalDiceCount = dicePool.Count;
        var remainingDice = dicePool;
        
        for (var index = 2; index < originalDiceCount;)
        {
            if(index > remainingDice.Count || index > successTarget) break;
            var knownSets = GetKnownSets(successTarget, index);
            
            foreach (var set in knownSets)
            {
                var (raiseSets,remaining) = MatchKnownSets(remainingDice,set, index );
                raises.AddRange(raiseSets);
                remainingDice = remaining;
            }
            index++;
        }
        return (raises, remainingDice);
    }

    private static (List<List<int>> RaiseSets, List<int> TraitorDice) OptimizeRaises(List<List<int>> raiseSets, List<int> traitorDice)
    {
        if (traitorDice.Count > 1)
        {
            var sum = traitorDice.Sum();
           // var optimizedRaises = new List<List<int>>();
            var foundMatch = false;
            
            foreach (var set in raiseSets.TakeWhile(_ => !foundMatch))
            {
                for (var i = 0; i < set.Count; i++)
                {
                    var die = set[i];
                    if (die != sum) continue;
                    
                    set.Remove(die);
                    set.AddRange(traitorDice);
                    set.Sort();
                    set.Reverse();
                    traitorDice.Clear();
                    traitorDice.Add(die);
                    foundMatch = true;
                    break;
                }
            }
        }
        else
        {
            return (raiseSets, traitorDice);
        }
        return (raiseSets, traitorDice);
    }
    private static (List<List<int>> RaiseSets, List<int> TraitorDice) MatchKnownSets( IEnumerable<int> dicePool,
        IEnumerable<int> knownSet,int setSize)
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
            
            if (intersectCount != setSize) continue;
            
            result.Add(set);
            foreach (var die in set)
            {
                workingSet.Remove(die);
            }
        } while (intersectCount == setSize);

        return (result, workingSet);
    }
    private static RaiseResults ProcessUnknownSets(IEnumerable<int> diePool, int successTarget)
    {
        var currentDiePool = diePool.ToList();
        if (currentDiePool.Sum() < successTarget) return new RaiseResults(new List<List<int>>(),currentDiePool);
        
        var permutations = new Permutations<int>(currentDiePool, GenerateOption.WithoutRepetition);

        // foreach (var v in permutations)
        // {
        //     Console.WriteLine(string.Join(",", v));
        // }
        
        var resultsList = ProcessPermutations(permutations,successTarget);
       // var hhhh = resultsList.ToList();
        
        // foreach (var r in hhhh)
        // {
        //     Console.WriteLine($"Raises = {r.RaiseSets.Count}");
        //     foreach (var item in r.RaiseSets)
        //     {
        //         Console.WriteLine(string.Join(",", item));
        //     }
        //     Console.WriteLine($"Traitor Dice = {r.TraitorDice.Count}");
        //     Console.WriteLine(string.Join(",", r.TraitorDice));
        //     Console.WriteLine("--------------------------------------------------");
        // }
        //  Console.ReadKey();
        
        // var successes = hhhh.Where(x => x.RaiseSets)

       var result = resultsList
           .OrderBy(x => x.TraitorDice.Count)
            .MinBy(x => x.RaiseSets[0].Sum());
        
        return result ?? throw new InvalidOperationException();
    }

    private static IEnumerable<RaiseResults> ProcessPermutations(Permutations<int> permutations, int successTarget)
    {
        var resultsList = new List<RaiseResults>();
        foreach (var p in permutations)
        {
            var queue = CreateQueue(p);
           var (raiseSets,traitorDice) = ProcessQueue(queue, successTarget);
           resultsList.Add(new RaiseResults(raiseSets,traitorDice));
        }

        return resultsList;
    }

    private static (List<List<int>> RaiseSets, List<int> TraitorDice) ProcessQueue(Queue<int> queue, int successTarget)
    {
        var raiseSets = new List<List<int>>();
        var remaining = new List<int>();
        var temp = new List<int>();
        
        do
        {
            if (temp.Sum() == 0 && queue.Count >= 2)
            {
                var die = queue.Dequeue();
                var nextDie = queue.Dequeue();
                
                temp.Add(die);
                temp.Add(nextDie);
            }
            else
            {
                var die = queue.Dequeue();
                temp.Add(die);
            }

            if (temp.Sum() < successTarget)
            {
                if (queue.Count > 0) continue;
                remaining = temp.ToList();
            }
            else
            {
                raiseSets.Add(temp);
                temp = new List<int>();
            }
        } while (queue.Count > 0);
        
        
        return (raiseSets, remaining);
    }

    // ReSharper disable once ReturnTypeCanBeEnumerable.Local
    // ReSharper disable once ParameterTypeCanBeEnumerable.Local
    private static Queue<int> CreateQueue(IReadOnlyList<int> permutation)
    {
        var queue = new Queue<int>();
        foreach (var item in permutation)
        {
            queue.Enqueue(item);
        }
        return queue;
    }
    
}