# PapperCompany Catalog API

## Overview
This project provides an API to manage processes related to the context of product catalog and product categories. The solution is structured into several projects to separate concerns and ensure modularity.

## Solution Structure
The solution consists of the following projects:

- **PapperCompany.Catalog.Core**: Core logic and business rules.
- **PapperCompany.Catalog.API**: API endpoints and controllers.
- **PapperCompany.Catalog.Domain**: Domain entities and models.
- **PapperCompany.Catalog.Test**: Unit tests for the solution.

## Prerequisites
- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [PostgreSQL](https://www.postgresql.org/download/)
- [Docker](https://www.docker.com/)
- Docker image for PostgreSQL:
  ```sh
  docker pull postgres
  ```
  
## Setting Up the Project

### Configuration
For the API to work correctly, you need to configure a `secrets.yaml` file in the root directory of the `PapperCompany.Catalog.API` project. This file is crucial for managing sensitive information like connection strings and security keys.

#### secrets.yaml
Create a file named `secrets.yaml` in the root of the `PapperCompany.Catalog.API` project directory with the following content:

```yaml
DBCatalogConnectionString: <YOUR CONNECTION STRING>
JwtSymmetricSecurityKey: <YOUR SECURITY HASH>
```

**Note**: This file is set to be ignored by git by default to prevent sensitive information from being exposed in version control.

### Implementing Secrets Configuration
To utilize the `secrets.yaml` file, you need to implement the following classes and methods:

#### Secrets Class
Define the structure of the secrets in `PapperCompany.Catalog.Core/Configurations/Secrets/Secrets.cs`:

```csharp
namespace PapperCompany.Catalog.Core.Configurations.Secrets
{
    public class Secrets
    {
        public string DBCatalogConnectionString { get; set; }
        public string JwtSymmetricSecurityKey { get; set; }
    }
}
```

#### SecretsReader Class
Create a class to read the `secrets.yaml` file in `PapperCompany.Catalog.Core/Configurations/Secrets/SecretsReader.cs`:

```csharp
using YamlDotNet.Serialization;

namespace PapperCompany.Catalog.Core.Configurations.Secrets
{
    public class SecretsReader
    {
        public static Secrets Get(string file)
        {
            try
            {
                using StreamReader reader = new(string.Format(@"./{0}", file));
                return new DeserializerBuilder().Build().Deserialize<Secrets>(reader);
            }
            catch (Exception ex)
            {
                throw new FileLoadException($"Unable to get system secrets :{ex}");
            }
        }
    }
}
```

#### ConfigurationExtensions Class
```csharp
using PapperCompany.Catalog.Core.Configurations.Secrets;

namespace PapperCompany.Catalog.API.Extensions;

public static class ConfigurationExtensions
{
    public static IConfigurationBuilder AddSecrets(this IConfigurationBuilder builder)
    {
        string file = "secrets.yaml";

        Secrets secrets = SecretsReader.Get(file);

        builder.AddInMemoryCollection(new Dictionary<string, string>
        {
            {"DBCatalogConnectionString", secrets.DBCatalogConnectionString},
            {"JwtSymmetricSecurityKey", secrets.JwtSymmetricSecurityKey},
        });

        return builder;
    }
}
```
#### Applying Configuration in Program.cs
Ensure the secrets.yaml file is used during the application startup in PapperCompany.Catalog.API/Program.cs:
```csharp
// Add use of 'secrets.yaml' file
builder.Configuration.AddSecrets();
```

### Running the Project

1. Clone the repository.
2. Run docker.
```bash
docker-compose up -d
```
4. Navigate to the `PapperCompany.Catalog.API` directory.
5. Ensure `secrets.yaml` is correctly configured.
6. Run API.
```bash
dotnet run
```
### API Endpoints
The API provides the following endpoints to manage categories and products.

#### Category Endpoints
- **GET** `/api/v1/category/paged`: Retrieves a paginated list of categories.
- **GET** `/api/v1/category/{id}`: Retrieves the details of a specific category.
- **PUT** `/api/v1/category/{id}`: Updates an existing category.
- **DELETE** `/api/v1/category/{id}`: Deletes a category.
- **POST** `/api/v1/category`: Creates a new category.

#### Product Endpoints
- **GET** `/api/v1/product/paged`: Retrieves a paginated list of products.
- **GET** `/api/v1/product/{id}`: Retrieves the details of a specific product.
- **PUT** `/api/v1/product/{id}`: Updates an existing product.
- **DELETE** `/api/v1/product/{id}`: Deletes a product.
- **POST** `/api/v1/product`: Creates a new product.


### Testing
To run the tests, navigate to the `PapperCompany.Catalog.Test` directory and use the following command:

```bash
dotnet test
```

### Dependencies
The following packages are used in the project:

- Microsoft.AspNetCore.Authentication.JwtBearer
- Microsoft.AspNetCore.Mvc.Versioning
- Microsoft.AspNetCore.Mvc.Versioning.ApiExplorer
- Microsoft.AspNetCore.OpenApi
- Swashbuckle.AspNetCore
- System.IdentityModel.Tokens.Jwt
- AutoMapper.Extensions.Microsoft.DependencyInjection
- Dapper
- Npgsql
- Serilog
- YamlDotNet

Ensure these packages are restored before building and running the project.

### Contact
For any issues or inquiries, please contact the Developer Team.


