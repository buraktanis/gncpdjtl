using System;
using System.Collections.Generic;
using System.Text;
using Core.Results;
using Entities.Concrete;
using static Core.DataTables.DatatablesJS;

namespace Business.OrderItemManagement
{
    public interface IOrderItemService
    {
        DataTablesObjectResult GetOrderItemByOrderIdDatatables(DatatablesObject requestobj);
        IResult Add(OrderItem orderItem);
        IResult Update(OrderItem orderItem);
        IResult Delete(OrderItem orderItem);
        IDataResult<OrderItem> GetOrderItemById(int orderItemId);
        IDataResult<List<OrderItem>> GetAll();
        IDataResult<OrderItem> GetOrderItemByLogoIdAndOrderId(int LogoId, int OrderId);
        public IDataResult<IList<OrderItem>> GetOrderItemByOrderId(int orderId);
    }
}
