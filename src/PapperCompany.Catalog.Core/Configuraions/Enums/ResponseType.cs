using System.ComponentModel;

namespace PapperCompany.Catalog.Core;

public enum ResponseType
{
    [Description("Warning")]
    Warning,

    [Description("Error")]
    Error,

    [Description("Fatal")]
    Fatal,
}
