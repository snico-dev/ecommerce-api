using GetApi.Ecommerce.Core.OrderForm.Dtos;
using GetApi.Ecommerce.Core.OrderFrom.Dtos;
using GetApi.Ecommerce.Core.Shared.Entities;
using System.Collections.Generic;
using System.Linq;

namespace GetApi.Ecommerce.Core.OrderFrom.Entities
{
    public class OrderForm: Entity
    {
        public ICollection<OrderFormItemDto> Items { get; set; }
        public ICollection<TotalDto> Totalizers { get; set; }
        public decimal Total { get; set; }

        private OrderForm() {
            Items = new List<OrderFormItemDto>();
        }

        public static OrderForm Create()
        {
            return new OrderForm();
        }

        public void AddItem(OrderFormItemDto item)
        {
            var foundItem = Items.FirstOrDefault();

            if (foundItem is null)
            {
                foundItem.Quantity += 1;
                return;
            }

            Items.Add(item);
        }

        public void GetTotal()
        {
        }
    }
}
