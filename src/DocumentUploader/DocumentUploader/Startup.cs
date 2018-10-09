using DocumentUploader.DataAccess;
using DocumentUploader.DataAccess.EntityFramework;
using DocumentUploader.DataAccess.FileStorage;
using DocumentUploader.DataAccess.Helpers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DocumentUploader
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
            services.AddCors(option =>
                option.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().AllowCredentials()));

            var connectionStr = Configuration.GetConnectionString("DefaultConnection");
            services.AddTransient<IDatabaseProvider, EFDatabaseProvider>(provider =>
            {
                return new EFDatabaseProvider(connectionStr);
            });
            services.AddTransient<IFileNameGenerator, FileNameGenerator>();
            services.AddTransient<IFileStorage, LocalFileStorage>();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseCors("CorsPolicy");

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
