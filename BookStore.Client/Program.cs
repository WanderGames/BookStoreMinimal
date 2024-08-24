using BookStore.Client.Clients;
using BookStore.Client.Components;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents();
//register our books client as a singleton so its just one instance accross the app
//this allows us to inject it in our razor components
builder.Services.AddSingleton<BooksClient>();
//register our genres client as a singleton so its just one instance accross the app
//this allows us to inject it in our razor components
builder.Services.AddSingleton<GenresClient>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>();

app.Run();
