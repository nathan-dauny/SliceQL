var builder = WebApplication.CreateBuilder(args);

// Ajout des services MVC
builder.Services.AddControllersWithViews();

var app = builder.Build();

// 🎯 Port dynamique pour Render (nécessaire)
var port = Environment.GetEnvironmentVariable("PORT") ?? "5000";
app.Urls.Add($"http://0.0.0.0:{port}");

// 📦 Pipeline HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts(); // Sécurité en production
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

// 🌐 Route MVC par défaut
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Query}/{action=Index}/{id?}");

app.Run();
