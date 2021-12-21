using System;
using System.Collections.Generic;
using System.Text;

namespace Ewa.CsvConverter.Test
{
    public interface IOrderService
    {
        public Order GetOrderById(string id);
    }
}
