using CONSWARE_Vhicles.Data;
using CONSWARE_Vhicles.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Load configuration from appsettings.json
builder.Configuration.AddJsonFile("Config/appsettings.json", optional: true, reloadOnChange: true);

// Aquí puedes registrar tus servicios
builder.Services.AddScoped<DataAccess>();
builder.Services.AddScoped<FuelTypeService>();
builder.Services.AddScoped<VehicleTypeService>();
builder.Services.AddScoped<VehicleService>();
builder.Services.AddScoped<VehicleDetailsService>();


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

