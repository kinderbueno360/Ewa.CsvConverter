using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Ewa.CsvConverter.Test.Model
{
    public class FooWithExternalSerivceWithoutInjectServiceMap : CsvConvertMap<FooWithOrder, FooOrderDest>
    {
        public FooWithExternalSerivceWithoutInjectServiceMap(TextReader source, bool hasHeaders = false) : base(source, hasHeaders)
        {
            //Sample using HoF
            Func<Foo, IOrderService , string> GetOrderAmount = (foo, orderService) => orderService.GetOrderById(foo.OrderId).Amount;

            Map(destination => destination.CompleteName)
                    .Value(sourceCsv => sourceCsv.GetCompleteName());
            Map(destination => destination.Id)
                    .Value(sourceCsv => sourceCsv.Id);
            Map(destination => destination.OrderId)
                    .Value(sourceCsv => sourceCsv.OrderId.ToString());

            Map(destination => destination.Amount) // sample using HOP
                    .Value(sourceCsv => sourceCsv.Order.Amount);

            Map(destination => destination.OrderDate) 
                    .Value(sourceCsv => sourceCsv.Order.Date);

            Map(destination => destination.OrderDate) // Sample using extension method
                    .Value(sourceCsv => sourceCsv.Order.ToDestinationFormatStatus());

        }
    }

    /// <summary>
    /// Sample using Extension 
    /// </summary>
    public static class ConverterRulesSample
    {
        public static string GetCompleteName(this FooWithOrder foo) => $"{foo.Name} {foo.Surname}";

    }


}
