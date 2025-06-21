using CleanArchMvc.Infra.Ioc;
using CleanArchMvc.WebApi.Configurations;

var builder = WebApplication.CreateBuilder(args);

// Adicione a configuração de CORS lendo do appsettings.json
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

builder.Services.AddControllers()
                .AddNewtonsoftJson(x =>
                {
                    // Resolvendo problema de referência circular com o Newtonsoft.Json
                    x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                    // Removendo o endereço de categoria quando for nulo (campo utilizado para url de pagina ASP NET)
                    x.SerializerSettings.ContractResolver = new IgnoreCategoryWhenNullResolver();
                });

builder.Services.AddEndpointsApiExplorer();

// Adiciona a configuração do Swagger com JWT
builder.Services.AddInfrastructureSwagger();

// Adiciona a configuração de injeção de dependência
builder.Services.AddInfrastructureApi(builder.Configuration);

// Adiciona a configuração de autenticação JWT
builder.Services.AddInfrastructureJWT(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "CleanArchMvc.API v1");
    });
}

// Adiciona o middleware de tratamento de erros
app.UseHttpsRedirection();

// Ative a política de CORS
app.UseCors("AllowSpecificOrigins");

// Adiciona o middleware de tratamento de erros para códigos de status HTTP
app.UseStatusCodePages();

// Adiciona o middleware de roteamento
app.UseRouting();

// Adiciona o middleware de autenticação JWT antes do middleware de autorização
app.UseAuthentication();

// Adiciona o middleware de autorização
app.UseAuthorization();

// Mapear os controladores para as rotas definidas
app.MapControllers(); 

app.Run();
