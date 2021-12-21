using Ewa.CsvConverter.Test.Model;
using Ewa.CsvConverter.Test.Stub.OrderService;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Ewa.CsvConverter.Test
{
    public class CsvConvertExternalServiceTest_Spec
    {

        [Fact]
        public async Task CsvConvertExternalServiceTest_Convert_with_func_to_query_data_to_destination_file_should_result_expected_data()
        {
            // Arrange
            var sourceString = "Id,Name,Surname,Number,Order Id\r\n" +
                                "2,Carlos,Bueno,2,1\r\n" +
                                "1,Andre,Ferreira,2,3\r\n" +
                                "5,Bruno,Carvalho,2,4\r\n";

            var expected = "Cod,Complete Name,Order Id,Order Date,Amount,Order Status\r\n" + 
                            "1,Andre Ferreira,3,paid,$ 90.00,\r\n" + 
                            "5,Bruno Carvalho,4,canceled,$ 109.00,\r\n" + 
                            "2,Carlos Bueno,1,paid,$ 910.00,\r\n";

            using var source = new StringReader(sourceString);
            using var destination = new StringWriter();
            var converter = new FooWithExternalServiceMap(source, new FakeOrderService());
            Func<IEnumerable<Foo>, IEnumerable<Foo>> rulesToApply = x => x.OrderBy(y => y.Name);

            // Act
            await converter
                    .Convert(rulesToApply)
                    .Save(destination);

            // Assert
            Assert.Contains(expected, destination.ToString());
        }

        [Fact]
        public async Task CsvConvertExternalServiceTest_Convert_with_func_to_query_data_to_destination_file_should_result_expected_data_2()
        {
            // Arrange
            var sourceString = "Id,Name,Surname,Number,Order Id\r\n" +
                                "2,Carlos,Bueno,2,1\r\n" +
                                "1,Andre,Ferreira,2,3\r\n" +
                                "5,Bruno,Carvalho,2,4\r\n";

            var expected = "Cod,Complete Name,Order Id,Order Date,Amount,Order Status\r\n" +
                                "1,Andre Ferreira,3,paid,$ 90.00,\r\n" +
                                "2,Carlos Bueno,1,paid,$ 910.00,\r\n" + 
                                "5,Bruno Carvalho,4,canceled,$ 109.00,\r\n";

            using var source = new StringReader(sourceString);
            using var destination = new StringWriter();
            var converter = new FooWithExternalSerivceWithoutInjectServiceMap(source);

            var service = new FakeOrderService();

            IEnumerable<Order> getOrders(IEnumerable<FooWithOrder> x) => 
                        service.GetOrdersByIds(x.Select(x => x.OrderId));

            IEnumerable<FooWithOrder> ApplyOrder(IEnumerable<FooWithOrder> foos, IEnumerable<Order> orders) => 
                        foos.Select(x => x.UpdateOrder(GetOrder(orders, x.OrderId)));

            Order GetOrder(IEnumerable<Order> orders, string Id) =>
                    orders.Where(y => y.Id == Id).SingleOrDefault();

            IEnumerable<FooWithOrder> rulesToApply(IEnumerable<FooWithOrder> x) => 
                        ApplyOrder(x, getOrders(x)).OrderBy(x => x.Id);

            // Act
            await converter
                    .Convert(rulesToApply)
                    .Save(destination);

            // Assert
            Assert.Contains(expected, destination.ToString());
        }
    }
}
