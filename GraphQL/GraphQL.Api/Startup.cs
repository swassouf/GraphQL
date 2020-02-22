using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
//using GraphQL;
using GraphQL.Server;
using Microsoft.EntityFrameworkCore;
using GraphQL.Repository.Entities;
using GraphQL.Repository.Repositories;
using GraphQL.Repository.GraphQL;
using GraphQL.Repository.GraphQL.Types;
using Microsoft.Extensions.DependencyInjection.Extensions;
using GraphQL.Server.Ui.GraphiQL;
using GraphQL.Server.Ui.Playground;
using GraphQL.Repository.Authorization;
using GraphQL.Validation;
using GraphQL.Types;
using GraphQL.Utilities;

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

            services.AddScoped<ISchema, MenuSchema>();

           // services.TryAddSingleton<MenuSchema>(s=>s.GetService<MenuSchema>());

            //services.TryAddSingleton<ISchema>(s =>
            //{
            //    var definitions = @"
            //      type User {
            //        id: ID
            //        name: String
            //      }

            //      type Query {
            //        viewer: User
            //        users: [User]
            //      }
            //    ";
            //    var schema = Schema.For(
            //        definitions,
            //        _ =>
            //        {
            //            _.Types.Include<GraphQL.Repository.Query>();
            //        });
            //    schema.FindType("User");//.AuthorizeWith("AdminPolicy");
            //    return schema;
            //});
            services.AddGraphQLAuth((_, s) =>
            {
                _.AddPolicy("AdminPolicy", p => p.RequireClaim("role", "Admin"));
            });

            services.AddGraphQL(o => { o.ExposeExceptions = true; })
                //.AddGraphQLAuthorization(options =>
                //{
                //    options.AddPolicy("AUTHORIZED", p => p.RequireAuthenticatedUser());
                //})
                .AddUserContextBuilder(context => new GraphQLUserContext { User = context.User })
                .AddGraphTypes(ServiceLifetime.Scoped)
                .AddDataLoader();
               // .AddWebSockets();
       
            services.AddCors();
 
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            var validationRules = app.ApplicationServices.GetServices<IValidationRule>();

            app.UseCors(builder =>
                builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

            //app.UseGraphQL<MenuSchema,Server.Transports.AspNetCore.GraphQLHttpMiddleware<MenuSchema>>("/graphql");

            //  app.UseWebSockets();
            //  app.UseGraphQLWebSockets<MenuSchema>("/graphql");
            app.UseGraphQL<ISchema>("/graphql");
            
            app.UseGraphiQLServer(new GraphiQLOptions());

            app.UseGraphQLPlayground(new GraphQLPlaygroundOptions());

            

        }
    }
}
