using Microsoft.EntityFrameworkCore;
using TodoApi.Models;
using CardApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<TodoContext>(opt =>
    opt.UseInMemoryDatabase("myDb"));
builder.Services.AddDbContext<CardContext>(opt =>
    opt.UseInMemoryDatabase("myDb"));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseDeveloperExceptionPage();
} else {
    // In Prod, use HSTS
    // app.UseHsts();
    // In Prod, use HTTPS Redirection
    app.UseHttpsRedirection();
    // In Prod, use CORS
    app.UseCors();
    // In Prod, use Authentication
    app.UseAuthentication();
    // In Prod, use Authorization
    app.UseAuthorization();
    // In Prod, use Exception Handling
    app.UseExceptionHandler();
    // In Prod, use Routing
    app.UseRouting();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
