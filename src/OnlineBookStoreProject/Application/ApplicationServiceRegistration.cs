using Application.Features.Books.Rules;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Books.Commands.CreateBook;
using Application.Features.OrderItems.Rules;
using Core.Application.Pipelines.Validation;
using FluentValidation;
using MediatR;
using Application.Features.Orders.Rules;
using Application.Features.Bookshelves.Rules;
using Application.Features.Reviews.Rules;
using Application.Features.Users.Rules;

namespace Application
{
    public static class ApplicationServiceRegistration
    {

        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));



            services.AddScoped<BookBusinessRules>();
            services.AddScoped<OrderItemBusinessRules>();
            services.AddScoped<OrderBusinessRules>();
            services.AddScoped<BookshelfBusinessRules>();
            services.AddScoped<ReviewBusinessRules>();
            services.AddScoped<UserBusinessRules>();


            return services;

        }


    }
}
