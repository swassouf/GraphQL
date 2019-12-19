using GraphQL.Authorization;
using GraphQL.Repository.GraphQL.Types;
using GraphQL.Repository.Repositories;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace GraphQL.Repository.GraphQL
{
    [GraphQLAuthorize(Policy = "AUTHORIZED")]
    public class MenuQuery : ObjectGraphType
    {
        public MenuQuery(MenuRepository menuRepository)
        {
            Field<ListGraphType<NavigationMenuType>>(
                "NavigationMenu",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "applicationId" }),
                resolve: context =>
                {
                    var user = (ClaimsPrincipal)context.UserContext;
                   
                    var applicationId = context.GetArgument<int>("applicationId");
                    return menuRepository.GetNavigationMenus(applicationId);
                });
        }
    }
}
