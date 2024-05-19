namespace PapperCompany.Catalog.Core.Configurations.Mapper.Interfaces;

public interface IObjectConverter
{
    T Map<T>(object source);
    D Map<T, D>(T source, D destination);
}
