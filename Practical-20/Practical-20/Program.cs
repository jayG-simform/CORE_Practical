using Microsoft.EntityFrameworkCore;
using Practical_20.Data;
using Practical_20.Execption;
using Practical_20.Interface;
using Practical_20.Services;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Services.AddControllersWithViews();
builder.Services.AddMvc();

builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));

builder.Services.AddDbContext<AddDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseExceptionHandlerMiddleware();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
