using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphQL.Rock.Repository.GraphQL
{
   public class RockSchema: Schema
    {
        public RockSchema(IDependencyResolver resolver) : base(resolver)
        {
            Query = resolver.Resolve<RockQuery>();
        }
    }
}
