using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Ewa.CsvConverter.Test.Model
{
    public class FooWithExternalServiceMap : CsvConvertMap<Foo, FooOrderDest>
    {
        public FooWithExternalServiceMap(TextReader source, IOrderService orderService, bool hasHeaders = false) : base(source, hasHeaders)
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
                    .Value(sourceCsv => GetOrderAmount(sourceCsv, orderService));

            Map(destination => destination.OrderDate) 
                    .Value(sourceCsv => orderService.GetOrderById(sourceCsv.OrderId).Date);

            Map(destination => destination.OrderDate) // Sample using extension method
                    .Value(sourceCsv => orderService
                                                .GetOrderById(sourceCsv.OrderId)
                                                .ToDestinationFormatStatus());

        }
    }

    public static class FooWithExternalServiceRules
    {
        public static string ToDestinationFormatStatus(this Order order) =>
            order.Status switch
            {
                "pd" => "paid",
                "wp" => "waiting for paiment",
                "can" => "canceled",
                _ => throw new NotImplementedException(),
            };
    }
}
