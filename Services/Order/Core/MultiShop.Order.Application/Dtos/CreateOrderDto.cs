    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Order.Application.Dtos
{
    public class CreateOrderDto
    {
        public string UserId { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime OrderDate { get; set; }
        public CreateAddressDto? Address { get; set; } 
        public List<CreateOrderDetailDto> OrderDetails { get; set; }
    }
}
