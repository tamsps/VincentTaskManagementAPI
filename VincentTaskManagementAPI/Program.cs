using log4net;
using log4net.Config;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using VincentTaskManagementAPI.Db;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//get configuration file
IConfigurationRoot configuration = new ConfigurationBuilder()
								.SetBasePath(Directory.GetCurrentDirectory())
								.AddJsonFile("appsettings.json")
								.Build();
//configure db context
////builder.Services.AddDbContext<VincDbContext>(options =>	options.UseSqlite(configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddDbContext<VincDbContext>(options => options.UseSqlite(@"Data source = "+Directory.GetCurrentDirectory()+ "\\Database\\vincetask.db"));

//Configure log4net
var logRepository = LogManager.GetRepository(System.Reflection.Assembly.GetEntryAssembly());
XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
		app.UseSwagger();
		app.UseSwaggerUI();
} 

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
