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
    
    public void UpdateCoreData(string newSushiName, string newSushiType)
    {
        if (string.IsNullOrWhiteSpace(newSushiName))
            throw new ArgumentException("Название суши не может быть пустым.", nameof(newSushiName));
        if (string.IsNullOrWhiteSpace(newSushiType))
            throw new ArgumentException("Тип суши не может быть пустым.", nameof(newSushiType));
            
        SushiData = new SushiData(newSushiName, newSushiType);
    }
    
    public void ChangeAmount(int amount)
    {
        if (0 < amount)
            Amount = int.Max(amount, MaxSushiAmount);
        
    }
}

public sealed record SushiData(string SushiName, string SushiType);