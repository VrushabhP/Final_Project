using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using System.Threading.Tasks;
using System;

namespace Server
{
    public class Startup
    {
         public void ConfigureServices(IServiceCollection services)
        {
            var secretBytes = Encoding.UTF8.GetBytes(Constants.Secret);
            var key = new SymmetricSecurityKey(secretBytes);
            services.AddAuthentication("OAuth")
                .AddJwtBearer("OAuth", config => {

                    //PASS ACCESS TOKEN IN THE URL
                    config.Events = new JwtBearerEvents()
                    {
                        OnMessageReceived = context =>
                        {
                            if(context.Request.Query.ContainsKey("access_token"))
                            {
                                context.Token = context.Request.Query["access_token"];
                            }
                            return Task.CompletedTask;
                        }

                    };


                    config.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ClockSkew = TimeSpan.Zero,
                        ValidIssuer = Constants.Issuer,
                        ValidAudience = Constants.Audience,
                        IssuerSigningKey = key,
                    };
                });
            

            services.AddControllersWithViews()
                .AddRazorRuntimeCompilation();
            services.AddRazorPages();
            
        }

         public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
                endpoints.MapRazorPages();
            });
        }
    }
}
