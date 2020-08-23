using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Infrastructure.Dependency;
using MediaAssetsPersistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Persistence;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace Api
{
    public class Startup
    {
        private readonly string AllowAnyOrigins = "allowAnyOrigins";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // add CORS service
            services.AddCors(options =>
            {
                options.AddPolicy(AllowAnyOrigins,
                    builder =>
                    {
                        builder.AllowAnyOrigin();
                        builder.AllowAnyHeader();
                        builder.AllowAnyMethod();
                    });
            });

            services.AddOptions();
            services.Configure<PersistenceConfigurationKeys>(Configuration.GetSection("Persistence"));
            services.Configure<MediaAssetsPersistenceConfigurationKeys>(Configuration.GetSection("MediaAssetsPersistence"));
            
            services.ConfigureMediaAssetsManagementServices();
            services.AddControllers()
                .AddNewtonsoftJson();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(AllowAnyOrigins);

            app.UseHttpsRedirection();

            //app.UseSwagger();
            //app.UseSwaggerUI(setup =>
            //{
            //    setup.SwaggerEndpoint("../swagger/v1/swagger.json", "MediaAssetsManagement API V1");
            //    setup.DocExpansion(DocExpansion.List);
            //});

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
