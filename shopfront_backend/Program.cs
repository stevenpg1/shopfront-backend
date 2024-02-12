using Microsoft.EntityFrameworkCore;
using shopfront_backend.DatabaseContext;

var FromReactOrigins = "_fromReactOrigins";
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: FromReactOrigins,
        policy =>
        {
            policy.WithOrigins("http://localhost:3000").AllowAnyHeader().AllowAnyMethod();
        });
});
builder.Services.AddControllers();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("default"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseHsts(); //prevents sending communication over http ie enforces https
app.UseHttpsRedirection();

app.UseCors(FromReactOrigins);

app.UseAuthorization();

app.MapControllers();

app.Run();
