using Utrans_API.DBContexts;
using Microsoft.EntityFrameworkCore;
using Utrans_API.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var serverVersion = new MySqlServerVersion(new Version(8, 0, 34));
var connectionString = "server=localhost;user=root;password=;database=utrans";

//builder.Services.AddDbContext<BrandContext>(options => options.UseMySQL(builder.Configuration.GetConnectionString("DefaultConnection")));

// Vendors Scopes, Contexts and Repositories
builder.Services.AddScoped<IVendorRepository, VendorRepository>();
builder.Services.Add(new ServiceDescriptor(typeof(VendorContext), new VendorContext()));
builder.Services.AddDbContext<VendorContext>(options => options.UseMySql(connectionString, serverVersion));

// Customers Scopes, Contexts and Repositories
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.Add(new ServiceDescriptor(typeof(CustomerContext), new CustomerContext()));
builder.Services.AddDbContext<CustomerContext>(options => options.UseMySql(connectionString, serverVersion));

// Products Scopes, Contexts and Repositories
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.Add(new ServiceDescriptor(typeof(ProductContext), new ProductContext()));
builder.Services.AddDbContext<ProductContext>(options => options.UseMySql(connectionString, serverVersion));

// Brands Scopes, Contexts and Repositories
builder.Services.AddScoped<IBrandRepository, BrandRepository>();
builder.Services.Add(new ServiceDescriptor(typeof(BrandContext), new BrandContext()));
builder.Services.AddDbContext<BrandContext>(options => options.UseMySql(connectionString, serverVersion));


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
