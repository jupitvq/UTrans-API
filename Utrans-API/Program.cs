using Microsoft.Extensions.Configuration;
using Utrans_API.DBContexts;
using Microsoft.EntityFrameworkCore;
using Utrans_API.Repository;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var serverVersion = new MySqlServerVersion(new Version(8, 0, 34));
var connectionString = "server=localhost;user=root;password=****;database=utrans";

//builder.Services.AddDbContext<BrandContext>(options => options.UseMySQL(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.Add(new ServiceDescriptor(typeof(BrandContext), new BrandContext()));
builder.Services.AddDbContext<BrandContext>(options => options.UseMySql(connectionString, serverVersion));

builder.Services.AddScoped<IBrandRepository, BrandRepository>();

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
