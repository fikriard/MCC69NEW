using API.Context;
using CLIENT.Repositories.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CLIENT
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
            services.AddDbContext<MyContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("connection")));

            services.AddScoped<RegionRepository>();
            services.AddScoped<CountryRepository>();
            //services.AddScoped<LocationRepository>();
            //services.AddScoped<DepartmentRepository>();
            services.AddScoped<EmployeesRepository>();
            //services.AddScoped<JobsRepository>();
            //services.AddScoped<JobHistoryRepository>();
            //services.AddScoped<UserRepository>();
            //services.AddScoped<RoleRepository>();
            //services.AddScoped<UserRoleRepository>();
            services.AddSession(option =>
            {
                option.IdleTimeout = TimeSpan.FromMinutes(15);
            });
            services.AddHttpContextAccessor();
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                o.RequireHttpsMetadata = false;
                var Key = Encoding.UTF8.GetBytes(Configuration["JwtConfig:secret"]);
                o.SaveToken = true;
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = "localhost",
                    ValidAudience = "localhost",
                    IssuerSigningKey = new SymmetricSecurityKey(Key),
                    ClockSkew = TimeSpan.Zero
                };
            });

            services.AddSession();
            /*services.JWTConfigure(Configuration);*/
            
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
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseSession();
 
            // Custome Error page
            app.UseStatusCodePages(async context => {
                var request = context.HttpContext.Request;
                var response = context.HttpContext.Response;
                if (response.StatusCode.Equals((int)HttpStatusCode.NotFound))
                {
                    response.Redirect("../home/NotFound404");
                }
                else if (response.StatusCode.Equals((int)HttpStatusCode.Forbidden))
                {
                    response.Redirect("../home/Forbidden");
                }
                else if (response.StatusCode.Equals((int)HttpStatusCode.Unauthorized))
                {
                        response.Redirect("../home/Unauth");
                   
                }
            });

            app.Use(async (HttpContext context, Func<Task> next) =>
            {
                var token = context.Session.GetString("token");
                if (!string.IsNullOrEmpty(token))
                {
                    context.Request.Headers.Add("Authorization", "Bearer " + token.ToString());
                }

                await next();
            });

            app.UseAuthentication();
            app.UseAuthorization();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            app.UseStaticFiles();
        }
    }
}
