using ProcureFlow.API.Common.Exceptions;
using ProcureFlow.Application;
using ProcureFlow.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Application
builder.Services.AddApplication();

// Infrastructure
builder.Services.AddInfrastructure(builder.Configuration);

// Global Exception Handler
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Global Exception Handling
//app.UseExceptionHandler();
app.UseExceptionHandler(_ => { });

app.UseAuthorization();

app.MapControllers();

app.Run();