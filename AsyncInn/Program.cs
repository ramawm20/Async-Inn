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

            builder.Services.AddControllers().AddNewtonsoftJson(options =>
       options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
     );
            //Dependency Injection 
            //Add scoped / transient / singelton   => depends on timelife 
            builder.Services.AddScoped<IHotels,HotelServices>();
            builder.Services.AddScoped<IHotelRoom, HotelRoomServices>();
            builder.Services.AddTransient<IRooms, RoomsServices>();
            builder.Services.AddTransient<IAmenities, AmenitiesServices>();

            string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<AsyncInnDbContext>
                (options => options.UseSqlServer(connectionString));

            //builder.Services.AddEndpointsApiExplorer();
            //builder.Services.AddSwaggerGen();

            var app = builder.Build();
            //Configure the HTTP request pipeline.
            //if (app.Environment.IsDevelopment())
            //{
            //    app.UseSwagger();
            //    app.UseSwaggerUI();
            //}
            //app.UseDeveloperExceptionPage();

            //app.UseHttpsRedirection();

            //app.UseAuthorization();


            app.MapControllers();

            app.MapGet("/", () => "Hello World!");

            app.Run();
        }
    }
}