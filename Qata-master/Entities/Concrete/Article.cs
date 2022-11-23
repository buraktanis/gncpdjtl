using System;
using System.Collections.Generic;
using System.Text;
using Core.EntitiesBase;

namespace Entities.Concrete
{
    public class Article:IEntity
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string CategoryId { get; set; }
        public string Hood { get; set; }
        public string Content { get; set; }
        public string ImageUrl { get; set; }
    }
}
