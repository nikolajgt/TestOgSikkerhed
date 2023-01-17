using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using TestOgSikkerhed.Areas.Identity;
using TestOgSikkerhed.Data;
using TestOgSikkerhed.Models;

var builder = WebApplication.CreateBuilder(args);

// Our connection string to our local db. It uses ApplicationDbContext for initiating different tables from our models
// it uses EF core with sql server and uses tool to do the table creatin
// add-migration {NameOfMigration} and then update-database to update.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));


builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// This is where our special rules for user, password and 2factor comes in
builder.Services.AddIdentityCore<IdentityUser>(i =>
{
    i.Password.RequireDigit = false;
    i.Password.RequireNonAlphanumeric = false;
    i.Password.RequireUppercase = false;
    i.Password.RequiredLength = 1;
    i.Password.RequireLowercase = false;
    i.User.RequireUniqueEmail = false;
    i.SignIn.RequireConfirmedPhoneNumber = false;
    i.SignIn.RequireConfirmedEmail = false;
}).AddRoles<IdentityRole>()
  .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddAuthorization(opt => opt.AddPolicy("TwoFactorEnabled", x => x.RequireClaim("amr", "mfa")));

//builder.Services.AddIdentityCore<User>(i =>
//{
//    i.Password.RequireDigit = true;
//    i.Password.RequireNonAlphanumeric = true;
//    i.Password.RequireUppercase = true;
//    i.Password.RequiredLength = 12;
//    i.Password.RequireLowercase = true;
//    i.User.RequireUniqueEmail = true;
//    i.SignIn.RequireConfirmedPhoneNumber = true;
//    i.SignIn.RequireConfirmedEmail = true;
//}).AddEntityFrameworkStores<ApplicationDbContext>();

//builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
//    .AddEntityFrameworkStores<ApplicationDbContext>();


// From where is standatrd blazor settings exceptp our Authentication DI that controls user state
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
// This is the one im talking a
builder.Services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<IdentityUser>>();
builder.Services.AddSingleton<WeatherForecastService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
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

app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
