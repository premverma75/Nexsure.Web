using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nexsure.Entities.Business_Model.Request_Model.SidebarMenu;
using Nexsure.Service.BusinessService.IService;

namespace Nexsure.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly ISidebarMenuService _sidebarMenuService;

        public MenuController(ISidebarMenuService sidebarMenuService)
        {
            _sidebarMenuService = sidebarMenuService;
        }

        [HttpGet("all")]
        public IActionResult GetAllSidebarMenuItems()
        {
            var items = _sidebarMenuService.GetAllSidebarMenuItems();
            return Ok(items);
        }

        [HttpGet]
        public IActionResult GetSidebarMenu()
        {
            var menu = _sidebarMenuService.GetSidebarMenu();
            if (menu == null)
            {
                return NotFound();
            }
            return Ok(menu);
        }

        [HttpGet("{id}")]
        public IActionResult GetSidebarMenuItemById(int id)
        {
            var item = _sidebarMenuService.GetSidebarMenuItemById(id);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);
        }

        [HttpGet("parent/{parentId}")]
        public IActionResult GetSidebarMenuByParentId(int? parentId)
        {
            var items = _sidebarMenuService.GetSidebarMenuByParentId(parentId);
            return Ok(items);
        }

        [HttpPost]
        public IActionResult AddSidebarMenuItem([FromBody] SidebarMenuItemDto menuItem)
        {
            _sidebarMenuService.AddSidebarMenuItem(menuItem);
            return CreatedAtAction(nameof(GetSidebarMenuItemById), new { id = menuItem.Id }, menuItem);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateSidebarMenuItem(int id, [FromBody] SidebarMenuItemDto menuItem)
        {
            if (id != menuItem.Id)
            {
                return BadRequest();
            }
            _sidebarMenuService.UpdateSidebarMenuItem(menuItem);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteSidebarMenuItem(int id)
        {
            _sidebarMenuService.DeleteSidebarMenuItem(id);
            return NoContent();
        }
    }
}