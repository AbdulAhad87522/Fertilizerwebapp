//using WebApplication1.DAl;
//using WebApplication1.Interfaces;

using Microsoft.AspNetCore.Authentication.Cookies;
using WebApplication1.DAL;
using WebApplication1.DAL.nadirlogin;
using WebApplication1.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// ✅ Register HttpContextAccessor
builder.Services.AddHttpContextAccessor();

// ✅ Add Session support
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // session timeout
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
}).AddCookie(options =>
{
    options.AccessDeniedPath = "/Nadir/AccessDenied";
    options.LoginPath = "/Nadir/Login";
    options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
    options.SlidingExpiration = true;

}).AddGoogle(options =>
{
    options.ClientId = builder.Configuration["Authentication:Google:ClientId"];
    options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
    options.CallbackPath="/signin-google";
}).AddGitHub(options =>
{

    options.ClientId = builder.Configuration["Authentication:GitHub:ClientId"];
    options.ClientSecret = builder.Configuration["Authentication:GitHub:ClientSecret"];
    options.CallbackPath = "/signin-github";
});



// ✅ Initialize DatabaseHelper BEFORE building the app
var connectionString = builder.Configuration.GetConnectionString("MyConnection");
DatabaseHelper.Init(connectionString);

builder.Services.AddScoped<IuserDAL , UsersDAL>();
builder.Services.AddScoped<Icustomer,Customerdal>();
builder.Services.AddScoped<IUSERDALinter,USERDAL>();
builder.Services.AddTransient<EmailService>();
var app = builder.Build();


app.UseSession();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

 // ✅ session should be after routing
app.UseAuthentication(); // ✅ needed for Google/GitHub
app.UseAuthorization();
app.UseSession();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Customer}/{action=Add}/{id?}");


app.Run();
