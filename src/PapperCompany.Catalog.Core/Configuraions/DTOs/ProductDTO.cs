namespace PapperCompany.Catalog.Core.Configurations.DTOs;

    public class ProductDTO : CatalogDTO
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public decimal Price { get; set; }
        public CategoryDTO Category { get; set; }
    }
