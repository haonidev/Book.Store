using Book.Store.Application.Mappings;
using Book.Store.Business.Interfaces;
using Book.Store.Business.Services;
using Book.Store.Data.Context;
using Book.Store.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Book.Store.Application
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddDbContext<BookStoreDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly("Book.Store.Data")));

            services.AddAutoMapper(typeof(MappingProfile));

            services.AddScoped<IAssuntoRepository, AssuntoRepository>();
            services.AddScoped<ILivroRepository, LivroRepository>();
            services.AddScoped<ICanalRepository, CanalRepository>();
            services.AddScoped<IAutorRepository, AutorRepository>();
            services.AddScoped<IVendaRepository, VendaRepository>();

            services.AddScoped<IAssuntoService, AssuntoService>();

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

                endpoints.MapGet("/health-check", async context =>
                {
                    await context.Response.WriteAsync("Healthy");
                });
            });
        }
    }
}
