using PapperCompany.Catalog.API.Extensions;
using Serilog;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add use of 'secrets.yaml' file
builder.Configuration.AddSecrets();

// Add Serilog Configuration
builder.Host.UseSerilog();

// Add Custom Cors Policies
builder.Services.AddCors(builder.Configuration);

// Add Custom API Versioning
builder.Services.AddVersioning(builder.Configuration);

// Add Custom Swagger e UI
builder.Services.AddSwagger(builder.Configuration);

WebApplication app = builder.Build();

app.UseSerilogRequestLogging();

app.UseCors();

app.UseApiVersioning();

app.UseSwagger(builder.Configuration);

app.Run();