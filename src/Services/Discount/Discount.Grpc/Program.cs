
using Discount.Grpc.Data;
using Discount.Grpc.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc();
builder.Services.AddDbContext<DiscountContext>(opts =>
        opts.UseSqlite(builder.Configuration.GetConnectionString("Database")));

builder.Services.AddGrpcReflection();

var app = builder.Build();

if (app.Environment.IsDevelopment())

    app.MapGrpcReflectionService();

app.UseMigration();
// Configure the HTTP request pipeline.
app.MapGrpcService<DiscountService>();

app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
