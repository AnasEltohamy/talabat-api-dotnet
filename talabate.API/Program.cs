using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using talabat.API.Errors;
using talabat.API.Helpers;
using talabat.Core.Entites.Identity;
using talabat.Core.Repositores.Contract;
using talabat.Core.Services.Contract;
using talabat.Repository;
using talabat.Repository.Data.Identity;
using talabat.Repository.Data.Store;
using talabat.Service.Account;
using talabat.Service.Orders;
using talabat.Service.Products;
using Microsoft.IdentityModel.Tokens;
using System.Drawing.Imaging;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace talabate.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);




            //configer Servises
            #region configer Servises

            // Add services to the container.
            builder.Services.AddAuthorization();
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            builder.Services.AddDbContext<StoreContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("StoreConnection"));
            });


            builder.Services.AddSingleton<IConnectionMultiplexer>(options =>
            {
                return ConnectionMultiplexer.Connect(builder.Configuration.GetConnectionString("RedisConnection"));
            });


            builder.Services.AddDbContext<IdentityContext>(opt =>
            {
                opt.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"));
            });


            builder.Services.AddIdentity<AppUser, IdentityRole>(Opt =>
            {
                Opt.Password.RequireUppercase = true;
                Opt.Password.RequireLowercase = true;
                Opt.Password.RequireNonAlphanumeric = true;
                Opt.Password.RequiredUniqueChars = 2;

            }).AddEntityFrameworkStores<IdentityContext>();


            builder.Services.AddScoped(typeof(IAuthService), typeof(AuthService));
            builder.Services.AddAuthentication(Opt =>
            {
                Opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                Opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(Opt=>{
                    Opt.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateAudience = true,
                        ValidAudience = builder.Configuration["JWT:Audience"],
                        ValidateIssuer = true,
                        ValidIssuer = builder.Configuration["JWT:Issuer"],
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:SecretKey"])),
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.FromDays(double.Parse(builder.Configuration["JWT:DurationInDays"]))
                    };
                });



            //builder.Services.AddScoped(typeof(IGenericRepositry<>), typeof(GenericRepositores<>));
            builder.Services.AddScoped(typeof(IBasketRepository), typeof(BasketRepository));
            builder.Services.AddScoped(typeof(IUnitOfWork) , typeof(UnitOfWork));
            builder.Services.AddScoped(typeof(IOrderService), typeof(OrderService));
            builder.Services.AddScoped(typeof(IProductService), typeof(ProductService));
            builder.Services.AddScoped(typeof(IPaymentService), typeof(PaymentService));
            builder.Services.AddAutoMapper(typeof(MappingProfiles));
        




            builder.Services.Configure<ApiBehaviorOptions>(Opt =>

            {
                Opt.InvalidModelStateResponseFactory = _actionContext =>
                {
                    var errors = _actionContext.ModelState
                   .Where(P => P.Value.Errors.Count() > 0)
                   .SelectMany(p => p.Value.Errors)
                   .Select(e => e.ErrorMessage).ToList();

                    var VaIidationErrorResponse = new ApiValidationErrorResponse()
                    {
                        Errors = errors
                    };


                    return new BadRequestObjectResult(VaIidationErrorResponse);
                };


            });

            builder.Services.AddCors(opt =>
            {
                opt.AddPolicy("MyPolicy", opt =>
                {
                    opt.AllowAnyHeader().AllowAnyMethod().WithOrigins(builder.Configuration["FrontBaseUrl"]);
                });
            });



            #endregion





            var app = builder.Build();

            #region Update Database
            var Scope = app.Services.CreateScope();
            var Services = Scope.ServiceProvider;
            var LoggerFactory = Services.GetRequiredService<ILoggerFactory>();
            var IdentityServies = Services.GetRequiredService<IdentityContext>();
            var DbContext = Services.GetRequiredService<StoreContext>();
            try
            {
                await DbContext.Database.MigrateAsync();
                await StoreContextSeed.Seeding(DbContext);

                await IdentityServies.Database.MigrateAsync();
                await IdentityContextSeed.Seeding(Services.GetRequiredService<UserManager<AppUser>>());
               // await IdentityContextSeed.Seeding(DbContext);
            }
            catch (Exception Ex)
            {
                var Logger = LoggerFactory.CreateLogger<Program>();
                Logger.LogError(Ex , "an error been occured during apply the migration");
            }
            #endregion










            //Configure Kestral Middlewares
            #region Configure Kestral Middlewares
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                
            }
            app.UseStatusCodePagesWithReExecute("/errors/{0}");
            app.UseHttpsRedirection();
            app.UseStaticFiles();
          

            app.UseCors("MyPolicy");
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();

            #endregion


            app.Run();
        }
    }
}
