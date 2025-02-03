using System.Collections.Generic;

namespace API.DTOs
{
    public class OrderCreateDto
    {
        public string User { get; set; }
        public List<OrderProductDto> Products { get; set; }
    }
}
