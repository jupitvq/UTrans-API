﻿using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Utrans_API.Models

{
    public class Customers
    {
        public int id { get; set; }
        public string? Code { get; set; }
        public string Name { get; set; }
        public string? Address { get; set; }
        public string? District { get; set; }
        public string? City { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        [JsonIgnore]
        public DateTime? Created_at { get; set; }
        [JsonIgnore]
        public DateTime? Updated_at { get; set; }
        [JsonIgnore]
        public DateTime? Deleted_at { get; set; }
    }
}
