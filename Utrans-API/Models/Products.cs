using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Utrans_API.Models

{
    public class Products
    {
        public int id { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Brand_id { get; set; }
        public string? Code { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal stock { get; set; }
        public decimal sales_price { get; set; }
        public decimal standard_price { get; set; }
        [JsonIgnore]
        public DateTime? Created_at { get; set; }
        [JsonIgnore]
        public DateTime? Updated_at { get; set; }
        [JsonIgnore]
        public DateTime? Deleted_at { get; set; }
    }
}
