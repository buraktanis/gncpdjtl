using Core.EntitiesBase;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Concrete
{
   public class Item: IEntity
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Zorunlu Alan")]
        public string Name { get; set; }

        public string Code { get; set; }
        public int Price { get; set; }
        public int Stock { get; set; }
        public int ItemType { get; set; }
        public int LogoId { get; set; }

    }
}
