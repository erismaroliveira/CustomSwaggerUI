using CustomSwaggerUI.Filters;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Swagger WeatherForecast",
        Version = "1.0.7",
        Description = "This is a sample server WeatherForecast server. You can find out more about Swagger at [Swagger Official Site](http://swagger.io) or on [IRC freenode.net, #swagger](http://swagger.io). " +
                          "For this sample, you can use the API key `special-key` to test the authorization filters.",
        TermsOfService = new Uri("http://swagger.io/terms/"),
        Contact = new OpenApiContact
        {
            Name = "Developer",
            Email = "developer@weatherforecast.swagger.io"
        },
        License = new OpenApiLicense
        {
            Name = "Apache 2.0",
            Url = new Uri("http://www.apache.org/licenses/LICENSE-2.0.html")
        }
    });
    c.EnableAnnotations();
    c.AddServer(new OpenApiServer
    {
        Url = "http://localhost:5000",
        Description = "Servidor local (HTTP)"
    });
    c.AddServer(new OpenApiServer
    {
        Url = "https://localhost:5001",
        Description = "Servidor local (HTTPS)"
    });
    c.AddSecurityDefinition("api_key", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Name = "api_key",
        Description = "Insira sua chave de API"
    });
    c.OperationFilter<AuthorizeCheckOperationFilter>();
    c.OperationFilter<ValidationOperationFilter>();
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
        c.InjectStylesheet("/swagger-ui/custom.css");
        c.InjectJavascript("/swagger-ui/custom.js");
        c.DocumentTitle = "API Documentation - Personalizada";
        c.DefaultModelsExpandDepth(-1);
    });
}

app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
