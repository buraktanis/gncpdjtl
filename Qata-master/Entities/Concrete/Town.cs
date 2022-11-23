using System;
using System.Collections.Generic;
using System.Runtime;
using System.Text;
using Core.EntitiesBase;

namespace Entities.Concrete
{
    public class Town : IEntity
    {
        public int Id { get; set; }
        public string  Name { get; set; }
        public int CityId { get; set; }
    }
}
