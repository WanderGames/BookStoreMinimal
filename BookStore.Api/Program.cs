using BookStore.Api.Data;
using BookStore.Api.Endpoints;

var builder = WebApplication.CreateBuilder(args);

//connection string for sqllite, source lets us say what path to save to and name our db
// use our connection string we made in our appsettings.json config file
//"Data Source=GameStore.db";
var connectionString = builder.Configuration.GetConnectionString("BookStore");
//add our sqllite service, this is a scoped service (created and disposed once per http request)
//this is scoped because db conections are limited and expensive, dont do a singleton because dbs are not thread safe
builder.Services.AddSqlite<BookStoreContext>(connectionString);

var app = builder.Build();

//call our extension methods we created to map our endpoints
app.MapBooksEndpoints();
app.MapGenreEndpoints();
//call our extension method we created to update our db with our migrations
await app.MigrateDbAsync();

app.Run();
