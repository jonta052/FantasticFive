using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NewsApp.Data;
using NewsApp.Models;
using NewsApp.Services;
using System.Net.Http.Headers;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseLazyLoadingProxies().UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<User>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    options.Password.RequireDigit = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 4;
    options.Password.RequireUppercase = false;
})
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

//Add services here
builder.Services.AddScoped<IArticleService, ArticleService>();
builder.Services.AddScoped<ICommentService, CommentService>();
builder.Services.AddScoped<IWeatherService, WeatherService>();
builder.Services.AddScoped<ISubscriptionService, SubscriptionService>();
builder.Services.AddScoped<IEmailService, EmailService>();

builder.Services.AddHttpClient("stockMarket", config => {

    config.BaseAddress = new("https://lexnewsbizdata.azurewebsites.net/");

});

builder.Services.AddHttpClient("weatherForecast", config => {

    config.BaseAddress = new("https://weatherapi.dreammaker-it.se/");

});

builder.Services.AddHttpClient("getUserLocationInfo", config => {

    config.BaseAddress = new("https://ipinfo.io/");

});

builder.Services.AddHttpClient("ConfirmHttp", config => {

    config.BaseAddress = new(builder.Configuration["MyLocalFuncAddress"]);

});
//KLARNA STUFF START//
var klarnaUid = builder.Configuration["KlarnaUid"];
var klarnaPass = builder.Configuration["KlarnaPassword"];
var bytes = Encoding.UTF8.GetBytes($"{klarnaUid}:{klarnaPass}");
var auth = Convert.ToBase64String(bytes);
builder.Services.AddHttpClient("klarna-payment", config =>
{
    config.BaseAddress = new("https://api.playground.klarna.com/payments/v1/");
    config.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", auth);
    config.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
});
builder.Services.AddHttpClient("klarna-order", config =>
{
    config.BaseAddress = new("https://api.playground.klarna.com/ordermanagement/v1/");
    config.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", auth);
    config.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
});
builder.Services.AddScoped<IKlarnaService, KlarnaService>();

builder.Services.AddSession(
    options =>
    {
        options.Cookie.HttpOnly = true;
        options.IdleTimeout = TimeSpan.FromSeconds(600);
        options.Cookie.IsEssential = true;
    });

//KLARNA STUFF END//
var app = builder.Build();

//Adds the required data to the database
await Seeder.Seed(app);

//Initializes seeding data from project models
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    SeedData.Initialize(services);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
