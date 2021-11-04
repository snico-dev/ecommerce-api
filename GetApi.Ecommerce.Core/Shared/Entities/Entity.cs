using System;

namespace GetApi.Ecommerce.Core.Shared.Entities
{
    public abstract class Entity
    {
        public Guid Id { get; set; }
        
        public Entity()
        {
            Id = Guid.NewGuid();
        }
    }
}
