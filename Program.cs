using Microsoft.EntityFrameworkCore;
using Set.Data;
using Set.Models;
using Set.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Transient: A new instance is provided to every controller and every service.
// Scoped: A new instance is provided to every controller and every service, but only one instance is provided within the same scope.
// Singleton: A single instance is provided for every request and every application.
builder.Services.AddScoped<IGameRepo, GameRepo>();
builder.Services.AddScoped<IPlayService, PlayService>();
// builder.Services.AddScoped<ICardRepo, CardRepo>();
//builder.Services.AddScoped<iTestRepo, TestRepo>();

//TODO: Migrate do MsSQL Server
builder.Services.AddDbContext<SetContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("MsSQL")));
    
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddMvc(option => option.EnableEndpointRouting = false).AddNewtonsoftJson(opt => opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

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
