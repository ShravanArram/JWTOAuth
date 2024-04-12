using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //This tells you need to authentication and that needs to be done through cookie and cookie Name
            services.AddAuthentication("OAuth")
                    .AddJwtBearer("OAuth",config =>
                        {
                            var secretBytes = Encoding.UTF8.GetBytes(Constant.Secret);
                            var key = new SymmetricSecurityKey(secretBytes);
                            config.TokenValidationParameters = new TokenValidationParameters()
                            {
                                ValidIssuer = Constant.Issuer,
                                ValidAudience = Constant.Audience,
                                IssuerSigningKey = key
                            };

                        });

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
