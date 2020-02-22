using GraphQL.Authorization;
using GraphQL.DataLoader;
using GraphQL.Repository.Entities;
using GraphQL.Repository.Repositories;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphQL.Repository.GraphQL.Types
{

    [GraphQLAuthorize(Policy = "AdminPolicy")]
    public  class NavigationMenuType : ObjectGraphType<NavigationMenu>
    {
      public NavigationMenuType(MenuRepository menuRepository, IDataLoaderContextAccessor dataLoaderAccessor)
        {
            Field(t => t.Id);
            Field(t => t.Name);
            Field(t => t.Description);
            Field(t => t.ApplicationId);
            Field(t => t.ActionRoute, nullable: true);
            Field(t => t.ImageUrl);
            Field(t => t.ParentId, nullable: true);
            Field(t => t.Sort);
            Field(t => t.IsActive);
            Field(t => t.Created, nullable: true);
            Field(t => t.CreatedBy);
            Field(t => t.Modified, nullable: true);
            Field(t => t.ModifiedBy);
            Field<ListGraphType<NavigationMenuType>>(
                "Children",
                resolve: context =>
                {
                    var loader = dataLoaderAccessor.Context.GetOrAddCollectionBatchLoader<int, NavigationMenu>(
                        "GetNavigationMenuChildren", menuRepository.GetNavigationMenusChildren);
                    return loader.LoadAsync(context.Source.Id);
                });
            Field<ListGraphType<RoleNavigationMenuType>>("RoleNavigationMenus",
                resolve: context =>
                {
                    var loader = dataLoaderAccessor.Context.GetOrAddCollectionBatchLoader<int, RoleNavigationMenu>(
                        "GetRoleNavigationMenus", menuRepository.GetRoleNavigationMenus);
                    return loader.LoadAsync(context.Source.Id);
                });
        }
    }
}

 
