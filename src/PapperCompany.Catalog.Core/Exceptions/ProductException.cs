using System.Net;

namespace PapperCompany.Catalog.Core.Exceptions;

public class ProductException : BaseException
{
    public ProductException(string title, string message, HttpStatusCode code = HttpStatusCode.UnprocessableEntity) : base(title, message)
    {
        Code = code;
    }

    public ProductException(string title, string message, Exception inner, HttpStatusCode code = HttpStatusCode.UnprocessableEntity) : base(title, message, inner)
    {
        Code = code;
    }

    public ProductException(string message, Exception inner) : base(message, inner)
    {
        Title = "Product Error";
    }
}
