var builder = WebApplication.CreateBuilder(args);

// Ajout des services MVC
builder.Services.AddControllersWithViews();

var app = builder.Build();

// 🎯 Port dynamique pour Render uniquement
if (app.Environment.IsDevelopment())
{
    // Rien, comportement ASP.NET Core par défaut (localhost + navigateur)
}
else
{
    var port = Environment.GetEnvironmentVariable("PORT") ?? "5000";
    app.Urls.Add($"http://0.0.0.0:{port}");
}

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
    pattern: "{controller=Query}/{action=Index}/{id?}");

app.Run();