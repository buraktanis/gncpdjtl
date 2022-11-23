using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DTOs
{
    public  class AddressDTOs
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string CityId { get; set; }
        public string TownId { get; set; }
        public string Address { get; set; }
    }
}
