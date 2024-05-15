using PapperCompany.Catalog.API.Extensions;
using Serilog;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add use of 'secrets.yaml' file
builder.Configuration.AddSecrets();

// Add Serilog Configuration
builder.Host.UseSerilog();

// Add Custom Cors Policies
builder.Services.AddCors(builder.Configuration);



WebApplication app = builder.Build();

// Use Requests Logging
app.UseSerilogRequestLogging();

// Use Custom Cors Policies
app.UseCors();

app.Run();