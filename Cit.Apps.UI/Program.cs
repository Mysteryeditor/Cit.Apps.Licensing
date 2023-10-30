using Cit.Apps.Licensing.Application.Extensions;
using Cit.Apps.Licensing.Persistence.Contexts;
using Cit.Apps.Licensing.Persistence.Extensions;
using Cit.Apps.Licensing.Shared.Password;
using Cit.Apps.Licensing.UI.Mappings;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("ConnectionString");

builder.Services.AddDbContext<DatabaseContext>(options =>
   options.UseSqlServer(connectionString,
       builder => builder.MigrationsAssembly(typeof(DatabaseContext).Assembly.FullName)));
builder.Services.AddAutoMapper(config =>
{
    config.AddProfile<CommonMappings>();
});
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

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "user",
    pattern: "{controller=User}/{action=AllUsers}/{id?}");


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
