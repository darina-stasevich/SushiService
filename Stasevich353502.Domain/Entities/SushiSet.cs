namespace Stasevich353502.Domain.Entities;

public class SushiSet : Entity
{
    private List<Sushi> _sushi = new List<Sushi>();

    private SushiSet()
    {
        
    }

    public SushiSet(string name, decimal price, decimal weight)
    {
        Name = name;
        Price = price;
        Weight = weight;
    }
    
    public string Name { get; private set; }
    public decimal Price { get; private set; }
    public decimal Weight { get; private set; }
    
    public IReadOnlyList<Sushi> GetSushi => _sushi.AsReadOnly();
}