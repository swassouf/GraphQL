using System;
using System.Collections.Generic;

namespace GraphQL.Repository.Entities
{
    public partial class Application
    {
        public Application()
        {
            NavigationMenu = new HashSet<NavigationMenu>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string DomainUrl { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }

        public virtual ICollection<NavigationMenu> NavigationMenu { get; set; }
    }
}
