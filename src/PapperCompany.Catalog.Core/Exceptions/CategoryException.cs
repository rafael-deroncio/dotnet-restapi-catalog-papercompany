using System.Net;

namespace PapperCompany.Catalog.Core.Exceptions;

public class CategoryException : BaseException
{
    public CategoryException(string title, string message, HttpStatusCode code = HttpStatusCode.UnprocessableEntity) : base(title, message)
    {
        Code = code;
    }

    public CategoryException(string title, string message, Exception inner, HttpStatusCode code = HttpStatusCode.UnprocessableEntity) : base(title, message, inner)
    {
        Code = code;
    }

    public CategoryException(string message, Exception inner) : base(message, inner)
    {
        Title = "Category Error";
    }
}
