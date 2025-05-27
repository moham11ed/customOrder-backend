using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace customOrder.Models
{
    public class ShapeWithDesign
    {
        [Key]
        public int Id { get; set; }

        public int ShapeId { get; set; }  
        public int DesignId { get; set; } 
        public string ImageUrl { get; set; }
    }
}