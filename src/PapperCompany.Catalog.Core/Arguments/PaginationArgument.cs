namespace PapperCompany.Catalog.Core.Arguments;

public class PaginationArgument
{
    private int pageNumber = 1;
    private int pagesize = 5;

    public int Page
    {
        get { return pageNumber; }
        set { pageNumber = value < 1 || value == default ? 1 : value; }
    }

    public int Size
    {
        get { return pagesize; }
        set { pagesize = value > 10 || value == default  ? 10 : value; }
    }

    public int Skip => (Page - 1) * Size;
}
