using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using SegurosApi.Context;
using System;

namespace SegurosApi
{
    public class Startup
    {
        readonly string MyCors = "_myCors"; 
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
                options.AddPolicy(name: MyCors, builder =>
                    {
                        // builder.WithOrigins("http://localhost:3003/");
                        builder.SetIsOriginAllowed(origin => new Uri (origin).Host == "localhost" )
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });
            });
            services.AddDbContext<AutosContext>(opt => opt.UseInMemoryDatabase("ListaAutos"));
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SegurosApi", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SegurosApi v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(MyCors);

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
