var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient("SchoolAPI", client =>
{
    client.BaseAddress = new Uri("https://localhost:7043/");
});

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=SchoolClient}/{action=GetAllSchools}/{id?}");

app.Run();
