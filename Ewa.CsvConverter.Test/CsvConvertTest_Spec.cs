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
        public async Task CsvConvertTest_create_should_have_correct_data_from_csv() {
            // Arrange
            var sourceString = "Id,Name,Surname,Number\r\n" +
                           "1,Carlos,Bueno,2\r\n";
            var fooSource = new List<Foo> 
                    { new Foo { Id = "1", Name = "Carlos" } };

            using var source = new StringReader(sourceString);

            // Act
            var converter = new FooMap(source);

            // Assert
            Assert.Contains(fooSource.SingleOrDefault().Name, converter.data.SingleOrDefault().Name);

        }

        [Fact]
        public async Task CsvConvertTest_create_should_have_correct_data_from_csv2()
        {
            // Arrange
            var sourceString = "Id,Name,Surname,Number\r\n" +
                           "1,Carlos,Bueno,2\r\n";
            var fooSource = new List<Foo>
                    { new Foo { Id = "1", Name = "Carlos" } };

            using var source = new StringReader(sourceString);
            using var destination = new StringWriter();

            // Act
            var converter = new FooMap(source);

            await converter
                    .Convert(x=>x.ToList())
                    .Save(destination);

            // Assert
            Assert.Contains(fooSource.SingleOrDefault().Name, converter.data.SingleOrDefault().Name);

        }


        [Fact]
        public async Task CsvConvertTest_create_should_have_correct_data_from_csv_final()
        {
            // Arrange
            var sourceString = "Id,Name,Surname,Number\r\n" +
                            "1,Carlos,Bueno,2\r\n";

            var expected = "Cod,Complete Name,Square\r\n" +
                            "1,Carlos Bueno,4\r\n";
;

            using var source = new StringReader(sourceString);
            using var destination = new StringWriter();
            var converter = new FooMap(source);


            // Act


            await converter
                    .Convert(x => x.ToList())
                    .Save(destination);

            // Assert
            Assert.Equal(expected,destination.ToString());

        }


    }
}
