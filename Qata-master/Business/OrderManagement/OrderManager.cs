using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Core.Extensions;
using Core.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using static Core.DataTables.DatatablesJS;

namespace Business.OrderManagement
{
    public class OrderManager:IOrderService
    {
        private IOrderDal _orderDal;

        public OrderManager(IOrderDal orderDal)
        {
            _orderDal = orderDal;
        }
        public IDataResult<Order> Add(Order order)
        {
            order.OrderDate=DateTime.Now;
            var result = _orderDal.Add(order);
            return result;
        }

        public IResult Update(Order order)
        {
            return _orderDal.Update(order);
        }
           public IResult UpdateAsync(Order order)
        {
            return _orderDal.UpdateAsync(order);
        }

        public IResult Delete(Order order)
        {
            return _orderDal.Delete(order);
        }

        public IResult DeleteById(int orderId)
        {
            throw new NotImplementedException();
        }

        public IDataResult<Order> GetOrderById(int orderId)
        {
            return _orderDal.Get(o => o.Id == orderId);
        } 
        public IDataResult<IList<Order>> GetList(Expression<Func<Order, bool>> filter = null)
        {
            return _orderDal.GetList(filter);
        }

        public IDataResult<List<Order>> GetAll()
        {
            return new SuccessDataResult<List<Order>>(_orderDal.GetList().Data.ToList());
        }
        public DataTablesObjectResult GetOrderDatatables(DatatablesObject requestobj)
        {
            string query = @"  SELECT * FROM OrderAll ";

            string privadewhere = "";
          
            var Slsman = requestobj.additionalvalues.ElementAt(0);

            if (!Slsman.Trim().isNull())
            {
                privadewhere = string.Format(" Slsman='{0}' ", Slsman);
            }

            if (!requestobj.additionalvalues.ElementAt(1).isNull() && !requestobj.additionalvalues.ElementAt(2).isNull())
            {
                if (!privadewhere.isNull())
                {
                    privadewhere = privadewhere + " AND ";
                }

                privadewhere = privadewhere + string.Format(" OrderDate BETWEEN '{0}' AND '{1}' ", requestobj.additionalvalues.ElementAt(1), requestobj.additionalvalues.ElementAt(2));
            }
            return new DataTablesObjectResult().getresults(requestobj, query, privadewhere);
        }   
        public DataTablesObjectResult GetCustomerOrders(DatatablesObject requestobj)
        {
             query = @" SELECT * FROM OrderAll ";

            string privadewhere = "";

            var val = requestobj.additionalvalues.ElementAt(0);

            if (!val.Trim().isNull())
            {
                privadewhere = string.Format(" CustomerId={0} ", val);
            }

            return new DataTablesObjectResult().getresults(requestobj, query, privadewhere);
        }




        public DataTablesObjectResult GetOrderConfirmayionDatatables(DatatablesObject requestobj)
        {
             query = @" SELECT * FROM OrderAll ";
           
            string privadewhere = "";
            var Status = requestobj.additionalvalues.ElementAt(0);
            var Slsman = requestobj.additionalvalues.ElementAt(1);
            if (!Slsman.Trim().isNull())
            {
                privadewhere = string.Format(" Slsman='{0}' ", Slsman);
            }


            
            if (!Status.isNull())
            {
                if (!privadewhere.isNull())
                {
                    privadewhere = privadewhere + " AND ";
                }
                privadewhere = privadewhere+ string.Format(" Status={0} ", Status);
            }


            return new DataTablesObjectResult().getresults(requestobj, query, privadewhere);
        }


        string query = @"  SELECT o.Id,c.Name,o.Status,o.OrderDate,o.Slsman,o.OrderNo,o.[Not], as Notlar,o.CustomerId FROM Orders o JOIN Customers c ON o.CustomerId=c.Id ";
    }
}
