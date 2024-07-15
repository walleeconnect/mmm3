using ModuleA1.API;
using ModuleA2.API;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddModuleA1();
builder.Services.AddModuleA2();
builder.Services.AddControllers().AddApplicationPart(typeof(ModuleA1.API.ModuleA1ServiceCollectionExtensions).Assembly);
builder.Services.AddControllers().AddApplicationPart(typeof(ModuleA2.API.ModuleA2Controller).Assembly);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
