using Nexsure.DataBridge.DataContext;
using Nexsure.DataBridge.Repositories.IRepository;
using Nexsure.Entities.Domain_Models.Model;

namespace Nexsure.DataBridge.Repositories.Repository
{
    public class SidebarMenuRepositoty : ISidebarMenuRepository
    {
        private readonly NexsureAppDbContext _dbContext;

        public SidebarMenuRepositoty(NexsureAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddSidebarMenuItem(SidebarMenuItem menuItem)
        {
            if (menuItem == null)
                throw new ArgumentNullException(nameof(menuItem));

            _dbContext.SidebarMenuItems.Add(menuItem);
            _dbContext.SaveChanges();
        }

        public void DeleteSidebarMenuItem(int id)
        {
            var menuItem = _dbContext.SidebarMenuItems.Find(id);
            if (menuItem == null)
                throw new KeyNotFoundException($"SidebarMenuItem with Id {id} not found.");

            _dbContext.SidebarMenuItems.Remove(menuItem);
            _dbContext.SaveChanges();
        }

        public IEnumerable<SidebarMenuItem> GetAllSidebarMenuItems()
        {
            return _dbContext.SidebarMenuItems.ToList();
        }

        public List<SidebarMenuItem> GetSidebarMenu()
        {
            var items = _dbContext.SidebarMenus
                .Where(x => x.IsVisible)
                .OrderBy(x => x.OrderIndex)
                .ToList();

            // Map SidebarMenu to SidebarMenuItem
            var menuItems = items.Select(x => new SidebarMenuItem
            {
                Id = x.Id,
                Title = x.Title,
                Action = x.Action,
                Controller = x.Controller,
                ParentId = x.ParentId,
                OrderIndex = x.OrderIndex,
                IsVisible = x.IsVisible,
                Children = new List<SidebarMenuItem>()
            }).ToList();

            return BuildMenuTree(menuItems);
        }

        private List<SidebarMenuItem> BuildMenuTree(List<SidebarMenuItem> items)
        {
            var lookup = items.ToLookup(x => x.ParentId);
            foreach (var item in items)
            {
                item.Children = lookup[item.Id].ToList();
            }
            return lookup[null].ToList(); // Root items
        }

        public List<SidebarMenuItem> GetSidebarMenuByParentId(int? parentId)
        {
            return _dbContext.SidebarMenuItems
                .Where(item => item.ParentId == parentId && item.IsVisible)
                .OrderBy(item => item.OrderIndex)
                .ToList();
        }

        public SidebarMenuItem GetSidebarMenuItemById(int id)
        {
            return _dbContext.SidebarMenuItems.FirstOrDefault(item => item.Id == id);
        }

        public void UpdateSidebarMenuItem(SidebarMenuItem menuItem)
        {
            if (menuItem == null)
                throw new ArgumentNullException(nameof(menuItem));

            var existingItem = _dbContext.SidebarMenuItems.Find(menuItem.Id);
            if (existingItem == null)
                throw new KeyNotFoundException($"SidebarMenuItem with Id {menuItem.Id} not found.");

            existingItem.Title = menuItem.Title;
            existingItem.Action = menuItem.Action;
            existingItem.Controller = menuItem.Controller;
            existingItem.ParentId = menuItem.ParentId;
            existingItem.OrderIndex = menuItem.OrderIndex;
            existingItem.IsVisible = menuItem.IsVisible;
            // Children property is not updated here as it is usually managed separately

            _dbContext.SaveChanges();
        }
    }
}