using System.Text.Json;

namespace PapperCompany.Catalog.Domain.Requests;

public abstract class Request
{
    public string guid => Guid.NewGuid().ToString();
    public abstract string ToJSON();
}
