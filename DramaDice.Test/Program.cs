using DramaDice.Generators;

Console.WriteLine();
Console.WriteLine();

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




