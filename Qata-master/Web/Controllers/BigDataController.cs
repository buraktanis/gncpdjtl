using Business.AppUserManagement;
using Business.BigDataManagenment;
using Business.ConfigurationManagement;
using Business.LogoManagement;
using Core.DataTables;
using Core.Results;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyCsvParser;
using TinyCsvParser.Mapping;
using Web.Models;
using Business.CustomerConsignmentManagement;
using ClosedXML.Excel;
using System.Data;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;

namespace Web.Controllers
{
    [Authorize]
    public class BigDataController : Controller
    {
        private IBigDataManager _bigDataManager;
        private IConfigureService _configureService;
        private IAppUserService _appUserService;
        private IConsignmentService _consignmentService;
        private ILogoManagement _logoManagement;
        private ILogoService _logoService;
        public BigDataController(IBigDataManager bigDataManager,
            IConfigureService configureService,
            IConsignmentService consignmentService,
            IAppUserService appUserService
            )
        {
            _bigDataManager = bigDataManager;
            _configureService = configureService;
            _appUserService = appUserService;
            _consignmentService = consignmentService;
        }

        public IActionResult Index()
        {
            return View(_configureService.GetConfigurationType(11).Data);
        }
        public IActionResult Add()
        {
            return View(GetbigDataViewModel().Data);
        }


        [HttpPost]
        public IActionResult GetBigData([FromBody] DatatablesJS.DatatablesObject requestobj)
        {
            try
            {
                return new JsonResult(_bigDataManager.GetBigData(requestobj));

            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                return new JsonResult(new { error = "Internal Server Error" });
            }
        }



        [HttpPost]
        public IActionResult Add(BigData bigData)
        {

            var bdata = _bigDataManager.Add(bigData);
            return View("Index");
        }


        public IActionResult Update(int id)
        {
            return View(GetbigDataViewModel(id).Data);
        }

