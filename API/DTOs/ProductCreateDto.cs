namespace API.DTOs
{
    public class ProductCreateDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
    
        public string Category { get; set; }
        public string token { get; set; }
    }
}
