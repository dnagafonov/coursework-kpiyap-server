using coursework_kpiyap.Models;
using coursework_kpiyap.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace coursework_kpiyap
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder.WithOrigins("httpû://example.com");
                    });
            });

            services.Configure<ServiceStoreDatabaseSettings>(
                Configuration.GetSection(nameof(ServiceStoreDatabaseSettings)));

            services.AddSingleton<IServiceStoreDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<ServiceStoreDatabaseSettings>>().Value);

            services.AddSingleton<ServiceService>();

            services.Configure<SpareStoreDatabaseSettings>(
                Configuration.GetSection(nameof(SpareStoreDatabaseSettings)));

            services.AddSingleton<ISpareStoreDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<SpareStoreDatabaseSettings>>().Value);

            services.AddSingleton<SpareService>();

            services.Configure<AccountStoreDatabaseSettings>(
                Configuration.GetSection(nameof(AccountStoreDatabaseSettings)));

            services.AddSingleton<IAccountStoreDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<AccountStoreDatabaseSettings>>().Value);

            services.AddSingleton<AccountService>();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
