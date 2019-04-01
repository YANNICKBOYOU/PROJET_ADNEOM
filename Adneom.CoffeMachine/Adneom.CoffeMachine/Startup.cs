using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Adneom.CoffeMachine.Domain.Entities;
using Adneom.CoffeMachine.Domain.Repository;
using Adneom.CoffeMachine.Domain.UnitOfWork;
using Adneom.CoffeMachine.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Adneom.CoffeMachine
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
               
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            var connstring = Configuration.GetConnectionString("DefaultConnection");

            if (connstring == null) throw new ArgumentNullException(nameof(connstring));

            var connSettings = Configuration.GetSection("ConnectionSettings").Get<ConnectionSettings>();
            if (connSettings == null) throw new ArgumentNullException(nameof(connSettings));

            connstring = connstring.Replace("{database}", connSettings.Database)
                .Replace("{username}", connSettings.Username)
                .Replace("{password}", connSettings.Password)
                .Replace("{server}", connSettings.Server);


            services.AddDbContext<DbEntityContext>(op =>
            op.UseSqlServer(connstring));

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddTransient<IUnitOfWork, UnitOfWork>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }


           //app.ApplicationServices.GetService<IUnitOfWork>();
          
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
