using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Core.EntitiesBase;

namespace Entities.Concrete
{
    public class AppUser:IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string  Email { get; set; }
        public string  Name { get; set; }
        public string Password { get; set; }
        public string Department { get; set; }
        public int Status { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
