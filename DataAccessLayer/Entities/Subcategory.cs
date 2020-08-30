namespace DataAccessLayer.Entities
{
    public class Subcategory : Category
    {
        public int ParentId { get; set; }
        public virtual MainCategory Parent { get; set; }
    }
}
