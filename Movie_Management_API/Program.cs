using Movie_Management_API.Classes;
using Movie_Management_API.Services;
using Movie_Management_API.DAOs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<DatabaseHelper>();
builder.Services.AddSingleton<MovieService>();
builder.Services.AddSingleton<InterfaceMovieDAO, MovieDAO>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();             // Enable Swagger JSON
    app.UseSwaggerUI(c =>         // Enable Swagger UI
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Movie Management API v1");
        c.RoutePrefix = string.Empty; // Optional: Swagger at root URL
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
