using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using NETCore.MailKit.Extensions;
using NETCore.MailKit.Infrastructure.Internal;
using Services.Server.DataAccessLayer;
using Services.Server.Helper;
using Services.Server.Models;
using Services.Server.Services;

using System;
using System.IO;
using System.Linq;
using System.Text;
using Twilio.Clients;

namespace Services.Server
{
    public class Startup
    {
       
      
        private readonly IConfiguration _config;
        public Startup(IConfiguration config )
        {
            _config = config;
           
        }


        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddResponseCompression(o =>
            {
                o.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[] {"application/octet-stream" }
                );
            });
            services.AddControllersWithViews();
            services.AddRazorPages();

            services.AddDbContext<ApplicationDbContext>(options =>
               options.UseSqlServer("Data Source=PC\\SQLEXPRESS;Initial Catalog=OnlineServices;Integrated Security=True"));
            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 7;
                options.User.RequireUniqueEmail = true;


            }).AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddSignalR();
            services.AddHttpClient<ITwilioRestClient, TwilioClient>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IUserRepository<ApplicationUser>, UserRepository>();
            services.AddScoped<ISMS, SMS>();
            services.AddAuthorizationCore();
            services.AddScoped<IMailService, SendGridMailService>();
            services.AddScoped<IServicerepository, ServiceRepository>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IMailingService, MailingService>();
            services.AddMailKit(config => config.UseMailKit(_config.GetSection("Email").Get<MailKitOptions>()));
            
            services.AddMvc();

            services.AddAutoMapper(typeof(Startup));

            services.ConfigureApplicationCookie(config =>
            { 
                config.LoginPath = "/User/login";
                config.AccessDeniedPath ="/Services.Server/Pages/Error";
            }) ;
            
            services.AddCors(option =>
            {
                option.AddPolicy("Cors", policy =>
                {
                    policy.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin();

                }
                );
            });

          
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseResponseCompression();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebAssemblyDebugging();
                
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseCors("Cors");
            app.UseHttpsRedirection();
            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();
           
        
            app.UseRouting();
            app.UseAuthentication();

            app.UseAuthorization();
           
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapHub<SignalRHub>("/signalRHub");
                endpoints.MapFallbackToFile("index.html");
               
            });
           
            
          
        }
       
     
    }
}

