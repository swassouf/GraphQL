using System;
using System.Collections.Generic;

namespace GraphQL.Rock.Repository.Data.Entities
{
    public partial class ProductReview
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string Title { get; set; }
        public string Review { get; set; }

        public virtual Product Product { get; set; }
    }
}
