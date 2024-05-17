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

// Add Custom Swagger Auth with JWT Bearer
builder.Services.AddSwaggerJwtBearer(builder.Configuration);

// Add URLs lowercase
builder.Services.AddLowerCaseRouting();

// Add Services DI
builder.Services.AddServices();

// Add Repositories DI
builder.Services.AddRepositories();

// Add Custom config auth
builder.Services.AddAuthentication(builder.Configuration);

builder.Services.AddAuthorization();

WebApplication app = builder.Build();

app.UseHsts();

app.UseHttpsRedirection();

app.UseSerilogRequestLogging();

app.UseRouting();

app.UseCors();

app.UseAuthentication();

app.UseAuthorization();

app.UseApiVersioning();

app.UseSwagger(builder.Configuration);

app.UseGlobalHandlerException();

app.UseEndpoints(e => e.MapControllers());

app.Run();