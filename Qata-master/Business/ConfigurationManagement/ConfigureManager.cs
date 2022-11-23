using Core.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccess.Abstract;
using Entities.DTOs;
using static Core.DataTables.DatatablesJS;
using Core.DataTables;
using Core.Extensions;
using System.Linq.Expressions;

namespace Business.ConfigurationManagement
{
    public class ConfigureManager : IConfigureService
    {
        private IConfigureDal _configureDal;
        private ICityDal _cityDal;
        private ITownDal _townDal;
        private INotificationDal _notificationDal;
        public ConfigureManager(IConfigureDal configureDal, ICityDal cityDal, ITownDal townDal, INotificationDal notificationDal)
        {
            _configureDal = configureDal;
            _cityDal = cityDal;
            _townDal = townDal;
            _notificationDal = notificationDal;
        }

        public IResult Add(Configuration configuration)
        {
            return _configureDal.Add(configuration);
        }
        
        public IResult Addnotification(Notification e)
        {
            return _notificationDal.Add(e);
        }
        public IDataResult<Notification> Getnotification(Expression<Func<Notification, bool>> filter = null)
        {
            return _notificationDal.Get(filter);
        }
        public IResult Delete(Configuration configuration)
        {
            return _configureDal.Delete(configuration);
        }

        public DataTablesObjectResult GetConfigurationDatatables(DatatablesObject requestobj)
        {
            string query = " SELECT * FROM Configurations ";

            string privadewhere = "";
            var tip = requestobj.additionalvalues.ElementAt(0);

            if (!tip.isNull())
            {
                privadewhere = string.Format(" Type={0} ", tip);
            }


            return new DatatablesJS.DataTablesObjectResult().getresults(requestobj, query, privadewhere);
        }
        public DataResult<List<Cities_Area>> GetCities_Area(string no)
        {
            List<Cities_Area> model = string.Format(@" SELECT * FROM Cities_Area WHERE City='{0}' ", no).GetQuery<Cities_Area>();

            return new SuccessDataResult<List<Cities_Area>>(model);
        }
        public IDataResult<List<City>> GetAllCities()
        {
            return new SuccessDataResult<List<City>>(_cityDal.GetList().Data.ToList());
        }

        public IDataResult<List<Town>> GetTownsByCityId(int cityId)
        {
            return new SuccessDataResult<List<Town>>(_townDal.GetList(c=>c.CityId==cityId).Data.ToList());
        } 
        public IDataResult<City> GetTownsByCityName(string name)
        {
            return _cityDal.Get(c => c.Name == name);
        }

        public IDataResult<List<Configuration>> GetAll()
        {
            return new SuccessDataResult<List<Configuration>>(_configureDal.GetList().Data.ToList());
        }
          
        public IDataResult<List<ConfigurationTypeDto>> GetConfigurationTypeAll()
        {
            var list =new List<ConfigurationTypeDto>()
            {
                new ConfigurationTypeDto{Id=1,Name="Müşteri Tipi" },
                new ConfigurationTypeDto{Id=2,Name="Müşteri Türü" },
                new ConfigurationTypeDto{Id=3,Name="Müşteri Durumu" },
                new ConfigurationTypeDto{Id=4,Name="Müşteri Sınıfı" },
                new ConfigurationTypeDto{Id=5,Name="Bölge" },
                new ConfigurationTypeDto{Id=6,Name="Depo" },
                new ConfigurationTypeDto{Id=7,Name="Ödeme Tipi" },
                new ConfigurationTypeDto{Id=8,Name="Teslimat Tipi" },
                new ConfigurationTypeDto{Id=9,Name="Banka Bilgilerimiz IBAN" },
                new ConfigurationTypeDto{Id=10,Name="Kargo Bilgileri" },
                new ConfigurationTypeDto{Id=11,Name="Notlar" },
            };

            return new SuccessDataResult<List<ConfigurationTypeDto>>(list);
        }

        public IDataResult<Configuration> GetConfigurationById(int configurationId)
        {
            return _configureDal.Get(c => c.Id == configurationId);
        }
         public IDataResult<IList<Configuration>> GetConfigurationType(int type)
        {
            return _configureDal.GetList(c => c.Type == type);
        }

        public IResult Update(Configuration configuration)
        {
           return _configureDal.Update(configuration);
        }
    }
}
