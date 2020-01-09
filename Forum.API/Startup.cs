using AutoMapper;
using Forum.API.ViewModels;
using Forum.Data.Contexts;
using Forum.Domain.Entities;
using Forum.Domain.Interfaces;
using Forum.Infra.Repositories;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Logging;

namespace Forum.API
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(_configuration.GetConnectionString("ForumConnection"));
            });

            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
                .AddIdentityServerAuthentication(options =>
                {
                    options.Authority = "https://localhost:44394";
                    options.RequireHttpsMetadata = true;
                    options.SaveToken = true;
                    options.ApiName = "ForumAPI";
                    options.ApiSecret = "secret";
                });

            services.AddControllers()
                    .AddNewtonsoftJson(x => {
                        x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                    });

            var autoMapperConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<CreateTopicViewModel, Topic>();
                config.CreateMap<CreateGategoryViewModel, Category>();
                config.CreateMap<CreateSectionViewModel, Section>();
                config.CreateMap<CreateReplyViewModel, Reply>();
            });

            IMapper mapper = autoMapperConfig.CreateMapper();

            services.AddScoped<IUsersRepository, UsersRepository>();
            services.AddScoped<ITopicsRepository, TopicsRepository>();
            services.AddScoped<ICategoriesRepository, CategoriesRepository>();
            services.AddScoped<ISectionsRepository, SectionsRepository>();
            services.AddScoped<IRepliesRepository, RepliesRepository>();
            services.AddSingleton(mapper);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                IdentityModelEventSource.ShowPII = true;
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => endpoints.MapDefaultControllerRoute());
        }
    }
}
