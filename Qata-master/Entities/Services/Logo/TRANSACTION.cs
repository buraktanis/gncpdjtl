using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Services.Logo
{
   public class TRANSACTION
    {
        public List<ITEM> items { get; set; }

        public TRANSACTION()
        {
            items = new List<ITEM>();
        }
    }
}
