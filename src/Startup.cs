using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using WebAppAuthentication.Configuration;
using WebAppAuthentication.Database;
using WebAppAuthentication.Middleware;
using WebAppAuthentication.Models;
using WebAppAuthentication.Services;

namespace WebAppAuthentication
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
            services.AddScoped<IUserService, UserService>();

            services.Configure<JwtAuthenticationConfig>(Configuration.GetSection("JwtAuthentication"));

            services.AddDbContext<JwtAuthenticationDbContext>(options =>
                            options.UseSqlServer(Configuration.GetConnectionString("SqlDatabase")));

            services.AddIdentity<AppUser, IdentityRole>()
                        .AddEntityFrameworkStores<JwtAuthenticationDbContext>()
                        .AddDefaultTokenProviders();

            services.AddLogging(options =>
                options.AddConsole());

            services.AddAuthentication(Constants.JwtAuthSection.AUTHENTICATION_SCHEME)
                .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>(Constants.BasicAuthSection.AUTHENTICATION_SCHEME, null)
                .AddScheme<AuthenticationSchemeOptions, ApiKeyAuthenticationHandler>(Constants.ApiKeySection.AUTHENTICATION_SCHEME, null)
                .AddJwtBearer(Constants.JwtAuthSection.AUTHENTICATION_SCHEME, options =>
                {
                    options.SaveToken = true;
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidAudience = Configuration["JwtAuthentication:ValidAudience"],
                        ValidIssuer = Configuration["JwtAuthentication:ValidIssuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtAuthentication:Secret"]))
                    };
                });

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
