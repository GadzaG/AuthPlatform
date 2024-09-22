using Newtonsoft.Json.Linq;

namespace AuthPlatformServer.Models
{
    public enum Status
    {
        FREE,
        BUSY,
        FREEZ
    }

    public class KeyEntity
    {
        public Guid Id { get; set; }

        public Guid ProductId { get; set; }

        public ProductEntity Product { get; set; } = new ProductEntity();

        public string KeyText { get; set; } = string.Empty;

        public DateTime? ActivationTime { get; set; }

        public JObject? Data { get; set; }

        public Status Status { get; set; }
    }
}