        [HttpPost]
        public IActionResult Update(BigData bigData)
        {

            var bdata = _bigDataManager.Update(bigData);
          
                return RedirectToAction("Update", new { id = bigData.Id });
      
        }
        public IDataResult<BigdataViewModel> GetbigDataViewModel(int id)
        {
            var cities = _configureService.GetAllCities();
            var users = _appUserService.GetAll();
            var bigDataResult = _bigDataManager.Get(x => x.Id == id);
            var consignment = _consignmentService.GetById(id);
            var configureResult = _configureService.GetAll();
            var city = _configureService.GetTownsByCityName(bigDataResult.Data.City);
            int cityid = 0;
            if (city.Data != null)
            {
                cityid = city.Data.Id;
            }
            var towns = _configureService.GetTownsByCityId(cityid).Data.ToList();
            var model = new BigdataViewModel
            {
                bigDataTypes = new List<SelectListItem> { new SelectListItem { Text = "Firma", Value = "0" }, new SelectListItem { Text = "Sahsi Firma", Value = "1" } },
                KindOfbigDatas = (from i in configureResult.Data.Where(x => x.Type == 2).ToList()
                                   select new SelectListItem
                                   {
                                       Text = i.Name,
                                       Value = i.Name
                                   }).ToList(),
                bigDataStatus = new List<SelectListItem> { new SelectListItem { Text = "Aktif", Value = "0" }, new SelectListItem { Text = "Pasif", Value = "1" } },
                bigDataClasses = (from i in configureResult.Data.Where(x => x.Type == 4).ToList()
                                   select new SelectListItem
                                   {
                                       Text = i.Name,
                                       Value = i.Name
                                   }).ToList(),
                Regions = (from i in configureResult.Data.Where(x => x.Type == 5).ToList()
                           select new SelectListItem
                           {
                               Text = i.Name,
                               Value = i.Name
                           }).ToList(),
                Cities = (from i in cities.Data
                          select new SelectListItem
                          {
                              Text = i.Name,
                              Value = i.Name
                          }).ToList(),
                Representatives = (from i in users.Data
                                   select new SelectListItem
                                   {
                                       Text = i.Name,
                                       Value = i.Name
                                   }).ToList(),
                Towns = (from i in towns
                         select new SelectListItem
                         {
                             Text = i.Name,
                             Value = i.Name
                         }).ToList(),

            };
            model.bigData = bigDataResult.Data;
            model.bigDataConsignment = consignment.Data;

            return new SuccessDataResult<BigdataViewModel>(model);
        }
        public IDataResult<BigdataViewModel> GetbigDataViewModel()
        {
            var configureResult = _configureService.GetAll();
            var cities = _configureService.GetAllCities();
            var users = _appUserService.GetAll();
            var model = new BigdataViewModel
            {
                bigDataTypes = new List<SelectListItem> { new SelectListItem { Text = "Firma", Value = "0" }, new SelectListItem { Text = "Sahsi Firma", Value = "1" } },
                KindOfbigDatas = (from i in configureResult.Data.Where(x => x.Type == 2).ToList()
                                   select new SelectListItem
                                   {
                                       Text = i.Name,
                                       Value = i.Name
                                   }).ToList(),
                bigDataStatus = new List<SelectListItem> { new SelectListItem { Text = "Aktif", Value = "0" }, new SelectListItem { Text = "Pasif", Value = "1" } },
                bigDataClasses = (from i in configureResult.Data.Where(x => x.Type == 4).ToList()
                                   select new SelectListItem
                                   {
                                       Text = i.Name,
                                       Value = i.Name
                                   }).ToList(),
                Regions = (from i in configureResult.Data.Where(x => x.Type == 5).ToList()
                           select new SelectListItem
                           {
                               Text = i.Name,
                               Value = i.Name
                           }).ToList(),
                Cities = (from i in cities.Data
                          select new SelectListItem
                          {
                              Text = i.Name,
                              Value = i.Name
                          }).ToList(),
                Representatives = (from i in users.Data
                                   select new SelectListItem
                                   {
                                       Text = i.Name,
                                       Value = i.Name
                                   }).ToList(),

            };

           
            return new SuccessDataResult<BigdataViewModel>(model);
        }
        public IResult Delete(int id)
        {
            var orderItem = _bigDataManager.Get(x => x.Id == id).Data;
            var result = _bigDataManager.Delete(orderItem);

            return new SuccessResult();
        }

        [Microsoft.AspNetCore.Mvc.HttpPost]
        public async Task<IResult> UploadFile(List<IFormFile> file, string name)
        {

            if (file.Count == 0)
            {
                return new ErrorResult("Dosya Bulunmadı");
            }
            var url = await GetUploadFile(file[0], name);
            CsvParserOptions csvParserOptions = new CsvParserOptions(true, ',');
            CsvUserDetailsMapping csvMapper = new CsvUserDetailsMapping();
            CsvParser<BigData> csvParser = new CsvParser<BigData>(csvParserOptions, csvMapper);
            var result = csvParser
                         .ReadFromFile(@"wwwroot" + url, Encoding.UTF8)
                         .ToList();
            var listbigs = new List<BigData>();
            foreach (var details in result)
            {
                listbigs.Add(new BigData
                {
                    Name = details.Result.Name,
                    Type = details.Result.Type,
                    Email = details.Result.Email,
                    Email1 = details.Result.Email1,
                    Phone = details.Result.Phone,
                    Phone1 = details.Result.Phone1,
                    Address = details.Result.Address,
                    City = details.Result.City,
                    Region = details.Result.Region,
                    Slsman = details.Result.Slsman,
                    Not = details.Result.Not,

                });
                
            }
            foreach (var item in listbigs)
            {
                item.City = item.City.ToUpper();
            }
           
            _bigDataManager.AddRange(listbigs);
            return new SuccessResult();
        }

