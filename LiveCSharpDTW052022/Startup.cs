using Mercadona.Repository.config;
using Mercadona.Repository.Links;
using Mercadona.Repository.Produits;
using Mercadona.Repository.Categorie;
using Mercadona.Repository.User;
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
using Mercadona.Repository.Promotion;
using Mercadona.Repository.ContactMessage;

namespace Mercadona
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

            services.AddTransient<IBaseRepository, BaseRepository>();
            services.AddTransient<ILinkRepository, LinkRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IProduitRepository, ProduitRepository>();
            services.AddTransient<ICategorieRepository, CategorieRepository>();
            services.AddTransient<IPromotionRepository, PromotionRepository>();
            services.AddTransient<IContactMessageRepository, ContactMessageRepository>();



            services.AddControllersWithViews();
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
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
