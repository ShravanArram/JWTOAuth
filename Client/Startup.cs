using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(config =>
            {
                config.DefaultAuthenticateScheme = "ClientCookie";
                config.DefaultSignInScheme = "ClientCookie";
                config.DefaultChallengeScheme = "OurServer";
                
            })
                    
                 .AddCookie("ClientCookie")
                 .AddOAuth("OurServer", config =>
                    {
                        config.ClientId = "client_id";
                        config.ClientSecret = "client_secret";
                        config.CallbackPath = "/oauth/callback";
                        config.AuthorizationEndpoint = "http://localhost:33742/oauth/authorize";
                        config.TokenEndpoint = "http://localhost:33742/oauth/token";
                        config.SaveTokens = true;


                    });
                
                ;
            services.AddControllersWithViews().
                    AddRazorRuntimeCompilation();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting(); // looks at the route you are accessing and then decides which end pointo use

            app.UseAuthentication(); //Who are you

            app.UseAuthorization(); //are you allowed

            //Actual functianlity
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();

            });
        }
    }
}
