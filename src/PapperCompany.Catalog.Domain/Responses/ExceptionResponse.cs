namespace PapperCompany.Catalog.Domain.Responses;

public class ExceptionResponse
{
    public string Title { get; set; }
    public string Type { get; set; }
    public string[] Messages { get; set; }
}
