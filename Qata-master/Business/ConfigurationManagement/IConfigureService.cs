using System;
using System.Collections.Generic;
using System.Text;
using Core.Results;
using Entities.Concrete;
using Entities.DTOs;
using static Core.DataTables.DatatablesJS;

namespace Business.ConfigurationManagement
{
    public interface IConfigureService
    {
        IResult Add(Configuration configuration);
        IResult Update(Configuration configuration);
        IResult Delete(Configuration configuration);
        IDataResult<Configuration> GetConfigurationById(int configurationId);
        IDataResult<List<Configuration>> GetAll();
        IDataResult<List<ConfigurationTypeDto>> GetConfigurationTypeAll();
        DataTablesObjectResult GetConfigurationDatatables(DatatablesObject requestobj);
        IDataResult<List<City>> GetAllCities();
        IDataResult<List<Town>> GetTownsByCityId(int cityId);
        IDataResult<City> GetTownsByCityName(string name);
        DataResult<List<Cities_Area>> GetCities_Area(string no);
        IDataResult<IList<Configuration>> GetConfigurationType(int type);
    
    }
}
