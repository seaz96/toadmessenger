using Application;
using Core.HttpLogic;
using Core.Logs;
using Serilog;
using Core.TraceIdLogic;
using Infrastructure;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((hostingContext, loggerConfig) =>
{
    loggerConfig.GetConfiguration();
});

builder.Services.AddControllers();
builder.Services.AddAuthentication();
builder.Services.AddHttpContextAccessor();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.TryAddInfrastructureServices();
builder.Services.TryAddApplicationServices();
builder.Services.AddHttpRequestService();
builder.Services.TryAddTraceId();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.UseMiddleware<ReadTraceIdMiddleware>();
app.UseMiddleware<WriteTraceIdMiddleware>();

app.Run();