namespace customOrder.Models
{
    public class Translation
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public List<TranslationItem> Items { get; set; } = new List<TranslationItem>();
    }

    public class TranslationItem
    {
        public int Id { get; set; }
        public string LanguageCode { get; set; } 
        public string Value { get; set; }

        public int TranslationId { get; set; }
        public Translation Translation { get; set; }
    }
}
