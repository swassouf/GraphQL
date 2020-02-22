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
    public  class RoleNavigationMenuType: ObjectGraphType<RoleNavigationMenu>
    {
        public RoleNavigationMenuType()
        {

            Field(t => t.Id);
            Field(t => t.NavigationMenuId);
            Field(t => t.RoleId);
            Field(t => t.IsActive);
            Field(t => t.Created, nullable: true);
            Field(t => t.CreatedBy);
            Field(t => t.Modified, nullable: true);
            Field(t => t.ModifiedBy);
        }
    }
}
