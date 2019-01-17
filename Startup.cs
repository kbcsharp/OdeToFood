using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OdeToFood.Services;

namespace OdeToFood
{
  public class Startup
  {
    // This method gets called by the runtime. Use this method to add services to the container.
    // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddSingleton<IGreeter, Greeter>();
      services.AddScoped<IRestaurantData, InMemoryRestaurantData>();
      services.AddMvc()
      // (options => options.EnableEndpointRouting = false)
      .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env, IGreeter greeter, ILogger<Startup> logger)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      // app.UseFileServer(); //this installs the two below
      // app.UseDefaultFiles();
      app.UseStaticFiles();

      app.UseMvc(ConfigureRoutes);


      // app.Use(next =>
      // {
      //   return async context =>
      //   {
      //     logger.LogInformation("Request Incoming");

      //     if (context.Request.Path.StartsWithSegments("/mym"))
      //     {
      //       await context.Response.WriteAsync("Hit!");
      //       logger.LogInformation("Request Handled");
      //     }
      //     else
      //     {
      //       await next(context);
      //       logger.LogInformation("Response Outgoing");
      //     }
      //   };
      // });

      // app.UseWelcomePage(new WelcomePageOptions
      // {
      //   Path = "/wp"
      // });

      app.Run(async (context) =>
      {

        var greeting = greeter.GetMessageOfTheDay();
        await context.Response.WriteAsync($"{greeting} : {env.EnvironmentName}");
      });
    }

    private void ConfigureRoutes(IRouteBuilder routeBuilder)
    {
      routeBuilder.MapRoute("Default",
         "{controller=Home}/{action=Index}/{id?}");
    }
  }
}
