using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Core.Results;
using Entities.Concrete;
using static Core.DataTables.DatatablesJS;

namespace Business.OrderManagement
{
    public interface IOrderService
    {
        IDataResult<IList<Order>> GetList(Expression<Func<Order, bool>> filter = null);
        IDataResult<Order> Add(Order order);
        IResult Update(Order order);
        IResult Delete(Order order);
        IResult DeleteById(int orderId);
        IDataResult<Order> GetOrderById(int orderId);
        IDataResult<List<Order>> GetAll();
        DataTablesObjectResult GetOrderDatatables(DatatablesObject requestobj);
        DataTablesObjectResult GetOrderConfirmayionDatatables(DatatablesObject requestobj);
        DataTablesObjectResult GetCustomerOrders(DatatablesObject requestobj);
    }
}
