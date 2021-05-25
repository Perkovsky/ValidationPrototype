using FluentValidation.AspNetCore;
using MicroElements.Swashbuckle.FluentValidation;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;
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
			services
				.AddControllers()
				.AddFluentValidation(c =>
				{
					c.RegisterValidatorsFromAssemblyContaining<Startup>();

					//// Optionally set validator factory if you have problems with scope resolve inside validators.
					//c.ValidatorFactoryType = typeof(HttpContextServiceProviderValidatorFactory);
				});
			
			services.AddSwaggerGen(c =>
			{
				c.DescribeAllParametersInCamelCase();
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "ValidationPrototype", Version = "v1" });
				c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "ValidationPrototype.xml"));
				//c.AddFluentValidationRules();
			});
			//services.AddFluentValidationRulesToSwagger();

			services.AddScoped<IEntityValidationService, EntityValidationService>();
			services.AddScoped<IEntityService, EntityService>();
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
