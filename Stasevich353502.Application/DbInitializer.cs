using Microsoft.Extensions.DependencyInjection;

namespace Stasevich353502.Application;

public static class DbInitializer
{
    public static async Task Initialize(IServiceProvider services)
    {
        var UoW = services.GetRequiredService<IUnitOfWork>();
        
        await UoW.DeleteDatabaseAsync();
        await UoW.CreateDatabaseAsync();

        var set1 = new SushiSet("name1", 10, 120);
        var set2 = new SushiSet("name2", 20, 150);
        await UoW.SushiSetRepository.AddAsync(set1);
        await UoW.SushiSetRepository.AddAsync(set2);

        await UoW.SaveAllAsync();

        var sushi1 = new Sushi(new SushiData("sushi1", "1"), 10);
        sushi1.AddToSet(set1.Id);
        var sushi2 = new Sushi(new SushiData("sushi2", "2"), 2);
        sushi2.AddToSet(set1.Id);
        var sushi3 = new Sushi(new SushiData("sushi3", "3"), 4);
        sushi3.AddToSet(set2.Id);
        var sushi4 = new Sushi(new SushiData("sushi4", "4"), 3);
        sushi4.AddToSet(set2.Id);

        await UoW.SushiRepository.AddAsync(sushi1);
        await UoW.SushiRepository.AddAsync(sushi2);
        await UoW.SushiRepository.AddAsync(sushi3);
        await UoW.SushiRepository.AddAsync(sushi4);

        // TODO Add sets
        // TODO Add sushi

        await UoW.SaveAllAsync();
    }
}