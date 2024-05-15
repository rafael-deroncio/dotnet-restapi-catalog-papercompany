
using PapperCompany.Catalog.API.Extensions;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Adds use of 'secrets.yaml' file
builder.Configuration.AddSecrets();

WebApplication app = builder.Build();

app.Run();