        [HttpPost]
        [Route("BigData/ImportExcel")]
        public async Task<IResult> ImportExcel(List<IFormFile> file)
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            // excel dosyamızı stream'e çeviriyoruz
            using (var ms = new MemoryStream())
            {
                file.First().CopyTo(ms);

                // excel dosyamızı streamden okuyoruz
                using (var workbook = new XLWorkbook(ms))
                {
                    var worksheet = workbook.Worksheet(1); // sayfa 1

                    // sayfada kaç sütun kullanılmış onu buluyoruz ve sütunları DataTable'a ekliyoruz, ilk satırda sütun başlıklarımız var
                    int i, n = worksheet.Columns().Count();
                    for (i = 1; i <= n; i++)
                    {
                        dt.Columns.Add(worksheet.Cell(1, i).Value.ToString());
                    }

                    // sayfada kaç satır kullanılmış onu buluyoruz ve DataTable'a satırlarımızı ekliyoruz
                    n = worksheet.Rows().Count();
                    for (i = 2; i <= n; i++)
                    {
                        DataRow dr = dt.NewRow();

                        int j, k = worksheet.Columns().Count();
                        for (j = 1; j <= k; j++)
                        {
                            // i= satır index, j=sütun index, closedXML worksheet için indexler 1'den başlıyor, ama datatable için 0'dan başladığı için j-1 diyoruz
                            dr[j - 1] = worksheet.Cell(i, j).Value;
                        }

                        dt.Rows.Add(dr);
                    }
                }
            }

            // tablomuzu json formatına çeviriyoruz
            dynamic json = JsonConvert.SerializeObject(dt);
            dynamic listjson = JsonConvert.DeserializeObject<dynamic>(json);
            var listbigs = new List<BigData>();

            foreach (var item in listjson)
            {
                listbigs.Add(new BigData
                {
                    Name = item.Name,
                    Email = item.Email,
                    Email1 = item.Email1,
                    Phone = item.Phone,
                    Phone1 = item.Phone1,
                    Address = item.Address,
                    City = item.City,
                    Region = item.Region,
                    Slsman = item.Slsman,
                    Not = item.Not,
                    CariKodu=item.CariKodu
                    


                });

            }

            _bigDataManager.AddRange(listbigs);

            return new SuccessResult();
         
        }


        public async Task<string> GetUploadFile(IFormFile file, string name)
        {
            FileStream stream;
            string path = Path.Combine(Directory.GetCurrentDirectory(), string.Format("wwwroot/{0}/", name));
            if (!Directory.Exists(path))
            {
                DirectoryInfo dir = Directory.CreateDirectory(path);
            }
            string filename = Guid.NewGuid().ToString();
            filename = EnsureCorrectFilename(filename);

            //if (!new string[] { ".jpg", ".jpeg", ".png", ".gif" }.Contains(Path.GetExtension(file.FileName)))
            //{
            //    return "not-image";
            //}

            using (FileStream output = System.IO.File.Create(path + "/" + filename + Path.GetExtension(file.FileName)))
            {
                await file.CopyToAsync(output);
            }

            return Path.Combine(string.Format("/{0}/", name) + filename + Path.GetExtension(file.FileName));
        }
        private string EnsureCorrectFilename(string filename)
        {
            if (filename.Contains("\\"))
                filename = filename.Substring(filename.LastIndexOf("\\") + 1);

            return filename;
        }




        private class CsvUserDetailsMapping : CsvMapping<BigData>
        {
            public CsvUserDetailsMapping()
                : base()
            {
                MapProperty(0, x => x.Name);
                MapProperty(1, x => x.Type);
                MapProperty(2, x => x.Email);
                MapProperty(3, x => x.Email1);
                MapProperty(4, x => x.Phone);
                MapProperty(5, x => x.Phone1);
                MapProperty(6, x => x.Address);
                MapProperty(7, x => x.City);
                MapProperty(8, x => x.Town);
                MapProperty(9, x => x.Region);
                MapProperty(10, x => x.Slsman);
                MapProperty(11, x => x.Not);

            }
        }


    }
}
