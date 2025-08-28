//using WebApplication1.DAl;
//using WebApplication1.Interfaces;

using WebApplication1.DAL;
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



// ✅ Initialize DatabaseHelper BEFORE building the app
var connectionString = builder.Configuration.GetConnectionString("MyConnection");
DatabaseHelper.Init(connectionString);

builder.Services.AddScoped<IuserDAL , UsersDAL>();
builder.Services.AddScoped<Icustomer, Customerdal>();
builder.Services.AddScoped<IProductsDAL, Productdal>();
builder.Services.AddScoped<Isupplierdal, SuplierDAL>();

var app = builder.Build();

builder.Services.AddSession();
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

app.UseAuthorization();

// ✅ Enable Session middleware
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Supplier}/{action=Show}/{id?}");

app.Run();
