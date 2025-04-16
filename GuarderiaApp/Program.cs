using GuarderiaApp.Data;
using Microsoft.EntityFrameworkCore;
using GuarderiaApp;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Configuración de servicios
builder.Services.AddDbContext<GuarderiaDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("GuarderiaDb")));

// Agregar controladores y vistas
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configuración de la pipeline de solicitudes HTTP
if (app.Environment.IsDevelopment()){

    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
string x = "Hola";
Console.WriteLine(x);

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"); 

app.Run();
