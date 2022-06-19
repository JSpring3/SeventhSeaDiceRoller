using DramaDice.Services;
using Gapotchenko.FX.Math.Combinatorics;

var dicePool = new int[] { 10, 10, 8, 4, 3, 3, 2, 1, 1 };


var dicePoolNoTens = dicePool.Where(x => x < 10);

// var testCalc = new RaiseGenerator(dicePoolNoTens,10);
// testCalc.Calc();

//foreach (var i in Permutations.Of(dicePoolNoTens).Distinct())
//{
//    // results.Add(new RaiseCalculator().ProcessDieSet(i.ToArray(), 10));

//    Console.WriteLine(string.Join(" ", i.Select(x => x.ToString())));
//}




//var test = dicePool[1..];




/*

Another example: I roll 8, 4, 3, 3, 2, 1, 1

8+1+1 = 10 (1 Raise),

4+3+3 = 10 (1 Raise)

I have a 2 left over.

8 4 = 12
3 3 1 1 2 = 10

 */

