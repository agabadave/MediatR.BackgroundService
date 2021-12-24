using MediatR;
using MediatR.BackgroundService;
using MediatR.BackgroundService.SampleApi.BusinessLogic.Handlers.LongOperation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//mediatr setup
builder.Services.AddMediatR(typeof(LongOperationHandler));

//setup background services configuration
builder.Services.ConfigureBackgroundServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
