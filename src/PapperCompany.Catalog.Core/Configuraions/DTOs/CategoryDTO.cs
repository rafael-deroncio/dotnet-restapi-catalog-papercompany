namespace PapperCompany.Catalog.Core.Configurations.DTOs;

    public class CategoryDTO : CatalogDTO
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
