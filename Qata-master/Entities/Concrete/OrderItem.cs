using System;
using System.Collections.Generic;
using System.Security.Permissions;
using System.Text;
using Core.EntitiesBase;

namespace Entities.Concrete
{
    public class OrderItem:IEntity
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public string ProductName { get; set; }
        public int UnitsInStock { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalAmount { get; set; }
        public int Vat { get; set; }
        public int ItemType { get; set; }
        public int LogoId { get; set; }
        public string LogoKod { get; set; }
    }
}
