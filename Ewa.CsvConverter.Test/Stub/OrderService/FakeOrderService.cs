using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ewa.CsvConverter.Test.Stub.OrderService
{
    public class FakeOrderService : IOrderService
    {
        public readonly IEnumerable<Order> orders;
        public FakeOrderService()
        {
            orders = new List<Order>
            {
                new Order{Amount = "$ 910.00", Date = "05/05/2021" , Id = "1" , Status ="pd" },
                new Order{Amount = "$ 1,110.00", Date = "05/06/2021" , Id = "2" , Status ="wp" },
                new Order{Amount = "$ 90.00", Date = "05/07/2021" , Id = "3" , Status ="pd"},
                new Order{Amount = "$ 109.00", Date = "05/08/2021" , Id = "4" ,Status ="can" },
                new Order{Amount = "$ 110.00", Date = "11/05/2021" , Id = "5"  , Status ="wp"  }
            };
        }
        public Order GetOrderById(string id)
            => orders
                    .Where(x => x.Id.Equals(id))
                    .SingleOrDefault();

        public IEnumerable<Order> GetOrdersByIds(IEnumerable<string> ids)
        {
            var result = orders
                        .Where(x => ids.Contains(x.Id))
                        .ToList();

            return result;
        }

    }
}
