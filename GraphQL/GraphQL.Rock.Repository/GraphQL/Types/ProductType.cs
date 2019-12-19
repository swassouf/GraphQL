using GraphQL.DataLoader;
using GraphQL.Rock.Repository.Data.Entities;
using GraphQL.Rock.Repository.Repositories;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace GraphQL.Rock.Repository.GraphQL.Types
{
    public class ProductType : ObjectGraphType<Product>
    {
        public ProductType(ProductReviewRepository reviewRepository, IDataLoaderContextAccessor dataLoaderAccessor)
        {
            Field(t => t.Id);
            Field(t => t.Name);
            Field(t => t.Description);
            Field(t => t.IntroducedAt).Description("When the product was first introduced in the catalog");
            Field(t => t.PhotoFileName).Description("The file name of the photo so the client can render it");
            Field(t => t.Price);
            Field(t => t.Rating).Description("The (max 5) star customer rating");
            Field(t => t.Stock);
            Field<ProductTypeEnumType>("Type", "The type of product");

            Field<ListGraphType<ProductReviewType>>(
                "reviews",
                resolve: context =>
                {
                    var user = (ClaimsPrincipal)context.UserContext;
                    var loader =
                        dataLoaderAccessor.Context.GetOrAddCollectionBatchLoader<int, ProductReview>(
                            "GetReviewsByProductId", reviewRepository.GetForProducts);
                    return loader.LoadAsync(context.Source.Id);
                });
        }
    }
}
