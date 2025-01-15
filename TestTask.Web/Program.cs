using TestTask.BLL.Extensions;
using TestTask.DAL.Extensions;
using TestTask.Web.Middleware;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddBllServices();
builder.Services.AddDalRepositories();
builder.Services.AddDalInfrastructute(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (!app.Environment.IsDevelopment())
//{
//	app.UseExceptionHandler("/Home/Error");
//	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//	app.UseHsts();
//}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCustomExceptionHandler();
app.UseRouting();

app.MigrateUp();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Contact}/{action=Index}/{id?}");

app.Run();
