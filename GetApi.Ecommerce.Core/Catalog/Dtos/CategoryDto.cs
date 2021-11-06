﻿using System;

namespace GetApi.Ecommerce.Core.Catalog.Dtos
{
    public class CategoryDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string FriendlyUrl { get; set; }
        public Guid? ParentId { get; set; }
    }
}
