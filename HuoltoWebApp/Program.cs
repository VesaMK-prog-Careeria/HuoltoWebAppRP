using HuoltoWebApp.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using HuoltoWebApp.Data;
using HuoltoWebApp.Areas.Identity.Data;
using Microsoft.Extensions.DependencyInjection;
using HuoltoWebApp.Pages;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();  // Lis‰t‰‰n Razor-sivujen runtime-k‰‰nnˆs

// Lis‰t‰‰n tietokantaan yhteys
builder.Services.AddDbContext<HuoltoContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))); // ConnectionString lˆytyy appsettings.jsonista

// Lis‰t‰‰n ImageService
builder.Services.AddTransient<ImageService>();

// Konfiguroi Identity k‰ytt‰m‰‰n HuoltoWebAppUser ja IdentityRole
builder.Services.AddIdentity<HuoltoWebAppUser, IdentityRole>()
    .AddDefaultUI()
    .AddEntityFrameworkStores<HuoltoContext>()
    .AddDefaultTokenProviders();

// Lis‰‰ autentikaatio ja autorisoinyi
builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

var app = builder.Build();

// Lis‰t‰‰n roolit ja k‰ytt‰j‰t
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<HuoltoWebAppUser>>();

    // Lis‰‰ roolit, jos niit‰ ei ole viel‰ olemassa
    if (!roleManager.RoleExistsAsync("Admin").Result)
    {
        roleManager.CreateAsync(new IdentityRole("Admin")).Wait();
    }

    if (!roleManager.RoleExistsAsync("User").Result)
    {
        roleManager.CreateAsync(new IdentityRole("User")).Wait();
    }

    // Lis‰‰ k‰ytt‰j‰t rooleihin
    var adminUser = userManager.FindByNameAsync("admin").Result;
    if (adminUser != null && !userManager.IsInRoleAsync(adminUser, "Admin").Result)
    {
        userManager.AddToRoleAsync(adminUser, "Admin").Wait();
    }

    var regularUser = userManager.FindByNameAsync("user").Result;
    if (regularUser != null && !userManager.IsInRoleAsync(regularUser, "User").Result)
    {
        userManager.AddToRoleAsync(regularUser, "User").Wait();
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapRazorPages();
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});
app.MapRazorPages();

app.Run();