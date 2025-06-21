using CleanArchMvc.Infra.Ioc;

var builder = WebApplication.CreateBuilder(args);

// Adicione a configura��o de CORS lendo do appsettings.json
var allowedOrigins = builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins", policy =>
    {
        policy.WithOrigins(allowedOrigins ?? new string[] { })
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Adiciona a configura��o de inje��o de depend�ncia
builder.Services.AddInfrastructureApi(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Ative a pol�tica de CORS
app.UseCors("AllowSpecificOrigins");

app.UseAuthorization();
app.MapControllers();

app.Run();
