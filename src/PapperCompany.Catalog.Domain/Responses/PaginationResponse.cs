using System.Text.Json.Serialization;

namespace PapperCompany.Catalog.Domain;

public class PaginationResponse<T>
{
    [JsonPropertyName("page")]
    public int PageNumber { get; set; }

    [JsonPropertyName("size")]
    public int PageSize { get; set; }

    [JsonPropertyName("first")]
    public Uri FirstPage { get; set; }

    [JsonPropertyName("last")]
    public Uri LastPage { get; set; }

    [JsonPropertyName("pages")]
    public int TotalPages { get; set; }

    [JsonPropertyName("records")]
    public int TotalRecords { get; set; }

    [JsonPropertyName("next")]
    public Uri NextPage { get; set; }

    [JsonPropertyName("previous")]
    public Uri PreviousPage { get; set; }
    
    [JsonPropertyName("data")]
    public T Data { get; set; }
}
