namespace customOrder.DTO
{
    public class TranslationDto
    {
        public string Key { get; set; }
        public Dictionary<string, string> Values { get; set; } = new Dictionary<string, string>();
    }
}
