using ListCRUD.Data;
using ListCRUD.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Serialization;

namespace ListCRUD
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddScoped<ApplicationDbContext>();

            string _GetConnStringMSSQL = Configuration.GetConnectionString("DefaultConnectionMSSQL");
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(_GetConnStringMSSQL));

            //string _GetConnStringMySQL = Configuration.GetConnectionString("DefaultConnectionMySQL");
            //services.AddDbContextPool<ApplicationDbContext>(options => options.UseMySql(_GetConnStringMySQL, ServerVersion.AutoDetect(_GetConnStringMySQL)));

            services.AddTransient<ICommon, Common>();

            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
            });
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

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=PersonalInfo}/{action=Index}/{id?}");
            });
        }
    }
}
