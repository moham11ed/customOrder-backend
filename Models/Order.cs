// Order.cs (Entity model)
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace customOrder.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        // Product information
        public string ProductType { get; set; }
        public int ProductTypeId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }

        // Customization options
        public string SelectedOilsJson { get; set; } // Stored as JSON string
        public int? ShapeId { get; set; }
        public string ShapeImageUrl { get; set; }
        public int? DesignId { get; set; }
        public string DesignUrl { get; set; }
        public string CustomImage { get; set; }

        // Client information
        public string ClientName { get; set; }
        public string ClientEmail { get; set; }
        public string ClientPhone { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }

        // Metadata
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string Status { get; set; } = "Pending";
    }
}