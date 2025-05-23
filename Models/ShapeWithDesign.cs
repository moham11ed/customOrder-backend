using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace customOrder.Models
{
    public class ShapeWithDesign
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("BottleDesign")]
        public int ShapeId { get; set; }
        public BottleDesign BottleDesign { get; set; }

        [ForeignKey("LogoDesign")]
        public int DesignId { get; set; }
        public LogoDesign LogoDesign { get; set; }

        public string ImageUrl { get; set; }
    }
}