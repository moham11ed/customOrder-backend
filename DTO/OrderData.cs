namespace customOrder.Service
{
    public class OrderData
    {
        public string? ProductType { get; set; }
        public int? ProductTypeId { get; set; }
        public List<SelectedOil> SelectedOils { get; set; } = new List<SelectedOil>();
        public int? ShapeId { get; set; }
        public string? ShapeImageUrl { get; set; }
        public int? DesignId { get; set; }
        public string? DesignUrl { get; set; }
        public string? CustomImage { get; set; } // Base64 string or URL
        public string? ProductName { get; set; }
        public int? Quantity { get; set; }
        public ClientInfo ClientInfo { get; set; }
    }

    public class SelectedOil
    {
        public int Id { get; set; }
        public string Name { get; set; }
        // Add any other oil-specific properties
    }

    public class ClientInfo
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
    }
}