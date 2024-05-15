namespace PapperCompany.Catalog.API.Settings;

public class SwaggerSettings
{
    public string Name { get; set; }
    public string Title { get; set; }
    public string Version { get; set; }
    public string Description { get; set; }

    public ContactInfo Contact { get; set; }
}

public class ContactInfo
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Url { get; set; }
}
