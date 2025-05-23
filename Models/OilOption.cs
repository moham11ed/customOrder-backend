namespace customOrder.Models
{

    public enum OilType
    {
        Shampoo = 1,
        Conditioner = 2,
    }
    public class OilOption
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }

        public OilType Type { get; set; }

    }
}
