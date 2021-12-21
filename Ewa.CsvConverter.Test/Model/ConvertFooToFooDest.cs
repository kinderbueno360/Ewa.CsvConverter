using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Ewa.CsvConverter.Test.Model
{
    public class ConvertFooToFooDest : CsvConverter<Foo, FooDest>
    {
        public ConvertFooToFooDest(TextReader source)
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

    public static class ConverRulesSample
    {
        public static string GetCompleteName(this Foo foo) => $"{foo.Name} {foo.Surname}";

    }

    public class Id<T> { 
        public T Value { get; set; } 
        public Id(T value) => Value = value; 
        public Id<T1> Map<T1>(Func<T, T1> f) => new Id<T1>(f(Value)); 
    }
}
