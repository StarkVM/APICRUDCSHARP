using Pessoa.Data;
using Pessoa.Routes;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<PessoaContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
//var port = Environment.GetEnvironmentVariable("PORT") ?? "8078";
//app.Urls.Add($"http//*:{port}");

app.UseSwagger();
app.UseSwaggerUI();


app.PessoaRoutes();

app.UseHttpsRedirection();
app.Run();

