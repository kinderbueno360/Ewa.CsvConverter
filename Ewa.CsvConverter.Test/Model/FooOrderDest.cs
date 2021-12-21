using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ewa.CsvConverter.Test.Model
{
    public class FooOrderDest
    {
        [Name("Cod"), Index(0)]
        public string Id { get; set; }
        [Name("Complete Name"), Index(1)]
        public string CompleteName { get; set; }

        [Name("Order Id")]
        public string OrderId { get; set; }

        [Name("Order Date")]
        public string OrderDate { get; set; }

        [Name("Amount")]
        public string Amount { get; set; }

        [Name("Order Status")]
        public string Status { get; set; }
    }
}
