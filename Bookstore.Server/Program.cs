using Bookstore.Server.Data;
using Bookstore.Server.Data.Models;
using Bookstore.Server.DTO;
using Bookstore.Server.DTOs;
using Bookstore.Server.Repositories;
using Bookstore.Server.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DatabaseContext>(options =>
				options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString")));

builder.Services.AddScoped<IRepository<Book>, BookRepository>();
builder.Services.AddTransient<IBookService, BookService>();

builder.Services.AddScoped<IRepository<Magazine>, MagazineRepository>();
builder.Services.AddTransient<IService<MagazineDTO>, MagazineService>();

builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddTransient<ICategoryService, CategoryService>();

const string policyName = "Policy";

builder.Services.AddCors(options =>
{
	options.AddPolicy(name: policyName,
		policy =>
		{
			policy.WithOrigins("http://localhost:4200");
			policy.WithMethods("GET", "POST", "PUT","HEAD", "DELETE", "OPTIONS");
			policy.WithHeaders("Access-Control-Allow-Headers", "Origin", "Accept", "X-Requested-With", "Content-Type", "Access-Control-Request-Method", "Access-Control-Request-Headers");
		});
});

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(policyName);

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
