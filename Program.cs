using customOrder.Models;
using customOrder.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

namespace customOrder
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();

            // Register DbContext
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Register services
            builder.Services.AddScoped<ISubscriptionService, SubscriptionService>();
            builder.Services.AddScoped<IOrderService, OrderService>();

            // Add CORS policy

            var allawOrigin = builder.Configuration.GetValue<string>("AllowedOrigin")!.Split(",");

            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                {
                    policy.WithOrigins(allawOrigin).AllowAnyHeader().AllowAnyMethod();

                });


            });

            

            // Add Swagger/OpenAPI
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseStaticFiles(); // هذه الخدمة الأساسية لـ wwwroot

            // إذا كنت تريد إعداد خاص لمجلد Uploads
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(builder.Environment.WebRootPath, "Uploads")),
                RequestPath = "/Uploads" // هذا هو المسار الذي سيستخدم للوصول من المتصفح
            });
            app.UseHttpsRedirection();

            // Enable CORS middleware - يجب أن يكون قبل UseAuthorization و MapControllers
            app.UseCors();

            app.UseAuthorization();
            app.MapControllers();

           
            app.Run();
        }
    }
}