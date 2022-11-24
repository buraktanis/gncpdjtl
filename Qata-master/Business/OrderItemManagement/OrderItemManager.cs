using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Extensions;
using Core.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using static Core.DataTables.DatatablesJS;

namespace Business.OrderItemManagement
{
    public class OrderItemManager:IOrderItemService

    {
        private IOrderItemDal _orderItemDal;

        public OrderItemManager(IOrderItemDal orderItemDal)
        {
            _orderItemDal = orderItemDal;
        }

        

        public DataTablesObjectResult GetOrderItemByOrderIdDatatables(DatatablesObject requestobj)
        {
            string query = @" SELECT *  FROM OrderItems   ";
            string privadewhere = "";
            var tip = requestobj.additionalvalues.ElementAt(0);

            if (!tip.isNull())
            {
                privadewhere = string.Format(" OrderId={0} ", tip);
            }

            return new DataTablesObjectResult().getresults(requestobj, query, privadewhere);
        }

        public DataTablesObjectResult riskkontrol(DatatablesObject requestobj)
        {
            
            string privadewhere = "";
            var tip = requestobj.additionalvalues.ElementAt(0);
            string query = string.Format(@"SELECT  TEMSİLCİ temsilci, [CH KODU] kod, [CH UNVANI] unvan, GECEN_GUN, CASE WHEN GECEN_GUN<0 THEN 'Vade Geçmemiş' ELSE 'VADE GEÇMİŞ!' END [MODÜL]  
    , SUM(TUTAR) TUTAR FROM ARY_XXX_TAHSILAT WHERE [TEMSİLCİ] NOT IN ('PASİF','BOŞ','HUKUK') AND [CH UNVANI] = '{0}' GROUP BY [CH KODU], [CH UNVANI],
  TEMSİLCİ,GECEN_GUN, CASE WHEN GECEN_GUN<0 THEN 'Vade Geçmemiş' ELSE 'VADE GEÇMİŞ!' END ORDER
 BY [CH UNVANI], CASE WHEN GECEN_GUN<0 THEN 'Vade Geçmemiş' ELSE 'VADE GEÇMİŞ!' END, SUM(TUTAR) DESC ", tip);

            

            return new DataTablesObjectResult().getresults(requestobj, query, privadewhere);
        }

        public IResult Add(OrderItem orderItem)
        {
            return _orderItemDal.Add(orderItem);
        }

        public IResult Update(OrderItem orderItem)
        {
            return _orderItemDal.Update(orderItem);
        }

        public IResult Delete(OrderItem orderItem)
        {
            return _orderItemDal.Delete(orderItem);
        }

        public IDataResult<OrderItem> GetOrderItemById(int orderItemId)
        {
            return _orderItemDal.Get(o => o.Id == orderItemId);
        }   
        
        public IDataResult<IList<OrderItem>> GetOrderItemByOrderId(int orderId)
        {
           
            return _orderItemDal.GetList(o => o.OrderId == orderId);
        } 
        public IDataResult<OrderItem> GetOrderItemByLogoIdAndOrderId(int LogoId,int OrderId)
        {
            return _orderItemDal.Get(o => o.LogoId == LogoId && o.OrderId== OrderId);
        }

        public IDataResult<List<OrderItem>> GetAll()
        {
            return new SuccessDataResult<List<OrderItem>>(_orderItemDal.GetList().Data.ToList());
        }
    }
}
