namespace PapperCompany.Catalog.API.Settings;

public class VersioningSettings
{
    public ApiVersion ApiVersion { get; set; }
    public string Reader { get; set; }
    public Explorer Explorer { get; set; }
}

public class ApiVersion
{
    public int High { get; set; }
    public int Medium { get; set; }
}

public class Explorer
{
    public string Format { get; set; }
}
