// ------------------------------------------------
//  Student Name: Spyros Mitsis
//  Student ID: 2228036
//  Course : Object Oriented Programming SWE4005
//  Professor: Hector Gatsos
//  Date 23/05/2023
// ------------------------------------------------

using hospitals.Data;
using hospitals.Interfaces;
using hospitals.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Configuration;
using Serilog;

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(configuration)
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);


builder.Host.UseSerilog();



// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IDoctorRepository, DoctorRepository>();
builder.Services.AddScoped<INurseRepository, NurseRepository>();
builder.Services.AddScoped<IPatientRepository, PatientRepository>();
builder.Services.AddScoped<IAddressRepository, AddressRepository>();
builder.Services.AddScoped<IRoomRepository, RoomRepository>();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("defaultConnection"));
});

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

// app.UseSerilogRequestLogging();

app.UseRouting();
app.UseStaticFiles();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "readme",
        pattern: "readme",
        defaults: new { controller = "Home", action = "Readme" }
    );
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
