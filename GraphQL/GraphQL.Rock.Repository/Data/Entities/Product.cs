using System;
using System.Collections.Generic;

namespace GraphQL.Rock.Repository.Data.Entities
{
    public partial class Product
    {
        public Product()
        {
            ProductReviews = new HashSet<ProductReview>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int Type { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public int Rating { get; set; }
        public DateTimeOffset IntroducedAt { get; set; }
        public string PhotoFileName { get; set; }

        public virtual ICollection<ProductReview> ProductReviews { get; set; }
    }
}
