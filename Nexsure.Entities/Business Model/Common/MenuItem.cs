namespace Nexsure.Entities.Business_Model.Common
{
    public class MenuItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public List<MenuItem> Children { get; set; } = new();
    }
}