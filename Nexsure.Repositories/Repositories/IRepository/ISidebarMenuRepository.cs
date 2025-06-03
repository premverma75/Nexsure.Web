using Nexsure.Entities.Domain_Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nexsure.DataBridge.Repositories.IRepository
{
    public interface ISidebarMenuRepository
    {
        List<SidebarMenuItem> GetSidebarMenu();

        SidebarMenuItem GetSidebarMenuItemById(int id);

        void AddSidebarMenuItem(SidebarMenuItem menuItem);

        void UpdateSidebarMenuItem(SidebarMenuItem menuItem);

        void DeleteSidebarMenuItem(int id);

        List<SidebarMenuItem> GetSidebarMenuByParentId(int? parentId);

        IEnumerable<SidebarMenuItem> GetAllSidebarMenuItems();
    }
}