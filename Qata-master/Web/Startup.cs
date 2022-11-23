using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business;
using Business.AccountManagement;
using Business.AppUserManagement;
using Business.ConfigurationManagement;
using Business.CustomerConsignmentManagement;
using Business.CustomerManagement;
using Business.OrderItemManagement;
using Business.OrderManagement;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.Cookies;
using Business.LogoManagement;
using Business.ItemManagemnet;
using Business.BigDataManagenment;
using Business.NotManagement;
using Business.SettingManager;
using Business.NetgsmManagement;
using Business.ErpSalesManagement;
using Business.ErpReportManagement;

namespace Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddMvc().AddSessionStateTempDataProvider();
            services.AddSession();
            services.AddControllersWithViews();
            services.AddRazorPages().AddNewtonsoftJson();
            services.AddDistributedMemoryCache();

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.  
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.AddControllers();
            services.AddControllersWithViews().AddRazorRuntimeCompilation();
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddHttpContextAccessor();
            services.AddMvc();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Latest);
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
              .AddCookie(options =>
              {
            
                  options.LoginPath = new PathString("/Auth/Login");
                  options.LogoutPath = new PathString("/Auth/Logout");
                  options.AccessDeniedPath = new PathString("/Auth/Login");
                  options.SlidingExpiration = true;
                  options.ExpireTimeSpan = TimeSpan.FromDays(1);
            
              });



            services.AddSingleton<INetgsmManagement, NetgsmManagement>();
            services.AddSingleton<INotificationDal, EfNotificationDal>();

            services.AddSingleton<IErpReportManagement, ErpReportManagement>();

            services.AddSingleton<IErpSalesManagement, ErpSalesManagement>();

            services.AddSingleton<INotDal, EfNotDal>();
            services.AddSingleton<INotManager, NotManager>();         
            
            services.AddSingleton<ISettingDal, EfSettingDal>();
            services.AddSingleton<ISettingManager, SettingManager>();

            services.AddSingleton<IBigDataManager, BigDataManager>();
            services.AddSingleton<IBigDataDal, EfBigDataDal>();
            services.AddSingleton<IItemDal, EfItemDal>();
            services.AddSingleton<ISettinManager, SettinManager>();
            services.AddSingleton<IArticleService, ArticleManager>();
            services.AddSingleton<IArticleDal, EfArticleDal>();
            services.AddSingleton<IAppUserService, AppUserManager>();
            services.AddSingleton<IAppUserDal, EfAppUserDal>();
            services.AddSingleton<IAuthService, AuthManager>();
            services.AddSingleton<ICustomerDal, EfCustomerDal>();
            services.AddSingleton<ICustomerService, CustomerManager>();
            services.AddSingleton<IOrderService, OrderManager>();
            services.AddSingleton<IOrderDal, EfOrderDal>();
            services.AddSingleton<IOrderItemService, OrderItemManager>();
            services.AddSingleton<IOrderItemDal, EfOrderItemDal>();
            services.AddSingleton<IConfigureService, ConfigureManager>();
            services.AddSingleton<IConfigureDal, EfConfigureDal>();
            services.AddSingleton<ICityDal, EfCityDal>();
            services.AddSingleton<ITownDal, EfTownDal>();
            services.AddSingleton<IConsignmentDal, EfConsignmentDal>();
            services.AddSingleton<IConsignmentService, ConsignmentManager>();
            services.AddSingleton<ILogoManagement, LogoManagement>();
            services.AddSingleton<ILogoService, LogoService>();

            services.AddHostedService<AppBackgroundServices>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
            });


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
