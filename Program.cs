using Microsoft.EntityFrameworkCore;
using Set.Data;
using Set.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
//builder.Services.AddSingleton<ITodoRepository, TodoRepository>();
builder.Services.AddScoped<IGameRepo, GameRepo>();

builder.Services.AddDbContext<TodoContext>(opt =>
    opt.UseInMemoryDatabase("myDb"));
builder.Services.AddDbContext<CardContext>(opt =>
    opt.UseInMemoryDatabase("myDb"));
builder.Services.AddDbContext<GameContext>(opt =>
    opt.UseInMemoryDatabase("myDb"));
    
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Middleware. Middlewares run in the order they are added.
#region Middleware
    app.Use(async (context, next) =>
    {
        Console.WriteLine("Request received, middleware pipeline executing...");

        await next(); // Call the next delegate/middleware in the pipeline
    });
#endregion

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
} else {
    // app.UseHsts();
    app.UseHttpsRedirection();
    app.UseCors();
    app.UseAuthentication();
    app.UseExceptionHandler();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
