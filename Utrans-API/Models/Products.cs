namespace Utrans_API.Models

{
    public class Products
    {
        public int id { get; set; }
        public int Brand_id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal? stock { get; set; }
        public decimal? retail_price { get; set; }
        public decimal? whole_sale_price { get; set; }
        public decimal? standard_price { get; set; }
        public DateTime? Created_at { get; set; }
        public DateTime? Updated_at { get; set; }
        public DateTime? Deleted_at { get; set; }
    }
}
