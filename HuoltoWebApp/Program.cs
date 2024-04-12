using HuoltoWebApp.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using HuoltoWebApp.Data;
using HuoltoWebApp.Areas.Identity.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

// Lis‰t‰‰n tietokantaan yhteys
builder.Services.AddDbContext<HuoltoContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))); // ConnectionString lˆytyy appsettings.jsonista

// Konfiguroi Identity k‰ytt‰m‰‰n HuoltoWebAppUser ja IdentityRole
builder.Services.AddIdentity<HuoltoWebAppUser, IdentityRole>()
    .AddDefaultUI()
    .AddEntityFrameworkStores<HuoltoContext>()
    .AddDefaultTokenProviders();

// Lis‰‰ autentikaatio ja autorisoinyi
builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

var app = builder.Build();

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

app.MapRazorPages();

app.Run();