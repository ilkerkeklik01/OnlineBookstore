using Application.Services.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Contexts;
using Persistence.Repositories;
using System.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Core.Persistence.Repositories;
using Domain.Entities;

namespace Persistence
{
    public static class PersistenceServiceRegistration
    {
        //class and method must be static while using extension
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            
            var cnnString = configuration.GetConnectionString("OnlineBookstoreConnectionString");
            //get and connect sql server from appsettings config file
            services.AddDbContext<BaseDbContext>(options =>
                options.UseSqlServer(cnnString));

            //dependency injections
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IOrderItemRepository, OrderItemRepository>();
            services.AddScoped<IReviewRepository, ReviewRepository>();
            services.AddScoped<IBookshelfRepository, BookshelfRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();



            //Reflections
            //var repositoryInterfaces = AppDomain.CurrentDomain.GetAssemblies()
            //    .SelectMany(x => x.GetTypes())
            //    .Where(x => typeof(IBaseRepository).IsAssignableFrom(x) && x.IsInterface).ToList();

            //foreach (Type repositoryInterface in repositoryInterfaces)
            //{
            //    var repositoryImplementation = AppDomain.CurrentDomain.GetAssemblies()
            //            .SelectMany(x => x.GetTypes())
            //            .Where(x => repositoryInterface.IsAssignableFrom(x) && !x.IsInterface).FirstOrDefault();
            //    if (repositoryImplementation != null)
            //    {
            //        services.AddScoped(repositoryInterface, repositoryImplementation);
            //    }
            //}



            //services.Scan(scan => scan.FromAssemblies(Persistence.AssemblyReference.Assembly).AddClasses().AsImplementedInterfaces().WithScopedLifetime());


            return services;
        }

    }
}
