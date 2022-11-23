using Core.EntitiesBase;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Concrete
{
    public class Not: IEntity
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Username { get; set; }
        public string Text { get; set; }
        public string NoteType { get; set; }
        public int CariId { get; set; }
        public int BigdataId { get; set; }
        public DateTime createDate { get; set; }
    }
}
