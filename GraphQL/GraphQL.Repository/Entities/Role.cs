using System;
using System.Collections.Generic;

namespace GraphQL.Repository.Entities
{
    public partial class Role
    {
        public Role()
        {
            RoleNavigationMenu = new HashSet<RoleNavigationMenu>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? Created { get; set; }
        public string CreatedBy { get; set; }

        public virtual ICollection<RoleNavigationMenu> RoleNavigationMenu { get; set; }
    }
}
