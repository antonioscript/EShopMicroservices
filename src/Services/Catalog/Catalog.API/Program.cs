using BuildingBlocks.Bahaviors;
using BuildingBlocks.Exceptions.Handler;
using Microsoft.AspNetCore.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

//Add services to the container
var assembly = typeof(Program).Assembly;
builder.Services.AddValidatorsFromAssembly(assembly);
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
});

builder.Services.AddCarter();

builder.Services.AddMarten(opts =>
{
    opts.Connection(builder.Configuration.GetConnectionString("Database")!);
}).UseLightweightSessions();

builder.Services.AddExceptionHandler<CustomExceptionHandler>();


var app = builder.Build();

//Configure the HTTP request pipeline
app.MapCarter();
app.UseExceptionHandler(options => { });

app.Run();
