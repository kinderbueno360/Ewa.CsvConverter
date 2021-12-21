using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ewa.CsvConverter.Test.Model
{
    public class FooDest
    {
        [Name("Cod"), Index(0)]
        public string Id { get; set; }
        [Name("Complete Name"), Index(1)]
        public string CompleteName { get; set; }

        [Name("Square"), Index(1)]
        public string Square { get; set; }
    }
}
