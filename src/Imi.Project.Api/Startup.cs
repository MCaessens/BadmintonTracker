using System;
using System.Text;
using Imi.Project.Api.Core.AuthorizationRequirements;
using Imi.Project.Api.Core.Entities;
using Imi.Project.Api.Core.Interfaces.Repositories;
using Imi.Project.Api.Core.Interfaces.Services;
using Imi.Project.Api.Core.Services;
using Imi.Project.Api.Infrastructure.Data;
using Imi.Project.Api.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace Imi.Project.Api
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
            services.AddControllers();
            services.AddDbContext<BadmintonDbContext>(options => { options.UseSqlServer(Configuration.GetConnectionString("BadmintonDb")); });

            services.AddIdentity<User, IdentityRole<Guid>>(options =>
            {
                options.SignIn.RequireConfirmedEmail = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireDigit = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequiredLength = 5;
            }).AddEntityFrameworkStores<BadmintonDbContext>();

            services.AddAuthentication(option =>
                {
                    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(jwtOptions =>
                {
                    jwtOptions.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateActor = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidIssuer = Configuration["JWTConfiguration:Issuer"],
                        ValidAudience = Configuration["JWTConfiguration:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWTConfiguration:SigningKey"]))
                    };
                });

            services.AddRouting(options => options.LowercaseUrls = true);

            // AccountIntegrityRequirement kijkt na of de AccountIntegrityId in de claims nog hetzelfde is als die in de database
            // Op deze manier heb ik voorkomen dat oude JWT tokens je nog toegang gaven tot je "account" (Uiteraard wordt dit enkel gedaan wanneer je uitlogt)
            services.AddAuthorization(options =>
            {
                options.AddPolicy(Constants.AdminPolicyName, policyOption =>
                {
                    policyOption.RequireRole(Constants.AdminRoleName);
                    policyOption.AddRequirements(new AccountIntegrityRequirement());
                });
                options.AddPolicy(Constants.UserPolicyName, policyOption =>
                {
                    policyOption.RequireAssertion(option =>
                    {
                        if (option.User.IsInRole(Constants.AdminRoleName) || option.User.IsInRole(Constants.UserRoleName))
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    });
                    policyOption.AddRequirements(new AccountIntegrityRequirement());
                });
            });

            // Repositories
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IGameRepository, GameRepository>();
            services.AddTransient<IRacketRepository, RacketRepository>();
            services.AddTransient<ILocationRepository, LocationRepository>();
            services.AddTransient<IShuttleCockRepository, ShuttleCockRepository>();

            // Services
            services.AddTransient<IAccountsService, AccountsService>();
            services.AddTransient<IUsersService, UsersService>();
            services.AddTransient<IMeService, MeService>();
            services.AddTransient<IGamesService, GamesService>();
            services.AddTransient<IRacketsService, RacketsService>();
            services.AddTransient<ILocationsService, LocationsService>();
            services.AddTransient<IShuttleCocksService, ShuttleCocksService>();
            services.AddTransient<IImageService, ImageService>();
            services.AddTransient<IAuthorizationHandler, AccountIntegrityHandler>();

            services.AddHttpContextAccessor();

            services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Description = "Example: Bearer {YOUR JWT TOKEN}",
                    Type = SecuritySchemeType.ApiKey,
                    BearerFormat = "JWT",
                    Scheme = "Bearer",
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] { }
                    }
                });
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "BadmintonTracker API",
                    Description = "An API used for the BadmintonTracker app.",
                });
            });
            services.AddCors(options => { options.AddPolicy("localhost", e => e.WithOrigins("https://localhost:44373").WithHeaders("Content-Type", "Authorization").WithMethods("GET", "PUT", "POST", "DELETE")); });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "BadmintonTracker API");
                c.RoutePrefix = string.Empty;
                c.DocumentTitle = "BadmintonTracker API";
            });
            app.UseStaticFiles();

            app.UseRouting();

            app.UseCors("localhost");
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}