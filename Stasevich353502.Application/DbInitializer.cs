using Microsoft.Extensions.DependencyInjection;

namespace Stasevich353502.Application;

public static class DbInitializer
{
    public static async Task Initialize(IServiceProvider services)
    {
        var UoW = services.GetRequiredService<IUnitOfWork>();

        await UoW.CreateDatabaseAsync();
        
        if((await UoW.SushiRepository.ListAllAsync()).Count != 0)
        {
            return;
        }
        
        var set1 = new SushiSet("Сет для двоих", 15, 800);
        var set2 = new SushiSet("Вегетарианский сет", 12, 700);
        var set3 = new SushiSet("Морской сет", 18, 900);
        var set4 = new SushiSet("Экзотический", 20, 950);
        var set5 = new SushiSet("Сет для вечеринки", 25, 1200);
        
        await UoW.SushiSetRepository.AddAsync(set1);
        await UoW.SushiSetRepository.AddAsync(set2);
        await UoW.SushiSetRepository.AddAsync(set3);
        await UoW.SushiSetRepository.AddAsync(set4);    
        await UoW.SushiSetRepository.AddAsync(set5);

        await UoW.SaveAllAsync();

        var sushi11 = new Sushi(new SushiData("с лососем", "нигири"), 4);
        sushi11.AddToSet(set1.Id);
        var sushi12 = new Sushi(new SushiData("Калифорния", "урамаки"), 8);
        sushi12.AddToSet(set1.Id);
        var sushi13 = new Sushi(new SushiData("Филадельфия", "урамаки"), 4);
        sushi13.AddToSet(set1.Id);
        
        var sushi21 = new Sushi(new SushiData("Овощной ролл", "хосомаки"), 6);
        sushi21.AddToSet(set2.Id);
        var sushi22 = new Sushi(new SushiData("с авокадо", "нигири"), 4);
        sushi22.AddToSet(set2.Id);

        var sushi31 = new Sushi(new SushiData("с тунцом", "сашими"), 6);
        sushi31.AddToSet(set3.Id);
        var sushi32 = new Sushi(new SushiData("тунцовый ролл", "хосомаки"), 4);
        sushi32.AddToSet(set3.Id);
        var sushi33 = new Sushi(new SushiData("с креветкой", "нигири"), 4);
        sushi33.AddToSet(set3.Id);
        
        var sushi41 = new Sushi(new SushiData("темпура ролл", "маки"), 4);
        sushi41.AddToSet(set4.Id);
        var sushi42 = new Sushi(new SushiData("с угрем", "нигири"), 4);
        sushi42.AddToSet(set4.Id);
        var sushi43 = new Sushi(new SushiData("с осьминогом", "сашими"), 6);
        sushi43.AddToSet(set4.Id);
        
        var sushi51 = new Sushi(new SushiData("Калифорния с крабом", "урамаки"), 10);
        sushi51.AddToSet(set5.Id);
        var sushi52 = new Sushi(new SushiData("с лососем", "нигири"), 6);
        sushi52.AddToSet(set5.Id);

        await UoW.SushiRepository.AddAsync(sushi11);
        await UoW.SushiRepository.AddAsync(sushi12);
        await UoW.SushiRepository.AddAsync(sushi13);
        await UoW.SushiRepository.AddAsync(sushi21);
        await UoW.SushiRepository.AddAsync(sushi22);
        await UoW.SushiRepository.AddAsync(sushi31);
        await UoW.SushiRepository.AddAsync(sushi32);
        await UoW.SushiRepository.AddAsync(sushi33);
        await UoW.SushiRepository.AddAsync(sushi41);
        await UoW.SushiRepository.AddAsync(sushi42);
        await UoW.SushiRepository.AddAsync(sushi43);
        await UoW.SushiRepository.AddAsync(sushi51);
        await UoW.SushiRepository.AddAsync(sushi52);

        await UoW.SaveAllAsync();
    }
}