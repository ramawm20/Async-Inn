 using AsyncInn.Data;
using AsyncInn.Interfaces;
using AsyncInn.Services;
using Microsoft.EntityFrameworkCore;

namespace AsyncInn
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();

            builder.Services.AddScoped<IHotels,HotelServices>();
            builder.Services.AddTransient<IRooms, RoomsServices>();
            builder.Services.AddTransient<IAmenities, AmenitiesServices>();

            string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<AsyncInnDbContext>
                (options => options.UseSqlServer(connectionString));

            var app = builder.Build();

            app.MapControllers();

            app.MapGet("/", () => "Hello World!");

            app.Run();
        }
    }
}