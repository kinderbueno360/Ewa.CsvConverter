using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Ewa.CsvConverter.Test.Model
{
    public class FooMap : CsvConvertMap<Foo, FooDest>
    {
        public FooMap(TextReader source)
            : base(source)
        {
            Func<int,string> square = x => (x * x).ToString(); 

            Map(destination => destination.CompleteName)
                    .Value(sourceCsv => sourceCsv.GetCompleteName());
            Map(destination => destination.Id)
                    .Value(sourceCsv => sourceCsv.Id);
            Map(destination => destination.Square)
                    .Value(sourceCsv => square(sourceCsv.Number));
        }
    }

    /// <summary>
    /// Sample using Extension 
    /// </summary>
    public static class ConverRulesSample
    {
        public static string GetCompleteName(this Foo foo) => $"{foo.Name} {foo.Surname}";

    }
}