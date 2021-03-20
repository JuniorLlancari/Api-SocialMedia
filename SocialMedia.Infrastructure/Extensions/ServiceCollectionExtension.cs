using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using SocialMedia.Core.Interfaces;
using SocialMedia.Core.Options;
using SocialMedia.Core.Services;
using SocialMedia.Infrastructure.Data;
using SocialMedia.Infrastructure.Interfaces;
using SocialMedia.Infrastructure.Options;
using SocialMedia.Infrastructure.Repositories;
using SocialMedia.Infrastructure.Services;

namespace SocialMedia.Infrastructure.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void AddDbContexts(this IServiceCollection services,IConfiguration configuration)
        {

            services.AddDbContext<SocialMediaContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("SocialMedia")));
        }
        public static void AddOptions(this IServiceCollection services, IConfiguration configuration)
        {
            // como la versiones son distintas se bindea
            // si el que retorna es de tipo action  lo menejamos como lamda
            services.Configure<PaginationOptions>( options=> configuration.GetSection("Pagination").Bind(options));
            services.Configure<PasswordOptions>(options => configuration.GetSection("PasswordOptions").Bind(options));

        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            //Estos servicios retornan por eso lo manejamo asi

            services.AddTransient<IPostService, PostService>();
            services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
            services.AddTransient<IUnitOfWork, UnirOfWork>(); //unit of work
            services.AddTransient<ISecurityServices, SecurityServices>(); //unit of work
            services.AddSingleton<IPasswordService, PasswordServices>(); //unit of work             
            services.AddSingleton<IUriService>(provider =>
            {
                var accesor = provider.GetRequiredService<IHttpContextAccessor>();
                var request = accesor.HttpContext.Request;
                var absoluteUrl = string.Concat(request.Scheme, "://", request.Host.ToUriComponent());
                return new UriService(absoluteUrl);
            });
            return services;
        }

        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            //Estos servicios retornan por eso lo manejamo asi

            services.AddSwaggerGen(doc =>
            {
                doc.SwaggerDoc("v1", new OpenApiInfo { Title = "Social Media API", Version = "v1" });
            });
            return services;

        }


    }
}
