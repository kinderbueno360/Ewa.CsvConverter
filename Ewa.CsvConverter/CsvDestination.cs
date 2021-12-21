using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Ewa.CsvConverter
{
    public class CsvDestination<TDest>
    {
        public readonly IEnumerable<TDest> destination;
        public CsvDestination(IEnumerable<TDest> destination)
        {
            this.destination = destination;
        }

        public async Task Save(TextWriter destination)
        {
            var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                MissingFieldFound = null
            };
            using var csv = new CsvWriter(destination, csvConfig);
            await csv.WriteRecordsAsync(this.destination);
        }
    }
}
