using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using ValidationPrototype.Extensions;
using ValidationPrototype.Services;

namespace ValidationPrototype
{
	public class Startup
	{
		public IConfiguration Configuration { get; }

		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllers();
			services.AddSwaggerGen(c =>
			{
				c.DescribeAllParametersInCamelCase();
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "ValidationPrototype", Version = "v1" });
				c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "ValidationPrototype.xml"));
			});

			services.AddSingleton<IIdentityService, IdentityService>();
			services.AddSingleton<IEntityService, EntityService>();
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseSwagger();
				app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ValidationPrototype v1"));
			}

			app.UseGlobalErrorHandling();

			app.UseHttpsRedirection();
			app.UseRouting();
			app.UseAuthorization();

			// add custom middlewares here
			//app.UsePermissionAccessValidation();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
