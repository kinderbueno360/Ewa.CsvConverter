using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ewa.CsvConverter.Test.Model
{
    public class FooWithOrder
    {
        [Name("Id"), Index(0)]
        public string Id { get; set; }

        [Name("Name")]
        public string Name { get; set; }

        [Name("Surname")]
        public string Surname { get; set; }

        [Name("Number")]
        public int Number { get; set; }

        [Name("Order Id")]
        public string OrderId { get; set; }

        [Ignore]
        public Order Order { get; set; }

        public FooWithOrder UpdateOrder(Order order)
        {
            this.Order = order;
            return this;
        }

    }

   
}
