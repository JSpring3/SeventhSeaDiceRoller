using DramaDice.Generators;

Console.WriteLine();
Console.WriteLine();

#region Dice Test Sets
// 9 dice
var dicePool1 = new[] {9,7,7,6,5,4,3,2,2};
// 30 dice
var dicePool2 = new[] {10,10,10,9,9,8,8,8,8,8,8,7,7,6,6,5,4,4,4,4,4,4,3,3,3,2,1,1,1,1};
// 40 dice
var dicePool3 = new[] {10,10,10,9,9,9,9,9,9,9,9,8,8,7,7,7,7,7,7,6,6,6,6,5,4,4,3,3,3,2,2,2,2,2,2,1,1,1,1,1};
// 50 dice
var dicePool4 = new[] {10,10,10,9,9,9,9,9,9,9,9,8,8,7,7,7,7,7,7,6,6,6,6,5,4,4,3,3,3,2,2,2,2,2,2,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1};
// 8 dice
var dicePool5 = new[] {9,7,7,6,5,4,2,2};
// 8 dice
var noSuccess1 = new[] {1,1,1,1,1,1,1,1};

var errorPool = new[] {10,10,10,10,10,10,10,10,10,10,10,9,9,9,9,9,9,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,8,7,7,7,7,7,7,7,6,6,6,6,6,6,6,5,5,5,5,5,5,4,4,4,4,4,4,4,4,4,3,3,3,3,3
    ,3,3,3,3,3,3,3,3,3,3,3,3,2,2,2,2,1,1,1,1,1,1,1,1,1,1,1,1,1};
#endregion

var myDicePool = DiceGenerator.Roll(40).ToList();
myDicePool.Sort();
myDicePool.Reverse();

var results = RaiseGenerator.Generate(myDicePool,10);

Console.WriteLine();
Console.WriteLine($"Dice Pool = { string.Join(",", myDicePool)}");
Console.WriteLine($"Dice Rolled = {myDicePool.Count}");
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
Dice Pool = 9,7,7,6,5,4,3,2,2
Dice Rolled = 9
Raise Groups = 3
7,3
6,4
7,5,2,2

Traitor Dice
9

9,7,7,6,5,4,3,2,2



*/