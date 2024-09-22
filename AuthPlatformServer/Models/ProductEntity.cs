namespace AuthPlatformServer.Models
{
    public class ProductEntity
    {
        public Guid Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public int Period { get; set; }

        public string Description { get; set; } = string.Empty;
    }
}
