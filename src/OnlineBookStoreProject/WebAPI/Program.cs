


using Application;
using Core.CrossCuttingConcerns.Exceptions;
using Persistence;
namespace WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddPersistenceServices(builder.Configuration);
            builder.Services.AddApplicationServices();


            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });


            var app = builder.Build();
            app.UseCors();
            // Configure the HTTP request pipeline.

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            


            //if (app.Environment.IsDevelopment())
            //    {
            //        app.UseDeveloperExceptionPage();
            //    }
            //    else
            //    {
            //        app.UseExceptionHandler("/Home/Error");
            //        app.UseHsts();
            //    }
            //    app.UseHttpsRedirection();
            //    app.UseStaticFiles();
            //    app.UseRouting();
            //    app.UseAuthentication();
            //    app.UseEndpoints(endpoints =>
            //    {
            //        endpoints.MapControllerRoute(
            //            name: "default",
            //            pattern: "{controller=Home}/{action=Index}/{id?}");
            //    });
            


            //if(app.Environment.IsDevelopment()) 
            app.UseDeveloperExceptionPage();
            app.ConfigureCustomExceptionMiddleware();
            
            //app.UseCors(builder =>
            //    builder.WithOrigins("http://localhost:5093").AllowAnyHeader()
            //);
            //app.UseExceptionHandler(); EF 

            app.UseAuthorization();


            app.MapControllers();//Probably old versions of NuGet packages exist

            app.Run();
        }
    }
}