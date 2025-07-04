using FastTechFoods.ProductsService.API.Configurations;
using FastTechFoods.SDK;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

if (builder.Environment.IsProduction())
{
    builder.WebHost.ConfigureKestrel(options =>
    {
        options.ListenAnyIP(80); 
    });
}

// Registro das dependÍncias customizadas
builder.Services.AddServices(builder.Configuration);
builder.Services.AddAutentication(builder.Configuration);
builder.Services.AddRabbitMqEventSubscriber();

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