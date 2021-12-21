using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ewa.CsvConverter.Test.Model
{
    public class Foo
    {
        [Name("Id"), Index(0)]
        public string Id { get; set; }

        [Name("Name")]
        public string Name { get; set; }

        [Name("Surname")]
        public string Surname { get; set; }

        [Name("Number")]
        public int Number { get; set; }
    }

   
}
