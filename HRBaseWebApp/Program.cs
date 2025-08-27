using Business.Interfaces;
using Business.Services;
using Data.Contexts;
using Data.Interfaces;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

var MyAllowSpecificOrigins = "_myAllowSpecificOrgins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,

        policy =>
        {
            policy.WithOrigins("http://localhost:5173", "http://localhost:5174").AllowAnyHeader().AllowAnyMethod();
        });
});

builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddControllersWithViews();


//var serviceCollection = new ServiceCollection();
//serviceCollection.AddDbContext<DataContext>(options => options.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Projekt\HRBase\Data\Databases\HRData.mdf;Integrated Security=True;Connect Timeout=30"));
//var serviceProvider = serviceCollection.BuildServiceProvider();

builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Projekt\HRBase\Data\Databases\HRData.mdf;Integrated Security=True;Connect Timeout=30"));

builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IEmploymentRepository, EmploymentRepository>();

builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IEmploymentService, EmploymentService>();


var app = builder.Build();
app.MapOpenApi();
app.UseHttpsRedirection();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}



app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Employee}/{action=Index}/{id?}")
    .WithStaticAssets();

app.UseCors(MyAllowSpecificOrigins);
app.Run();
