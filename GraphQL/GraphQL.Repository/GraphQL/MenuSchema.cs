using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphQL.Repository.GraphQL
{
    public class MenuSchema : Schema
    {
        public MenuSchema(IDependencyResolver resolver) : base(resolver)
        {
            Query = resolver.Resolve<MenuQuery>();

        }
    }
}
