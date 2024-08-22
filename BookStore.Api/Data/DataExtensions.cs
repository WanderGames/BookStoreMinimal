using Microsoft.EntityFrameworkCore;

namespace BookStore.Api.Data;

public static class DataExtensions
{
    //this extension method upgrades our db when we do a migration so we dont have to keep 
    //manually doing it in the cli each time
    public static void MigrateDb(this WebApplication app)
    {
        //every thing that deals with db has to be scoped
        //this tells asp/net core to provide us a scoped service
        using var scope = app.Services.CreateScope();
        //tell our scope we want to get our DbContext service (BookStoreContext) and save it to a variable so we can use it
        var dbContext = scope.ServiceProvider.GetRequiredService<BookStoreContext>();
        
        //apply any pending migrations to our db
        dbContext.Database.Migrate();
    }
}
