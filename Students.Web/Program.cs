using Students.Infrastructure;
using Students.Application;
using Serilog;
using Students.Web;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.SerilogConfiguration();

builder.Services.AddControllers();

builder.Services.AddApplicationService();
builder.Services.AddInfrastructureService(builder.Configuration);
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

var app = builder.Build();


app.UseSerilogRequestLogging();
app.UseExceptionHandler(err => { });
// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
