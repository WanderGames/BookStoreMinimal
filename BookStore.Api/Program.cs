using BookStore.Api.Endpoints;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

//call our extension method we created to map our endpoints
app.MapBooksEndpoints();

app.Run();
