using AutoMapper;
using Library.Business.Mapping;
using Library.Business.Services;
using Library.Business.Services.Interfaces;
using Library.DAL.Entities;
using Library.DAL.Repositories;
using Library.DAL.UnitOfWork;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library
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
            services.AddDbContext<ApplicationContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("LibraryDefaultConnection")));
            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddAutoMapper(typeof(ModelMappingProfile));

            //services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
            //    .AddEntityFrameworkStores<ApplicationContext>();
            services.AddControllers();
            //services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            //services.AddScoped<ApplicationContext>();
            services.AddScoped<IUnitOfWork, EFUnitOfWork>();
            //services.AddScoped<IRepository<BookAuthor>, BookAuthorRepository>();
            //services.AddScoped<IRepository<Book>, BookRepository>();
            //services.AddScoped<IRepository<Country>, CountryRepository>();
            //services.AddScoped<IRepository<Person>, PersonRepository>();
            //services.AddScoped<IRepository<PublishingHouse>, PublishingHouseRepository>();
            //services.AddScoped<IRepository<PublishingHouse>, PublishingHouseRepository>();
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<ICountryService, CountryService>();
            services.AddScoped<IPersonService, PersonService>();
            services.AddScoped<IPublishingHouseService, PublishingHouseService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                    options.RoutePrefix = string.Empty;
                });
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
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

            //app.UseAuthentication();
            //app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                //endpoints.MapRazorPages();
            });
        }
    }
}
