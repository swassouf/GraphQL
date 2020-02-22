using GraphQL.Authorization;
using GraphQL.Types;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphQL.Repository.GraphQL
{
    public class MenuSchema : Schema
    {
        public MenuSchema(IServiceProvider provider) : base(provider)
        {
            Query = provider.GetRequiredService<MenuQuery>();

        }
    }
}
