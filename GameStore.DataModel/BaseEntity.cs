using System;

namespace GameStore.DataModel
{
    public class BaseEntity
    {
        /// <summary>
        /// When the entity was created.
        /// </summary>
        public DateTime Created { get; set; }

        /// <summary>
        /// When the entity was modified.
        /// </summary>
        public DateTime Modified { get; set; }
    }
}