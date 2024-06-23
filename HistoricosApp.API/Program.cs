using HistoricosApp.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddRouting(config => { config.LowercaseUrls = true; });
builder.Services.AddSwaggerDoc();
builder.Services.AddCorsConfig();
builder.Services.AddDatabaseConfig(builder.Configuration);
builder.Services.AddMessagesConfig(builder.Configuration);

var app = builder.Build();

app.UseAuthorization();
app.UseSwaggerDoc();
app.UseCorsConfig();
app.MapControllers();
app.Run();
