# Ewa.CsvConverter

This is a Framework designed to convert CSV files applying business rules.


## Documentation (Wiki)

[Go to Documentation](https://github.com/kinderbueno360/Ewa.CsvConverter/wiki)


## Usage


*Sample* 

```csharp
    
    public class Program 
    {
    
        var sourceString = "Id,Name,Surname,Number,Order Id\r\n" +
                                    "1,Carlos,Bueno,2,1\r\n" +
                                    "1,Andre,Ferreira,2,3\r\n" +
                                    "1,Bruno,Carvalho,2,4\r\n";

        using var source = new StringReader(sourceString);
        using var destination = new StringWriter();
        var converter = new FooMap(source);
        Func<IEnumerable<Foo>, IEnumerable<Foo>> rulesToApply = x => x.OrderBy(y=>y.Name);
        // Act
        await converter
                .Convert(rulesToApply)
                .Save(destination);
    }
    
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

        [Name("Order Id")]
        public string OrderId { get; set; }
    }
    
    public class FooDest
    {
        [Name("Cod"), Index(0)]
        public string Id { get; set; }
        [Name("Complete Name"), Index(1)]
        public string CompleteName { get; set; }

        [Name("Square"), Index(2)]
        public string Square { get; set; }
    }
```


## License

MIT
