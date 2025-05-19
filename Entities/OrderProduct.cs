namespace SirketYonetim.Entities
{
    public class OrderProduct
    {
        public Guid orderId { get; set; }
        public Order order { get; set; }
        public Guid productId { get; set; }
        public Product product { get; set; }
        public int quantity { get; set; }

            
    }
}
