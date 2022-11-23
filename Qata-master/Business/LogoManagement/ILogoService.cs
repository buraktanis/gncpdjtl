using Core.Results;
using Entities.Concrete;
using Entities.Services.Logo;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.LogoManagement
{
   public interface ILogoService
    {
        IResult ArpShipmentLocationsPost(CustomerConsignment consignment);
        IDataResult<List<CL_LIST>> GetCL_LISTById(int id);
        IResult CLCARDAdd(Customer customer);
        IResult AddOrderLogo(Order order, List<OrderItem> orderItems, Customer customer, CustomerConsignment consignment);
        IDataResult<CLCARD> GetCLCARDByLogoId(int id);
        public IResult GetCLCARD();
        IDataResult<CL_LIST> GetCL_LISTByPost(int id);
        IResult Getsalesmen();
    }
}
