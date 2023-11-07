using AspNetCoreHero.ToastNotification;
using Microsoft.EntityFrameworkCore;
using QuanLyBanSach.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Services.AddControllersWithViews();
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
builder.Services.AddNotyf(config => { config.DurationInSeconds = 3; config.IsDismissable = true; config.Position = NotyfPosition.TopRight; });
builder.Services.AddDbContext<QlbanSachContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("QlBanSachConnectionString")));
builder.Services.AddCors(p => p.AddPolicy("MyCors", build =>
{
	build.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));
builder.Services.AddSession();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseCors("MyCors");
app.UseStaticFiles();

app.UseRouting();
app.UseSession();
app.UseAuthorization();

app.MapControllerRoute(
	name: "MyArea",
	pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
	name: "default",
     //pattern: "{controller=Home}/{action=Index}/{id?}");
     pattern: "{controller=Access}/{action=Login}/{id?}");

app.Run();
