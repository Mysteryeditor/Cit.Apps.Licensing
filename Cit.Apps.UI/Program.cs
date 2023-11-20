using Cit.Apps.Licensing.Application.Extensions;
using Cit.Apps.Licensing.Persistence.Contexts;
using Cit.Apps.Licensing.Persistence.Extensions;
using Cit.Apps.Licensing.Shared.Password;
using Cit.Apps.Licensing.UI.Mappings;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using NToastNotify;


var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
#pragma warning disable CS0618 // Type or member is obsolete
builder.Services.AddRazorPages().AddNToastNotifyNoty(new NotyOptions
{
    ProgressBar = true,
    Timeout = 5000,

});

builder.Services.AddAuthentication(
    CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(
    option =>
    {
        option.LoginPath = "/Authentication/Login";
        option.ExpireTimeSpan = TimeSpan.FromMinutes(5);
    });
// Add distributed memory cache for session
builder.Services.AddDistributedMemoryCache();

// Configure session options
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(10); // Set session timeout
});
#pragma warning restore CS0618 // Type or member is obsolete
var connectionString = builder.Configuration.GetConnectionString("ConnectionString");

builder.Services.AddDbContext<DatabaseContext>(options =>
   options.UseSqlServer(connectionString,
       builder => builder.MigrationsAssembly(typeof(DatabaseContext).Assembly.FullName)));
builder.Services.AddAutoMapper(config =>
{
    config.AddProfile<CommonMappings>();
});
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddPersistenceLayer(builder.Configuration);
builder.Services.AddApplicationLayer();
builder.Services.AddTransient<IPasswordService, PasswordService>();
builder.Services.AddControllersWithViews();





var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();

}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthentication();
app.UseRouting();
app.MapRazorPages();
app.UseAuthorization();
app.UseNToastNotify();
app.UseSession();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
