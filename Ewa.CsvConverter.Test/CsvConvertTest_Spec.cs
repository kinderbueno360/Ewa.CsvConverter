using Ewa.CsvConverter.Test.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Ewa.CsvConverter.Test
{
    public class CsvConvertTest_Spec
    {
        [Fact]
        public async Task CsvConvertTest_create_should_get_correct_data_from_csv() {
            // Arrange
            var sourceString = "Id,Name,Surname,Number,Order Id\r\n" +
                           "1,Carlos,Bueno,2,3\r\n";

            var fooSource = new List<Foo> 
                    { new Foo { Id = "1", Name = "Carlos" } };

            using var source = new StringReader(sourceString);

            // Act
            var converter = new FooMap(source);

            // Assert
            Assert.Contains(fooSource.SingleOrDefault().Name, converter.data.SingleOrDefault().Name);
        }

        [Fact]
        public async Task CsvConvertTest_Convert_with_func_to_query_data_to_destination_file_should_result_expected_data()
        {
            // Arrange
            var sourceString = "Id,Name,Surname,Number,Order Id\r\n" +
                                "1,Carlos,Bueno,2,1\r\n" +
                                "1,Andre,Ferreira,2,3\r\n" +
                                "1,Bruno,Carvalho,2,4\r\n";

            var expected = "Cod,Complete Name,Square\r\n" +
                                "1,Andre Ferreira,4\r\n" + 
                                "1,Bruno Carvalho,4\r\n" + 
                                "1,Carlos Bueno,4\r\n";

            using var source = new StringReader(sourceString);
            using var destination = new StringWriter();
            var converter = new FooMap(source);
            Func<IEnumerable<Foo>, IEnumerable<Foo>> rulesToApply = x => x.OrderBy(y=>y.Name);
            // Act
            await converter
                    .Convert(rulesToApply)
                    .Save(destination);

            // Assert
            Assert.Contains(expected, destination.ToString());
        }

        [Fact]
        public async Task CsvConvertTest_Convert_with_quering_data_to_destination_file_should_result_expected_data()
        {
            // Arrange
            var sourceString = "Id,Name,Surname,Number,Order Id\r\n" +
                                "1,Rafael,Ferreira,2,2\r\n"+ 
                                "2,Carlos,Bueno,2,4\r\n";

            var expected = "Cod,Complete Name,Square\r\n" +
                            "2,Carlos Bueno,4\r\n";
;
            using var source = new StringReader(sourceString);
            using var destination = new StringWriter();
            var converter = new FooMap(source);

            // Act
            await converter
                    .Convert(x => x.Where(x=>x.Name.Equals("Carlos")))
                    .Save(destination);

            // Assert
            Assert.Equal(expected,destination.ToString());
        }
    }
}