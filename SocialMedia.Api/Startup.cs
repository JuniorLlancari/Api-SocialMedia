using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SocialMedia.Core.Interfaces;
using SocialMedia.Infrastructure.Repositories;
using SocialMedia.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using SocialMedia.Infrastructure.Filters;

namespace SocialMedia.Api
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

            


            //CONFIGURAR AUTOMAPER ,  solo se mapea una vez, obtener los ascembli busca en nues 2
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


            //BREAK REFERENCE CIRCLE 1
            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            })
            //QUITAMOS LAS VALIDADCION DE APICONTROLLER SOBRE EL MODELO  3
            .ConfigureApiBehaviorOptions(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });
            
            

            services.AddTransient<IPostRepository, PostRepository>();
            services.AddDbContext<SocialMediaContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("SocialMedia")));
            //APLICANDO FILTRO DE FORMA GLOBAL 4
            services.AddMvc(options =>
            {
                options.Filters.Add<ValidationFilter>();
            });
 
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

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
