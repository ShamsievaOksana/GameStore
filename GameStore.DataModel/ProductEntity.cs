using System;

namespace GameStore.DataModel
{
    public class ProductEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public bool Published { get; set; }

        public DateTime Created { get; set; }

        public DateTime Modified { get; set; }
    }
}