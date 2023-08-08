 using AsyncInn.Data;
using AsyncInn.Interfaces;
using AsyncInn.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.Options;
using Microsoft.EntityFrameworkCore;

namespace AsyncInn
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers().AddNewtonsoftJson(options =>
       options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
     );
            //Dependency Injection 
            //Add scoped / transient / singelton   => depends on timelife 
            builder.Services.AddTransient<IUserService,IdentityUserService>();
            builder.Services.AddScoped<IHotels,HotelServices>();
            builder.Services.AddScoped<IHotelRoom, HotelRoomServices>();
            builder.Services.AddTransient<IRooms, RoomsServices>();
            builder.Services.AddTransient<IAmenities, AmenitiesServices>();

            string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<AsyncInnDbContext>
                (options => options.UseSqlServer(connectionString));

            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo()
                {
                    Title = "Async INN Hotel API",
                    Version = "v1",
                });
            }
            );

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.User.RequireUniqueEmail = true;

            })
                .AddEntityFrameworkStores<AsyncInnDbContext>();


            var app = builder.Build();

            app.UseSwagger(options =>
            {
                options.RouteTemplate = "/api/{documentName}/swagger.json";
            }
            );
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/api/v1/swagger.json", "Async INN Hotel API");
                options.RoutePrefix = "docs";
            });


            


            app.MapControllers();

            app.MapGet("/", () => "Hello World!");

            app.Run();
        }
    }
}