using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Utrans_API.Models

{
    public class Vendors
    {
        public int id { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string? Code { get; set; }
        public string Name { get; set; }
        public string? Address { get; set; }
        public string? District { get; set; }
        public string? City { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public DateTime? Created_at { get; set; }
        public DateTime? Updated_at { get; set; }
        public DateTime? Deleted_at { get; set; }
    }
}
