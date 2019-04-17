using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Model.DAL;
using Model.Infrastructure;
using Model.UnitOfWork;
using System.IO;

namespace PishtazanGroupEm
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            ////-------------- for automapper ----------------------------------
            AutoMapperConfiguration.InitializeAutoMapper();
            //------------------------------------------------------------

            // appsetting  تنطیم کردن خواندن از  
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();

        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            //------------------------ set the database and its identity setting--------------------------------
            //verify Connection String To app
            services.AddDbContext<ApplicationDbContext>(option =>
            option.UseSqlServer(Configuration.GetConnectionString("MyConnectionString")));

            //verify Idenity service
            services.AddIdentity<ApplicationUsers, ApplicationRoles>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders(); //به هرکاربردر هنگام لاگین بودن یک توکن اختصاص میدهد  باتغیرر توکن لاگ وت میشود
                                             //-----------------------------------------------------------------------

            //-------------------------- set the services and Unit of work and repository setting ---------------------------------------------

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            //----------------------------------------------------------------------------

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                //------------------------------NPM  node_modules  دسترسی دادن پروژه به فایل های   --------------------------------------------------
                //----------- برای آپدیت کردن کتابخانه های کلاینت NPM  node_modules  ..------------------
                app.UseStaticFiles(new StaticFileOptions
                {
                    FileProvider = new PhysicalFileProvider(Path.Combine(env.ContentRootPath, "node_modules")),
                    RequestPath = "/" + "node_modules"
                });
                //--------------------------------------------------=-------


            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                  name: "areas",
                  template: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}"
                    );
            });
        }
    }
}
