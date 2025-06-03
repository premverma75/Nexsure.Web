using Nexsure.Entities.Domain_Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nexsure.Entities.Business_Model.Request_Model.SidebarMenu
{
    public class SidebarMenuItemDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Action { get; set; }
        public string Controller { get; set; }
        public int? ParentId { get; set; }
        public int OrderIndex { get; set; }
        public bool IsVisible { get; set; }
        public List<SidebarMenuItemDto> Children { get; set; } = new List<SidebarMenuItemDto>();
    }
}