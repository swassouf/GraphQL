using GraphQL.Utilities;
//using Microsoft.AspNetCore.Authorization;
using System;

namespace GraphQL.Repository
{
    [AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public class CustomAuthorizeAttribute: GraphQLAttribute
    {
        public CustomAuthorizeAttribute(string Policy) :base()
        {

        }

        public override void Modify(FieldConfig field)
        {
            base.Modify(field);
        }

        public override void Modify(TypeConfig type)
        {
            base.Modify(type);
        }
    }
}
