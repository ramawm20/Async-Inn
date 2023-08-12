 using AsyncInn.Data;
using AsyncInn.Interfaces;
using AsyncInn.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
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
            //Each time create new one
            builder.Services.AddScoped<JwtTokenService>();
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

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
  .AddJwtBearer(options =>
  {
      // Tell the authenticaion scheme "how/where" to validate the token + secret
      options.TokenValidationParameters = JwtTokenService.GetValidationParameters(builder.Configuration);
  });


            builder.Services.AddAuthorization(options =>
            {
                // Add "Name of Policy", and the Lambda returns a definition
                options.AddPolicy("create", policy => policy.RequireClaim("permissions", "create"));
                options.AddPolicy("update", policy => policy.RequireClaim("permissions", "update"));
                options.AddPolicy("delete", policy => policy.RequireClaim("permissions", "delete"));
            });

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


            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.MapGet("/", () => "Hello World!");

            app.Run();
        }
    }
}