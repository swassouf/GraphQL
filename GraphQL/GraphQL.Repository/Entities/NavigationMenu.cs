using System;
using System.Collections.Generic;

namespace GraphQL.Repository.Entities
{
    public partial class NavigationMenu
    {
        public NavigationMenu()
        {
            InverseParent = new HashSet<NavigationMenu>();
            RoleNavigationMenu = new HashSet<RoleNavigationMenu>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int ApplicationId { get; set; }
        public string ActionRoute { get; set; }
        public string ImageUrl { get; set; }
        public int? ParentId { get; set; }
        public int Sort { get; set; }
        public bool IsActive { get; set; }
        public DateTime? Created { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? Modified { get; set; }
        public string ModifiedBy { get; set; }

        public virtual Application Application { get; set; }
        public virtual NavigationMenu Parent { get; set; }
        public virtual ICollection<NavigationMenu> InverseParent { get; set; }
        public virtual ICollection<RoleNavigationMenu> RoleNavigationMenu { get; set; }
    }
}
