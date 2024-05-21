using System.Text.Json;

namespace PapperCompany.Catalog.Domain.Responses;

public abstract class Response
{
    public abstract string ToJSON();
}