using afi_demo.Classes;
using afi_demo.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCustomServices(builder.Configuration);

var app = builder.Build();

app.UseExceptionHandler();

app.RegisterDemoEndPoints();

app.MapGet("/", () => "I'm here!").ExcludeFromDescription();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

app.UseHttpsRedirection();


app.Run();


