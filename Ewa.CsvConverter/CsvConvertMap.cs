using CsvHelper;
using CsvHelper.Configuration;
using Ewa.CsvConverter.Extensions;
using Ewa.CsvConverter.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Ewa.CsvConverter
{
    public class CsvConvertMap<TClass, TDest>
    {

        public readonly IEnumerable<TClass> data;

        public readonly bool hasHeaders;

        private List<ItemMap<TClass, TDest>> itemsDefinition;

        public CsvConvertMap(TextReader source, bool hasHeaders = false)
        {
            var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                MissingFieldFound = null
            };
            using var csv = new CsvReader(source, csvConfig);
            data = csv.GetRecords<TClass>().ToList();
            this.hasHeaders = hasHeaders;
            this.itemsDefinition = new List<ItemMap<TClass, TDest>>();
        }

        private IEnumerable<TClass> ApplyRules(Func<IEnumerable<TClass>, IEnumerable<TClass>> funcRules) =>
            funcRules(data);

        public ItemMap<TClass, TDest> Map<TItem>(Expression<Func<TDest, TItem>> expression)
        {
            MemberExpression? member = expression.Body as MemberExpression;
            var itemDefinition = new ItemMap<TClass, TDest>(member!.Member);
            this.itemsDefinition.Add(itemDefinition);
            return itemDefinition;
        }

        public CsvDestination<TDest> Convert(Func<IEnumerable<TClass>, IEnumerable<TClass>> rulesToApply)
        {
            var csvData = rulesToApply(data);

            var result = csvData
                            .AddContent(itemsDefinition);

            return new CsvDestination<TDest>(result);
        }
    }
}
