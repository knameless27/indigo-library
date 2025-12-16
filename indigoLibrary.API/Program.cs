using indigoLibrary.Infrastructure.DependencyInjection;
using indigoLibrary.API.Middlewares;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddInfrastructure();


var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI();


app.UseMiddleware<ExceptionMiddleware>();


app.MapControllers();


app.Run();