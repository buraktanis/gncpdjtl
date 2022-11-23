using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Core.EntitiesBase;

namespace Entities.Concrete
{
    public class Configuration : IEntity

    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Info { get; set; }
        public int Status { get; set; }
        public int Type { get; set; }
    }
}
