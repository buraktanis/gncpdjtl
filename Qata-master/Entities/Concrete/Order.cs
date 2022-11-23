using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.AccessControl;
using System.Text;
using Core.EntitiesBase;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Entities.Concrete
{
    public class Order : IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int CustomerId { get; set; }
        [Required(ErrorMessage = "Zorunlu Alan")]
        public string DeliveryType { get; set; }
        public int Transporting { get; set; }
        public DateTime OrderDate { get; set; }
        public int WareHouse { get; set; }
        [Required(ErrorMessage = "Zorunlu Alan")]
        public string TypeOfPayment { get; set; }
        public string CityId { get; set; }
        public string TownId { get; set; }
        public string Address { get; set; }
        public int Confirmation { get; set; }
        public int CustomerConsignmentId { get; set; }
        public int Status { get; set; }
        public string OrderNo { get; set; }
        public string Slsman { get; set; }
        public string Not { get; set; }

    }
}
