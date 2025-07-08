using FastTechFoods.ProductsService.API;
using FastTechFoods.ProductsService.API.Configurations;
using FastTechFoods.SDK;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Registro das dependências customizadas
builder.Services.AddServices(builder.Configuration);
builder.Services.AddMessaging();
builder.Services.AddAutentication(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseGlobalExceptionHandler();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();