namespace DramaDice.Services;

public class RaiseResults
{
    public List<List<int>> RaiseSets { get; }
    public List<int> TraitorDice { get; }
    public RaiseResults(IEnumerable<List<int>> raiseSets, IEnumerable<int> traitorDice)
    {
        RaiseSets = raiseSets.ToList();
        TraitorDice = traitorDice.ToList();
    }
}