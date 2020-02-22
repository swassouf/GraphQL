using GraphQL.Authorization;
using GraphQL.DataLoader;
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
    public class RoleType: ObjectGraphType<Entities.Role>
    {
        public RoleType(MenuRepository menRepository, IDataLoaderContextAccessor dataLoaderAccessor)
        {
            Field(t => t.Id);
            Field(t => t.Name);
            Field(t => t.Created);
            Field(t => t.CreatedBy);

            Field<ListGraphType<RoleNavigationMenuType>>("RoleNavigationMenus",
                resolve: context =>
                {
                    var loader = dataLoaderAccessor.Context.GetOrAddCollectionBatchLoader<int, Entities.RoleNavigationMenu>(
                        "GetRoleNavigationMenus", menRepository.GetRoleNavigationMenus);
                    return loader.LoadAsync(context.Source.Id);
                });
        }
    }
}
