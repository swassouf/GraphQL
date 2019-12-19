using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using GraphQL;
using GraphQL.Server;
using GraphQL.Server.Ui.Playground;
using Microsoft.EntityFrameworkCore;
using GraphQL.Repository.Entities;
using GraphQL.Repository.Repositories;
using GraphQL.Repository.GraphQL;
using GraphQL.Repository.GraphQL.Types;
using Microsoft.AspNetCore.Http;
using GraphQL.Authorization;
using GraphQL.Validation;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace GraphQL.Api
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


            services.AddDbContext<MenuManagementDevContext>(options =>
              options.UseSqlServer(Configuration["ConnectionStrings:CarvedRockContext"]));

            services.AddScoped<MenuRepository>();
            services.AddScoped<MenuQuery>();

            services.AddScoped<NavigationMenuType>();
            services.AddScoped<RoleNavigationMenuType>();
            services.AddScoped<RoleType>();

            services.AddScoped<IDependencyResolver>(s => new FuncDependencyResolver(s.GetRequiredService));
            services.AddScoped<MenuSchema>();

            services.AddGraphQL(o => { o.ExposeExceptions = true; })
                .AddGraphQLAuthorization(options =>
                {
                    options.AddPolicy("AUTHORIZED", p => p.RequireAuthenticatedUser());
                })
                .AddGraphTypes(ServiceLifetime.Scoped).AddUserContextBuilder(httpContext => httpContext.User)
                .AddDataLoader()
                .AddWebSockets();
       
            services.AddCors();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors(builder =>
                builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

            app.UseWebSockets();
            app.UseGraphQLWebSockets<MenuSchema>("/graphql");
            app.UseGraphQL<MenuSchema>();
            app.UseGraphQLPlayground(new GraphQLPlaygroundOptions());


        }
    }
}
