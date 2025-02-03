namespace API.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string UserEmail { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
