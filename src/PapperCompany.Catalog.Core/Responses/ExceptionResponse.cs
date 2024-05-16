namespace PapperCompany.Catalog.Core.Responses;

public class ExceptionResponse
{
    public string Title { get; set; }
    public string Type { get; set; }
    public string[] Messages { get; set; }
}
