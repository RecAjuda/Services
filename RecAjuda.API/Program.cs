using Weasel.Core;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCarter();

builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssemblies(typeof(Program).Assembly);
});

builder.Services.AddMarten(options =>
{
    options.Connection(builder.Configuration.GetConnectionString("Development") ?? string.Empty);
    options.AutoCreateSchemaObjects = AutoCreate.CreateOrUpdate;
}).UseLightweightSessions();

var app = builder.Build();

app.MapCarter();
app.Run();
