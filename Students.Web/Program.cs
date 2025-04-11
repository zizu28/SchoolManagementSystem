using Students.Infrastructure;
using Students.Application;
using Students.Web;
using Core.EventBus;
using Infrastructure.BackgroundJobs.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.SerilogConfiguration();
builder.Services.AddMassTransitWithRabbitMQ(builder.Configuration);
builder.Services.AddControllers();

builder.Services.AddApplicationService();
builder.Services.AddInfrastructureService(builder.Configuration);
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

// Hangfire
builder.Services.AddBackgroundJobs(builder.Configuration);

var app = builder.Build();

app.UseHangfireDashboard();

app.UseExceptionHandler(err => { });
// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
