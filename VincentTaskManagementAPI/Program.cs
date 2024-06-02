using log4net;
using log4net.Config;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using VincentTaskManagementAPI.Authentication;
using VincentTaskManagementAPI.Db;
using VincentTaskManagementAPI.HandleException;
using VincentTaskManagementAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//get configuration file
IConfigurationRoot configuration = new ConfigurationBuilder()
								.SetBasePath(Directory.GetCurrentDirectory())
								.AddJsonFile("appsettings.json")
								.Build();

builder.Services.AddControllers(options =>
{
		options.Filters.Add<HttpResponseExceptionFilter>();
		options.Filters.Add<BearerAuthorizeFilter>();
});
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//configure db context
////builder.Services.AddDbContext<VincDbContext>(options =>	options.UseSqlite(configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddDbContext<VincDbContext>(options => options.UseSqlite(@"Data source = "+Directory.GetCurrentDirectory()+ "\\Database\\vincetask.db"));
builder.Services.AddScoped<IPolicyEvaluator, PolicyEvaluator>();
builder.Services.AddScoped<ITaskService, TaskService>();

//Configure log4net
var logRepository = LogManager.GetRepository(System.Reflection.Assembly.GetEntryAssembly());
XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
		app.UseExceptionHandler("/error-development");
		app.UseSwagger();
		app.UseSwaggerUI();
} 
else
{
		app.UseExceptionHandler("/error");
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
