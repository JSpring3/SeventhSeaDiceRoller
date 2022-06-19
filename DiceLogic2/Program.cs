using DramaDice.Services;

var dicePool0 = new[] {10, 10, 8, 4, 3, 3, 2, 1, 1};
var dicePoolNoTens = dicePool0.Where(x => x < 10);
var dicePool1 = new[] {9,9,7};
var dicePool2 = new[] {9,7,7,7,7,4};
var dicePool3 = new[] { 7,7,7,7,6,3,3};
var dicePool4 = new[] {9,7,7,6,5,4,2,2};
var dicePool5 = new[] {9,9,7,7,6,6,6,5,4,4,4,4,4,3,3,3,3,3,3,2,2,2,1,1};
var dicePool6 = new[] {10,10,9,9,7,7,6,6,6,5,4,4,4,4,4,3,3,3,3,3,3,2,2,2,1,1,1};
var dicePool6NoTens = dicePool6.Where(x => x < 10).ToArray();
var dicePool7 = new[] {10,10,8,8,5,5,3,3,2,1};
var dicePool8 = new[] {10,10,10,8,7,4,4,2,1,1};
var dicePool9 = new[] {9,9,8,5,5,1,1,1,1};
var dicePool10 = new[] {9,9,8,6,5,5,3,3,1,1};

// test really big sets
// 30 dice
var dicePool11 = new[] {10,10,10,9,9,8,8,8,8,8,8,7,7,6,6,5,4,4,4,4,4,4,3,3,3,2,1,1,1,1};
// 40 dice
var dicePool12 = new[] {10,10,10,9,9,9,9,9,9,9,9,8,8,7,7,7,7,7,7,6,6,6,6,5,4,4,3,3,3,2,2,2,2,2,2,1,1,1,1,1};
// 50 dice
var dicePool13 = new[] {10,10,10,9,9,9,9,9,9,9,9,8,8,7,7,7,7,7,7,6,6,6,6,5,4,4,3,3,3,2,2,2,2,2,2,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,};

var myDicePool = dicePool4;
var results = RaiseGenerator.Generate(myDicePool);



Console.WriteLine();
Console.WriteLine($"Dice Pool = { string.Join(",", myDicePool)}");
Console.WriteLine($"Dice Rolled = {myDicePool.Length}");
Console.WriteLine($"Raise Groups = {results.RaiseSets.Count}");
foreach (var set in results.RaiseSets)
{
 Console.WriteLine(string.Join(",", set));
}
if (!results.TraitorDice.Any()) return;
  Console.WriteLine();
  Console.WriteLine("Traitor Dice");
  Console.WriteLine(string.Join(",", results.TraitorDice));

/*
 * 9,7,7,6,5,4,2,2
 *
 * 
 * 6,2,2
 * 9,4
 * 7,5
 * 
 * traitor dice 7
 * 
 */
  