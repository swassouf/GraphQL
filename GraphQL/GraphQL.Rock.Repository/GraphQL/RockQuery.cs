using GraphQL.Rock.Repository.GraphQL.Types;
using GraphQL.Rock.Repository.Repositories;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphQL.Rock.Repository.GraphQL
{
    public class RockQuery : ObjectGraphType
    {
        public RockQuery(ProductRepository productRepository, ProductReviewRepository reviewRepository)
        {
            Field<ListGraphType<ProductType>>(
                           "products",
                           resolve: context => productRepository.GetAll()
                       );

            Field<ProductType>(
                "product",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id" }),
                resolve: context =>
                {
                    var id = context.GetArgument<int>("id");
                    return productRepository.GetOne(id);
                }
            );

            Field<ListGraphType<ProductReviewType>>(
                "reviews",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "productId" }),
                resolve: context =>
                {
                    var id = context.GetArgument<int>("productId");
                    return reviewRepository.GetForProduct(id);
                });
        }
    }
}
