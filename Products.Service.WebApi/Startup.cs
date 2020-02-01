using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Products.Service.Business;
using Products.Service.Domain;
using Products.Service.Interfaces.Business;
using Products.Service.Interfaces.Repository;
using Products.Service.Repositories;
using Products.Service.Repositories.Mappers;
using Products.Service.Repositories.Repositories;
using Products.Service.WebApi.Logging;

namespace Products.Service.WebApi
{
    public class Startup
    {
        private readonly string _workingDirectory;
        private readonly AppConfiguration _appConfiguration;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            _workingDirectory = Path.GetDirectoryName(Assembly.GetAssembly(typeof(Startup)).Location);
            ConfigurationManager.Initialize();
            _appConfiguration = ConfigurationManager.Get();

        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = _appConfiguration.ConnectionStrings.Database
            .Replace("{DataDirectory}", Path.Combine(_workingDirectory, "Data"));

            services.AddSingleton(_appConfiguration);
            services.AddSingleton(Logger.CreateInstance());
            services.AddDbContext<ProductsServiceDbContext>(b => b.UseSqlServer(connectionString));
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductOptionRepository, ProductOptionRepository>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IProductOptionService, ProductOptionService>();
            
            EntitiesMapper.Initialize();

            services.AddMvc();
            services.AddMvcCore().AddApiExplorer();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
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
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
