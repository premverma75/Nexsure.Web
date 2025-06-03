using Nexsure.DataBridge.Repositories.IRepository;
using Nexsure.Entities.Business_Model.Request_Model.SidebarMenu;
using Nexsure.Entities.Domain_Models.Model;
using Nexsure.Service.BusinessService.IService;
using Nexsure.Service.UtilityClasses;

namespace Nexsure.Service.BusinessService.Service
{
    public class SidebarMenuService : ISidebarMenuService
    {
        private readonly ISidebarMenuRepository _sidebarMenuRepository;

        public SidebarMenuService(ISidebarMenuRepository sidebarMenuRepository)
        {
            _sidebarMenuRepository = sidebarMenuRepository ?? throw new ArgumentNullException(nameof(sidebarMenuRepository));
        }

        public void AddSidebarMenuItem(SidebarMenuItemDto menuItem)
        {
            if (menuItem == null) throw new ArgumentNullException(nameof(menuItem));
            var entity = CommonMapper.Map<SidebarMenuItemDto, SidebarMenuItem>(menuItem);
            _sidebarMenuRepository.AddSidebarMenuItem(entity);
        }

        public void DeleteSidebarMenuItem(int id)
        {
            var menuItem = _sidebarMenuRepository.GetSidebarMenuItemById(id);
            if (menuItem == null) throw new KeyNotFoundException($"SidebarMenuItem with Id {id} not found.");
            _sidebarMenuRepository.DeleteSidebarMenuItem(menuItem.Id);
        }

        public IEnumerable<SidebarMenuItemDto> GetAllSidebarMenuItems()
        {
            var items = _sidebarMenuRepository.GetAllSidebarMenuItems().ToList();
            var dtos = items.Select(item => CommonMapper.Map<SidebarMenuItem, SidebarMenuItemDto>(item)).ToList();
            return dtos;
        }

        public List<SidebarMenuItemDto> GetSidebarMenu()
        {
            var allItems = _sidebarMenuRepository.GetAllSidebarMenuItems().Where(x => x.IsVisible).ToList();
            var menu = BuildMenuHierarchy(allItems, null);
            return menu.OrderBy(x => x.OrderIndex).ToList();
        }

        public List<SidebarMenuItemDto> GetSidebarMenuByParentId(int? parentId)
        {
            var allItems = _sidebarMenuRepository.GetAllSidebarMenuItems().Where(x => x.IsVisible).ToList();
            var menu = BuildMenuHierarchy(allItems, parentId);
            return menu.OrderBy(x => x.OrderIndex).ToList();
        }

        public SidebarMenuItemDto GetSidebarMenuItemById(int id)
        {
            var entity = _sidebarMenuRepository.GetSidebarMenuItemById(id);
            if (entity == null) throw new KeyNotFoundException($"SidebarMenuItem with Id {id} not found.");
            return CommonMapper.Map<SidebarMenuItem, SidebarMenuItemDto>(entity);
        }

        public void UpdateSidebarMenuItem(SidebarMenuItemDto menuItem)
        {
            if (menuItem == null) throw new ArgumentNullException(nameof(menuItem));
            var existing = _sidebarMenuRepository.GetSidebarMenuItemById(menuItem.Id);
            if (existing == null) throw new KeyNotFoundException($"SidebarMenuItem with Id {menuItem.Id} not found.");
            var entity = CommonMapper.Map<SidebarMenuItemDto, SidebarMenuItem>(menuItem);
            _sidebarMenuRepository.UpdateSidebarMenuItem(entity);
        }

        private List<SidebarMenuItemDto> BuildMenuHierarchy(List<SidebarMenuItem> allItems, int? parentId)
        {
            var items = allItems
                .Where(x => x.ParentId == parentId)
                .OrderBy(x => x.OrderIndex)
                .ToList();

            var mappedItems = new List<SidebarMenuItemDto>();

            foreach (var item in items)
            {
                var mappedItem = CommonMapper.Map<SidebarMenuItem, SidebarMenuItemDto>(item);
                mappedItem.Children = BuildMenuHierarchy(allItems, item.Id);
                mappedItems.Add(mappedItem);
            }

            return mappedItems;
        }
    }
}