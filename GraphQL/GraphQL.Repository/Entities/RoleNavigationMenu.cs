using System;
using System.Collections.Generic;

namespace GraphQL.Repository.Entities
{
    public partial class RoleNavigationMenu
    {
        public int Id { get; set; }
        public int NavigationMenuId { get; set; }
        public int RoleId { get; set; }
        public bool IsActive { get; set; }
        public DateTime? Created { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? Modified { get; set; }
        public string ModifiedBy { get; set; }

        public virtual NavigationMenu NavigationMenu { get; set; }
        public virtual Role Role { get; set; }
    }
}
