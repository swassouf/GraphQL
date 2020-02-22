using GraphQL.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GraphQL.Authorization;

namespace GraphQL.Repository.Repositories
{

    public class MenuRepository
    {
        public async Task<ILookup<int, RoleNavigationMenu>> GetRoleNavigationMenus(IEnumerable<int> navigationIds)
        {
            using (var ctx = new MenuManagementDevContext())
            {
              var result=  await ctx.RoleNavigationMenu.Where(m => navigationIds.Contains(m.NavigationMenuId)).ToListAsync();
                return result.ToLookup(r => r.NavigationMenuId);
            }
        }


        public async Task<List<NavigationMenu>> GetNavigationMenus(int applicationId)
        {
            using (var ctx = new MenuManagementDevContext())
            {
                var result = await ctx.NavigationMenu.Where(m => m.ApplicationId == applicationId).ToListAsync();
                return result;
            }
        }

        public async Task<ILookup<int, NavigationMenu>> GetNavigationMenusChildren(IEnumerable<int> ids)
        {
            using (var ctx = new MenuManagementDevContext())
            {
                var result = await ctx.NavigationMenu.Where(m => ids.Contains(m.Id)).ToListAsync();
                return result.ToLookup(n => n.ParentId.GetValueOrDefault());
            }
        }

    }
}
