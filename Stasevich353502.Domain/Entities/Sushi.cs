namespace Stasevich353502.Domain.Entities;

public class Sushi : Entity
{
    const int MaxSushiAmount = 30;
    
    private Sushi()
    {
        
    }

    public Sushi(SushiData sushiData, int? amount = 0)
    {
        Amount = amount.Value;
        SushiData = sushiData;
        SushiSetId = Guid.Empty;
    }
    public int Amount { get; private set; }
    public SushiData SushiData { get; private set; }
    public Guid SushiSetId { get; private set; }
    
    public SushiSet? SushiSet { get; set; }

    public void AddToSet(Guid sushiSetId)
    {
        if (sushiSetId != Guid.Empty)
        {
            SushiSetId = sushiSetId;
        }
    }

    public void RemoveFromSet()
    {
        SushiSetId = Guid.Empty;
    }

    public void ChangeAmount(int amount)
    {
        if (0 < amount && amount <= MaxSushiAmount)
            Amount = amount;
    }
}

public sealed record SushiData(string SushiName, string SushiType);