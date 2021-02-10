using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using mvccoreSeguridadProovedor.Data;

namespace mvccoreSeguridadProovedor
{
    public class Startup
    {
        private IConfiguration Configuration;
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            
            string cadenaAspNetDbSqlCasa = this.Configuration.GetConnectionString("cadenaAspNetDbSqlCasa");
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(cadenaAspNetDbSqlCasa));
            //services.AddMvc().AddMvcOptions(options=>options.EnableEndpointRouting = false);
            //DEBEMOS INDICAR QUE UTILIZAREMOS SERVICIOS DE TERCEROS PARA AUTENTICACION 
            services.AddIdentityCore<IdentityUser>().AddEntityFrameworkStores<ApplicationDbContext>();
            //INDICAMOS QUE UTILIZAREMOS LA AUTENTICACCIÓN DEL PROOVEDOR MICROSOFT 
            //services.AddAuthenticathion().AddProvider, EN EL MOMENTO DE INDICAR EL PROVEEDOR NOS PIDE LAS CLAVES 
            services.AddAuthentication(options =>
            {
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            }).AddCookie().AddMicrosoftAccount(microsoftOptions =>
            {
                microsoftOptions.ClientId = "3ea2b079-94d6-41d8-9dcb-fcbb847ee634";
                microsoftOptions.ClientSecret = "Oc5mA5bhATi5t.rn5f--R-3.2vIWzZ7RL~";
            });
            services.AddSession();
            services.AddControllersWithViews().AddRazorRuntimeCompilation();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSession();

            //app.UseMvc(routes =>
            //{
            //    routes.MapRoute(
            //        name: "default",
            //        template: "{controller=Home}/{action=Index}/{id?}"
            //        );
            //});
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}"
                );
                endpoints.MapRazorPages();
            });
        }
    }
}
