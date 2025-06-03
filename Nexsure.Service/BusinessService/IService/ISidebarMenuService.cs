using Nexsure.Entities.Business_Model.Request_Model.SidebarMenu;
using Nexsure.Entities.Domain_Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nexsure.Service.BusinessService.IService
{
    public interface ISidebarMenuService
    {
        List<SidebarMenuItemDto> GetSidebarMenu();

        SidebarMenuItemDto GetSidebarMenuItemById(int id);

        void AddSidebarMenuItem(SidebarMenuItemDto menuItem);

        void UpdateSidebarMenuItem(SidebarMenuItemDto menuItem);

        void DeleteSidebarMenuItem(int id);

        List<SidebarMenuItemDto> GetSidebarMenuByParentId(int? parentId);

        IEnumerable<SidebarMenuItemDto> GetAllSidebarMenuItems();
    }
}