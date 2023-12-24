using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Utrans_API.Models

{
    public class Brands
    {
        public int id { get; }
        public string? Code { get; set; }
        public string Name { get; set; }
        public string? Number { get; set; }
        public string? Email { get; set; }
        public string? Website { get; set; }
        public DateTime? Created_at { get; set; }
        public DateTime? Updated_at { get; set; }
        public DateTime? Deleted_at { get; set; }
    }
}
