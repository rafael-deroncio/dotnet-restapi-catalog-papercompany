using PapperCompany.Catalog.API.Extensions;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add use of 'secrets.yaml' file
builder.Configuration.AddSecrets();

// Add Serilog Configuration
builder.Host.UseSerilog();

WebApplication app = builder.Build();

app.Run();