using Microsoft.EntityFrameworkCore;
using Hotellerie_X.Models.HotellerieModel;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<HotellerieDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("HotellerieConnection")));

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Hotels}/{action=Index}/{id?}");

app.Run();
