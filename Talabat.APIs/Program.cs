using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Talabat.APIs.Errors;
using Talabat.APIs.Helpers;
using Talabat.APIs.Middleware;
using Talabat.Core.IRepo;
using Talabat.Repository;
using Talabat.Repository.Data;

namespace Talabat.APIs
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<StoreContext>(options=>{
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")); 
            });

            builder.Services.AddScoped(typeof(IGinaricRepository<>),typeof(GinaricRepository<>));
            builder.Services.AddAutoMapper(typeof(MappingProfiles));
            builder.Services.Configure<ApiBehaviorOptions>(opt =>
            {
                opt.InvalidModelStateResponseFactory = _actionContext =>
                {
                    var errors = _actionContext.ModelState.Where(p => p.Value.Errors.Count() > 0)
                                                          .SelectMany(p=>p.Value.Errors)
                                                          .Select(e=>e.ErrorMessage)
                                                          .ToList();
                    var VaIidationErrorResponse = new ApiVaIidationErrorResponse() 
                    {
                        Errors=errors
                    
                    };
                    return new BadRequestObjectResult(VaIidationErrorResponse);

                };
            });
         







            var app = builder.Build();

            
            
            # region Update Database
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                
               
                var LoggerFactory = services.GetRequiredService<ILoggerFactory>();
                try
                {
                    var dbContext = services.GetRequiredService<StoreContext>();
                    await dbContext.Database.MigrateAsync();
                    await StoreContextSeed.SeedAsync(dbContext);
                }
                catch (Exception ex)
                {
                    var Logger = LoggerFactory.CreateLogger<Program>();
                    Logger.LogError(ex, "An Error Occured During Appling The Migration");
                }
            }
           
            


                #endregion




                // Configure the HTTP request pipeline.
                if (app.Environment.IsDevelopment())
                {
                app.UseMiddleware<ExceptionMiddleware>();
                    app.UseSwagger();
                    app.UseSwaggerUI();
                }
            app.UseStaticFiles();
            //app.UseStatusCodePagesWithReExecute("/errors/{0}");
            app.UseStatusCodePagesWithRedirects("/errors/{0}");
           


            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
