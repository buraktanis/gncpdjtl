using Core.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.NetgsmManagement
{
    public interface INetgsmManagement
    {
        public IDataResult<dynamic> test(int id);
    }
}
