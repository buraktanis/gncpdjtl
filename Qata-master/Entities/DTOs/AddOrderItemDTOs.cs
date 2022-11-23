using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DTOs
{
   public class AddOrderItemDTOs
    {
        public int LogoId { get; set; }
        public int Amount { get; set; }
        public int OrderId { get; set; }
        public decimal Price { get; set; }
    }
}
