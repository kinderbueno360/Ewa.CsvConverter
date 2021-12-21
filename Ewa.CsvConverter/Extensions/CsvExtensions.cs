using Ewa.CsvConverter.Model;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Ewa.CsvConverter.Extensions
{
    public static class CsvExtensions
    {
        public static IEnumerable<T> Add<T>(this IEnumerable<T> e, T value)
        {
            foreach (var cur in e)
            {
                yield return cur;
            }
            yield return value;
        }


        public static IEnumerable<TDest> AddContent<TClass, TDest>(this IEnumerable<TClass> source, List<ItemMap<TClass, TDest>> itemsDefinition)
        {
            var list = new List<TDest>();
            foreach (var item in source)
            {
                TDest destinationItem = Activator.CreateInstance<TDest>();

                foreach (var itemDefinniton in itemsDefinition)
                {
                    var content = itemDefinniton.itemDefinition(item);
                    ((PropertyInfo)itemDefinniton.member).SetValue(destinationItem, content, null);

                }
                list.Add(destinationItem);
            }
            return list;
        }

    }
}
