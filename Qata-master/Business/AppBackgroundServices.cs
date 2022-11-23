using Business.LogoManagement;
using Business.OrderManagement;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business
{
    public class AppBackgroundServices : BackgroundService
    {
        private IOrderService _orderService;
        private readonly ILogoManagement  _logoManagement;
        public AppBackgroundServices(IOrderService orderService, ILogoManagement logoManagement)
        {
            _orderService = orderService;
            _logoManagement = logoManagement;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {


                int i = 0;
                logo();
                await Task.Delay(15*60*1000, stoppingToken);
            }



        }


        public bool logo()
        {
            
            var orders = _orderService.GetList(x => x.Status == 1).Data;
            if (orders.Count==0)
            {
                return false;
            }
            List<string> carikodlar = orders.Where(x=>x.OrderNo!=null).Select(x => x.OrderNo).ToList();

            var ARY_017_SİPARİŞ_HAREKETLERI_List = _logoManagement.ARY_017_SİPARİŞ_HAREKETLERI_List(carikodlar);

            var OrderNos = ARY_017_SİPARİŞ_HAREKETLERI_List.Data.Select(x => x.CariCodu).ToList();
             var oup = orders.Where(x => OrderNos.Contains(x.OrderNo)).ToList();
            foreach (var item in oup)
            {
                item.Status = 2;
                _orderService.Update(item);
            }

            return true;
        }


    }
}
