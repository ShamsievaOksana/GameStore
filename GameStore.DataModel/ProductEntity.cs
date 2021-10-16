namespace GameStore.DataModel
{
    public class ProductEntity
        : BaseEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public bool Published { get; set; }
    }
}