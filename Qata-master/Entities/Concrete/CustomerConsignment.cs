using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.AccessControl;
using System.Text;
using Core.EntitiesBase;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Entities.Concrete
{
    public class CustomerConsignment : IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int CLIENTREF { get; set; }
        public string CityId { get; set; }
        public string TownId { get; set; }
        [Required(ErrorMessage = "Zorunlu Alan")]
        public string Address { get; set; }
        public int CustomerId { get; set; }
        public string CariKodu { get; set; }
        public string Code { get; set; }
        public int LogoId { get; set; }
        public string Name { get; set; }
    }
}
