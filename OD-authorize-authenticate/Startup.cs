using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace OD_authorize_authenticate
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
            services.AddControllersWithViews();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            }).AddCookie(configureOptions =>
                {
                    //configureOptions.Cookie.Path = "/";
                    configureOptions.LoginPath = "/Home/Login";
                    configureOptions.LogoutPath = "/Home/Logout";
                    configureOptions.AccessDeniedPath = "/Home/Login";
                    configureOptions.Cookie.Name = "ODExample.Cookie.Auth.C.Sharp.Is.Best";
                });

            services.AddAuthorization(configure =>
            {
                //configure.DefaultPolicy = AuthorizationPolicy.
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseFileServer();
                
            app.UseRouting();

            app.UseStatusCodePages(async context =>
            {
                context.HttpContext.Response.ContentType = "text/html";

                await context.HttpContext.Response.WriteAsync(
                    "<html><body><div style='display:flex;flex-direction:row;justify-content:center;padding-left:40pt;font-size:48;color:#2196f3'>"
                    + context.HttpContext.Response.StatusCode + "</div>"
                    + "<div style='display:flex;flex-direction:row;justify-content:center;padding-left:40pt;font-size:48;color:#383f51'>"
                    + ((HttpStatusCode)(context.HttpContext.Response.StatusCode)).ToString()
                    + "</div></body></html>");
            });

            var cookiePolicyOptions = new CookiePolicyOptions
            {
                MinimumSameSitePolicy = SameSiteMode.Lax
            };
            
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCookiePolicy(cookiePolicyOptions);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
